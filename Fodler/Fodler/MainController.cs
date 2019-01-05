using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fodler.Forms;
using Fodler.Helpers;
using Fodler.Models;
using Fodler.Models.OutlookItem;
using Fodler.Properties;
using Microsoft.Office.Interop.Outlook;
using Action = System.Action;
using Exception = Microsoft.Office.Interop.Outlook.Exception;

namespace Fodler
{
    public static class MainController
    {
        #region Private

        private static bool _finishedLoading;
        private static readonly BackgroundWorker BwForLearning = new BackgroundWorker();
        private static HashSet<StoreModel> _storesWaitingForUpdate = new HashSet<StoreModel>();
        //Which stores are analyzed
        private static List<string> _analyzedStoresIds = new List<string>();
        //Model for each store, string is storeId
        private static Dictionary<string, StoreModel> _storeModels = new Dictionary<string, StoreModel>();
        //needs to be saved to catch event otherwise garbage collector throws it away
        private static SyncObject _syncObject;
        //Addin results
        private static Dictionary<Tuple<int,bool>, Result> _results = new Dictionary<Tuple<int, bool>, Result>();

        #endregion

        #region Public

        //Current selected item
        public static IOutlookItem SelectedItem { get; set; }
        //Scores for selected item
        public static Dictionary<string, double> SelectedMailScores { get; set; }
        #endregion

        #region EventHandling methods

        /// <summary>
        /// Background learning.
        /// Re-learn model on background
        /// </summary>
        private static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var parameters = e.Argument as object[];
                var storeModels = (HashSet<StoreModel>)parameters[0];
                foreach (var storeModel in storeModels)
                {
                    storeModel.CreateStoreModel();
                }
            }
            catch (System.Exception ex)
            {
                MyMessageBox.Show("Error occured during background learning. Error: " + ex);
            }
        }

        /// <summary>
        /// Runs when background learning is finished.
        /// If there is storeModel waiting for re-learn, it starts learning
        /// </summary>
        private static void bw_OnCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SaveData();
            SelectionChanged(SelectedItem);
            if (_storesWaitingForUpdate.Count != 0)
            {
                var param = new HashSet<StoreModel>(_storesWaitingForUpdate);
                object[] parameters = { param };
                BwForLearning.RunWorkerAsync(parameters);
                _storesWaitingForUpdate = new HashSet<StoreModel>();
            }
        }

        /// <summary>
        /// Loads data after sync of exchange account finishes.
        /// </summary>
        private static void mySyncObject_SyncEnd()
        {
            if (!_finishedLoading)
            {
                LoadData();
                SelectionChanged(SelectedItem);
            }
        }

        /// <summary>
        /// Work needed to be done on add-in start up.
        /// </summary>
        /// <param name="sessionSyncObject">Object needed to be saved from garbage collector</param>
        public static void OnAddinStartup(SyncObject sessionSyncObject)
        {
            Globals.Ribbons.MainRibbon.labelInfo.Label = "Addin is loading";
            FodlerSettings.Default.Upgrade();
            if (!FodlerSettings.Default.waitForSync || Globals.ThisAddIn.Application.Session.Offline)
            {
                LoadData();
            }
            else
            {
                _syncObject = sessionSyncObject;
                _syncObject.SyncEnd += mySyncObject_SyncEnd;
                _syncObject.Start();
            }

            BwForLearning.DoWork += bw_DoWork;
            BwForLearning.RunWorkerCompleted += bw_OnCompleted;
        }


        /// <summary>
        /// Saves all data if there are any.
        /// </summary>
        public static void OnAddinClose()
        {
            if (_analyzedStoresIds.Count > 0)
            {
                SaveData();
            }
        }

        #endregion

        #region FileEmail methods

        /// <summary>
        /// Files selected item to best match folder.
        /// </summary>
        public static void FileEmail()
        {
            FileEmail(SelectedItem, SelectedMailScores.First().Key, false);
        }

        /// <summary>
        /// Files selected item to folder and run learning if needed.
        /// </summary>
        /// <param name="folder">Destination folder for item</param>
        /// <param name="learn">Re-learn model if true</param>
        public static void FileEmail(string folder, bool learn)
        {
            FileEmail(SelectedItem, folder, learn);
        }

        /// <summary>
        /// Files item to folder and run learning if needed.
        /// </summary>
        /// <param name="item">Item to be moved</param>
        /// <param name="folder">Destination folder for item</param>
        /// <param name="learn">Re-learn model if true</param>   
        public static void FileEmail(IOutlookItem item, string folder, bool learn)
        {
            try
            {
                var storeModel = _storeModels[item.GetStoreId()];
                var destinationFolder = OutlookHelpers.GetFolderByName(folder);
                if (destinationFolder == null) return;
                item.Move(destinationFolder);
                var keyForResults = new Tuple<int, bool>(1, true);
                if (!_results.ContainsKey(keyForResults))
                {
                    _results.Add(keyForResults, new Result() { LearnerId = 1, WithBody = true});
                }
                if (learn)
                {
                    _results[keyForResults].IncrementMistakes();
                    if (BwForLearning.IsBusy)
                    {
                        _storesWaitingForUpdate.Add(storeModel);
                    }
                    else
                    {
                        object[] parameters = { new HashSet<StoreModel>() { storeModel } };
                        BwForLearning.RunWorkerAsync(parameters);
                    }
                }
                else
                {
                    _results[keyForResults].IncrementSuccess();
                }
            }
            catch (System.Exception ex)
            {
                MyMessageBox.Show("Error when moving email to folder. Error: " + ex);
            }
        }

        #endregion

        #region DataHandling

        /// <summary>
        /// Saves data needed for add-in to work
        /// </summary>
        public static void SaveData()
        {
            try
            {
                foreach (var model in _storeModels)
                {
                    model.Value.SaveStoreModel();
                }
                var stores = _storeModels.Select(s => s.Value.Store.DisplayName).ToList();
                StorageHelpers.SerializeAndSave(stores, Constants.ParamStoresNames);
                StorageHelpers.SerializeAndSave(_results?.Select(r => r.Value).ToList(), Constants.ParamResults);
            }
            catch (System.Exception ex)
            {
                MyMessageBox.Show("Error when saving data. Error: " + ex);
            }
        }

        /// <summary>
        /// Loads all saved data
        /// </summary>
        public static void LoadData()
        {
            try
            {
                var storeNames = StorageHelpers.LoadAndDeserialize<List<string>>(Constants.ParamStoresNames);
                _results = StorageHelpers.LoadAndDeserialize<List<Result>>(Constants.ParamResults)?.ToDictionary(r => new Tuple<int, bool>(r.LearnerId, r.WithBody), r => r) 
                           ?? new Dictionary<Tuple<int,bool>, Result>();

                if (storeNames != null && storeNames.Count > 0)
                {
                    var userStores = OutlookHelpers.GetDictionaryStores();
                    _analyzedStoresIds = userStores.Where(d => storeNames.Contains(d.Value.DisplayName)).Select(s => s.Value.StoreID).ToList();
                    foreach (var userStore in userStores)
                    {
                        if (_analyzedStoresIds.Contains(userStore.Value.StoreID))
                        {
                            var storeModel = new StoreModel(userStore.Value);
                            storeModel.LoadStoreModel();
                            _storeModels.Add(storeModel.StoreId, storeModel);
                        }
                    }
                    _finishedLoading = true;
                    Globals.Ribbons.MainRibbon.labelInfo.Label = " ";
                }
                else
                {
                    Globals.Ribbons.MainRibbon.labelInfo.Label = "Not analyzed";
                }
            }
            catch (System.Exception ex)
            {
                MyMessageBox.Show("Error when loading data. Error: " + ex);
            }
        }

        /// <summary>
        /// Removes all addin saved data.
        /// </summary>
        public static void RemoveSavedData()
        {
            StorageHelpers.RemoveStores();
            foreach (var store in OutlookHelpers.GetDictionaryStores())
            {
                StorageHelpers.RemoveStorage(store.Value.DisplayName);
            }
            SelectedMailScores = new Dictionary<string, double>();
            _analyzedStoresIds = new List<string>();
            _storeModels = new Dictionary<string, StoreModel>();
            SelectionChanged(SelectedItem);
        }

        #endregion

        #region Analyzation

        /// <summary>
        /// Analyzes user folders and creates model based on filed emails.
        /// </summary>
        /// <param name="bgWorker">Reports progress</param>
        /// <param name="label">Label to display progress</param>
        /// <param name="stores">Stores selected for analyze</param>
        /// <param name="selectedLearner">Selected learner index</param>
        /// <param name="includeEmailText">If email body should be analyzed too</param>
        public static void AnalyzeAsync(BackgroundWorker bgWorker, Label label, List<Store> stores)
        {
            try
            {
                _finishedLoading = false;
                bgWorker.ReportProgress(0);
                _analyzedStoresIds = new List<string>();
                _storeModels = new Dictionary<string, StoreModel>();
                var value = 100 / stores.Count;
                var currentValue = value;
                foreach (var store in stores)
                {
                    label.Invoke(new Action(() => label.Text = Resources.Analyzing + " " + store.DisplayName));
                    var storeModel = new StoreModel(store);
                    if (storeModel.CreateStoreModel())
                    {
                        _storeModels.Add(store.StoreID, storeModel);
                        _analyzedStoresIds.Add(store.StoreID);
                    }
                    bgWorker.ReportProgress(currentValue);
                    currentValue += value;
                }
                _finishedLoading = true;
                SelectionChanged(SelectedItem);
                label.Invoke(new Action(() => label.Text = Resources.SavingData));
                SaveData();
                bgWorker.ReportProgress(100);
                label.Invoke(new Action(() => label.Text = Resources.Finished));

                Globals.Ribbons.MainRibbon.labelInfo.Label = " ";
            }
            catch (System.Exception ex)
            {
                MyMessageBox.Show("Error when analyzing stores. Error: " + ex);
            }
        }

        /// <summary>
        /// Analyzes selected item and create folders scores for it.
        /// </summary>
        /// <param name="item">Selected item</param>
        public static async void SelectionChanged(IOutlookItem item)
        {
            try
            {
                SelectedItem = item;
                var r = item.GetText();
                var s = item.GetSubject();
                SelectedMailScores = await Task.Run(() => GetScoresForMail(item));

                var myRibbon = Globals.Ribbons.MainRibbon;
                myRibbon.menuChoose.Enabled = SelectedMailScores.Count > 0;
                myRibbon.btnFileEmail.Enabled = SelectedMailScores.Count > 0;
                myRibbon.btnChoose1.Visible = SelectedMailScores.Count > 0;
                myRibbon.btnNdOpt.Enabled = SelectedMailScores.Count > 1;
                myRibbon.btnChoose2.Visible = SelectedMailScores.Count > 1;
                myRibbon.btnRdOpt.Enabled = SelectedMailScores.Count > 2;
                myRibbon.btnChoose3.Visible = SelectedMailScores.Count > 2;
                myRibbon.btnChoose4.Visible = SelectedMailScores.Count > 3;
                myRibbon.btnChoose5.Visible = SelectedMailScores.Count > 4;

                myRibbon.btnFileEmail.Label = SelectedMailScores.Count > 0 ? SelectedMailScores.First().Key : "";
                myRibbon.btnNdOpt.Label = SelectedMailScores.Count > 1 ? OutlookHelpers.AdjustLabelLength(SelectedMailScores.ElementAt(1).Key) : "";
                myRibbon.btnRdOpt.Label = SelectedMailScores.Count > 2 ? OutlookHelpers.AdjustLabelLength(SelectedMailScores.ElementAt(2).Key) : "";
                myRibbon.btnChoose1.Label = SelectedMailScores.Count > 0 ? OutlookHelpers.AdjustLabelLength(SelectedMailScores.First().Key) : "";
                myRibbon.btnChoose2.Label = SelectedMailScores.Count > 1 ? OutlookHelpers.AdjustLabelLength(SelectedMailScores.ElementAt(1).Key) : "";
                myRibbon.btnChoose3.Label = SelectedMailScores.Count > 2 ? OutlookHelpers.AdjustLabelLength(SelectedMailScores.ElementAt(2).Key) : "";
                myRibbon.btnChoose4.Label = SelectedMailScores.Count > 3 ? OutlookHelpers.AdjustLabelLength(SelectedMailScores.ElementAt(3).Key) : "";
                myRibbon.btnChoose5.Label = SelectedMailScores.Count > 4 ? OutlookHelpers.AdjustLabelLength(SelectedMailScores.ElementAt(4).Key) : "";
            }
            catch (System.Exception ex)
            {
                MyMessageBox.Show("Error occured when selection changed. Error: " + ex);
            }
        }

        /// <summary>
        /// Returns folder names and its scores.
        /// </summary>
        /// <param name="item">Item to be analyzed</param>
        public static Dictionary<string, double> GetScoresForMail(IOutlookItem item)
        {
            try
            {
                if (item != null && _storeModels.Count != 0 && _analyzedStoresIds.Contains(item.GetStoreId()) && _finishedLoading)
                {
                    var storeModelOfItem = _storeModels[item.GetStoreId()];
                    return storeModelOfItem.GetScoresForMail(item);
                }
                
            }
            catch (System.Exception ex)
            {
                MyMessageBox.Show("Error when getting scores for email. Error: " + ex);
            }
            return new Dictionary<string, double>();
        }

        /// <summary>
        /// Returns addin results.
        /// </summary>
        public static string GetResults()
        {
            try
            {
                var strB = new StringBuilder();
                foreach (var result in _results)
                {
                    strB.AppendLine(result.Value.ToString());
                }

                return strB.ToString();
            }
            catch (System.Exception ex)
            {
                MyMessageBox.Show("Error when getting results. Error: " + ex);
            }

            return string.Empty;
        }

        #endregion
    }
}

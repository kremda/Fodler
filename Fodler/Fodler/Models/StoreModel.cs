using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Fodler.Helpers;
using Fodler.Models.MachineLearning;
using Fodler.Models.OutlookItem;
using Microsoft.Office.Interop.Outlook;

namespace Fodler.Models
{
    public class StoreModel
    {
        //Folders to ignore
        private readonly List<OlDefaultFolders> _defaultFolders = new List<OlDefaultFolders> {
            OlDefaultFolders.olFolderDeletedItems,
            OlDefaultFolders.olFolderInbox,
            OlDefaultFolders.olFolderSentMail,
            OlDefaultFolders.olFolderSyncIssues,
            OlDefaultFolders.olFolderLocalFailures,
        };
        //Ignored folders ids
        private readonly List<string> _foldersIdsToIgnore = new List<string>();

        public AccordML Accord { get; set; } 
        public string StoreId { get; set; } 
        public Store Store { get; set; } 
        public Dictionary<string, Folder> StoreFolders { get; set; }

        public StoreModel(Store store)
        {
            Accord = new AccordML();
            Store = store;
            StoreId = store.StoreID;
            foreach (var i in _defaultFolders)
            {
                _foldersIdsToIgnore.Add(Store.GetDefaultFolder(i).EntryID);
            }
            StoreFolders = OutlookHelpers.GetStoreFolders(store);
        }

        /// <summary>
        /// Gets all mail and meeting items for folder.
        /// </summary>
        /// <param name="folder">Folder to search for items</param>
        /// <param name="result">Result items for folder</param>
        private void GetAllItems(Folder folder, Dictionary<IOutlookItem, string> result)
        {
            bool isIgnored;
            try
            {
                isIgnored = _foldersIdsToIgnore.Contains(folder.EntryID);
            }
            catch (System.Exception)
            {
                //Some folders cant be accessed offline and throws exception
                return;
            }
            if (folder.DefaultItemType == OlItemType.olMailItem && !isIgnored)
            {
                foreach (var item in folder.Items)
                {
                    if (item != null && ( item is MailItem || item is MeetingItem))
                    {
                        result.Add(OutlookItemHelpers.GetItemClass(item), folder.Name);
                    }
                }
            }

            if (folder.Folders.Count == 0) return;
            foreach (Folder subFolder in folder.Folders)
            {
                GetAllItems(subFolder, result);
            }
        }

        /// <summary>
        /// Creates model and call learning.
        /// </summary>
        public bool CreateStoreModel()
        {
            var allItems = new Dictionary<IOutlookItem, string>();

            foreach (var folder in StoreFolders)
            {
                GetAllItems(folder.Value, allItems);
            }

            var listInputs = allItems.Select(k => k.Key.GetInput()).ToArray();
            var listSubjects = allItems.Select(k => k.Key.GetSubject()).ToArray(); ;
            var listTexts = allItems.Select(k => k.Key.GetText()).ToArray(); ;

            var listOutputs = allItems.Select(v => v.Value).ToArray();
            var uniqueFolders = new HashSet<string>(listOutputs);
            if (uniqueFolders.Count > 1)
            {
                Accord.CreateModel(listInputs, listSubjects, listTexts, listOutputs);
                return true;
            }

            MessageBox.Show("Store " + Store.DisplayName + " not analyzed, not enough folders with items");
            return false;
        }

        /// <summary>
        /// Return scores for item.
        /// </summary>
        /// <param name="item">Item to be analyzed</param>
        public Dictionary<string, double> GetScoresForMail(IOutlookItem item)
        {
            if (item != null && Accord.IsReady())
            {
                var input = item.GetInput();
                var subject = item.GetSubject();
                var text = item.GetText();
                var results = Accord.DecideFolder(input, subject, text);
                return results;
            }

            return new Dictionary<string, double>();
        }

        public void SaveStoreModel()
        {
            Accord.Save(Store.DisplayName);
        }

        public void LoadStoreModel()
        {
            Accord.Load(Store.DisplayName);
        }
    }
}

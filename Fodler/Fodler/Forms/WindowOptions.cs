using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Fodler.Properties;
using Microsoft.Office.Interop.Outlook;
using Action = System.Action;

namespace Fodler.Forms
{
    public partial class WindowOptions : Form
    {
        private readonly Dictionary<string, Store> _stores;
        private readonly bool _isLoading;
        public WindowOptions(Dictionary<string,Store> stores)
        {
            InitializeComponent();
            _isLoading = true;
            chBWaitForSync.Checked = FodlerSettings.Default.waitForSync;
            backgroundWorkerAnalyze.WorkerReportsProgress = true;
            _stores = stores;
            listStores.CheckOnClick = true;
            listStores.DataSource = stores.Keys.ToList();

            _isLoading = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            backgroundWorkerAnalyze.RunWorkerAsync();
        }

        private void listStores_SelectedValueChanged(object sender, EventArgs e)
        {
            btnAnalyze.Enabled = false;
            if (listStores.CheckedItems.Count != 0)
            {
                btnAnalyze.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainController.RemoveSavedData();
        }

        private void backgroundWorkerAnalyze_DoWork(object sender, DoWorkEventArgs e)
        {
            var checkedItems = listStores.CheckedItems;
            var result = new List<Store>();
            foreach (var item in checkedItems)
            {
                if (_stores.ContainsKey(item.ToString()))
                {
                    result.Add(_stores[item.ToString()]);
                }
            }

            labelProgress.Invoke(new Action(() => labelProgress.Text = Resources.AnalyzeStared));
            MainController.AnalyzeAsync(backgroundWorkerAnalyze, labelProgress, result);
        }

        private void backgroundWorkerAnalyze_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarAnalyze.Invoke(new Action(() => progressBarAnalyze.Value = e.ProgressPercentage));
            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            using (var winReport = new WindowSendEmail())
            {
                winReport.ShowDialog();
            }
        }

        private void chBWaitForSync_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                FodlerSettings.Default.waitForSync = chBWaitForSync.Checked;
                FodlerSettings.Default.Save();
            }
        }
    }
}

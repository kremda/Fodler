using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Fodler.Helpers;
using Fodler.Models.OutlookItem;
using Fodler.Properties;
using Microsoft.Office.Interop.Outlook;

namespace Fodler.Forms
{
    public partial class WindowChooseFolder : Form
    {
        private readonly IOutlookItem _item;
        public WindowChooseFolder(Dictionary<string,double> results, IOutlookItem item)
        {
            InitializeComponent();
            _item = item;
            PopulateTree(OutlookHelpers.GetListStores());
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewFolders.SelectedNode;
            if (selectedNode == null)
            {
                labelInfoText.Text = Resources.SelectFolderFirst;
                return;
            }
            var folder = OutlookHelpers.GetFolderByName(selectedNode.Text);
            MainController.FileEmail(_item, folder.Name, true);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PopulateTree(List<Store> stores)
        {
            treeViewFolders.BeginUpdate();
            foreach (Store store in stores)
            {
                treeViewFolders.Nodes.Add(OutlookHelpers.CreateTreeNodeForStore(store));
            }
            treeViewFolders.Sort();
            treeViewFolders.EndUpdate();
        }

        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewFolders.SelectedNode;
            if (selectedNode == null)
            {
                labelInfoText.Text = Resources.SelectParentFirst;
                return;
            }
            var parentFolder = OutlookHelpers.GetFolderByName(selectedNode.Text);
            using (var wCreateFolder = new WindowCreateFolder(parentFolder, _item))
            {
                var result = wCreateFolder.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }           
        }
    }
}

using System;
using System.Windows.Forms;
using Fodler.Helpers;
using Fodler.Models.OutlookItem;
using Microsoft.Office.Interop.Outlook;

namespace Fodler.Forms
{
    public partial class WindowCreateFolder : Form
    {
        private readonly Folder _parentFolder;
        private readonly IOutlookItem _item;

        public WindowCreateFolder(Folder parentFolder, IOutlookItem item)
        {
            InitializeComponent();
            _parentFolder = parentFolder;
            textBoxFolderPath.Text = _parentFolder.FolderPath;
            _item = item;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var folderName = textBoxFolderName.Text;
            if (!string.IsNullOrWhiteSpace(folderName))
            {
                Folder folder = OutlookHelpers.CreateFolder(_parentFolder, folderName);
                if (folder == null)
                {
                    return;
                }

                if (checkBoxFile.Checked)
                {
                    MainController.FileEmail(_item, folder.Name, true);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}

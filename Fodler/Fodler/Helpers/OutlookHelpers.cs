using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Windows.Forms;
using Fodler.Properties;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;

namespace Fodler.Helpers
{
    public static class OutlookHelpers
    {
        #region Stores methods

        /// <summary>
        /// Get all folders for store
        /// </summary>
        /// <param name="store">Store to get folders from</param>
        public static Dictionary<string, Folder> GetStoreFolders(Store store)
        {
            Dictionary<string, Folder> result = new Dictionary<string, Folder>();
            GetFolders((Folder)store.GetRootFolder(), result);
            return result;
        }

        /// <summary>
        /// Recursively writes folder to dictionaryToWrite
        /// </summary>
        /// <param name="folder">Folder to be scanned</param>
        /// <param name="dictionaryToWrite">Output dictionary</param>
        private static void GetFolders(Folder folder, Dictionary<string, Folder> dictionaryToWrite)
        {
            dictionaryToWrite.Add(folder.Name, folder);
            try
            {
                if (folder.Folders.Count == 0) return;
            }
            catch (Exception)
            {
                dictionaryToWrite.Remove(folder.Name);
                return;
            }

            foreach (Folder subFolder in folder.Folders)
            {
                GetFolders(subFolder, dictionaryToWrite);
            }
        }

        /// <summary>
        /// Creates TreeNode from store and its folders for TreeView element
        /// </summary>
        /// <param name="store">Store to be changed into TreeNode</param>
        public static TreeNode CreateTreeNodeForStore(Store store)
        {
            var rootFolder = store.GetRootFolder();
            var resultNode = new TreeNode(rootFolder.Name);
            RecursivePopulate((Folder)rootFolder, resultNode);
            return resultNode;
        }

        private static void RecursivePopulate(Folder rootFolder, TreeNode resultNode)
        {
            if (rootFolder.Folders.Count != 0)
            {
                foreach (Folder folder in rootFolder.Folders)
                {
                    var newNode = new TreeNode(folder.Name);
                    RecursivePopulate(folder, newNode);
                    resultNode.Nodes.Add(newNode);
                }
            }
        }

        /// <summary>
        /// Returns Folder by folder name
        /// </summary>
        public static Folder GetFolderByName(string name)
        {
            foreach (Store store in GetListStores())
            {
                var folders = GetStoreFolders(store);
                if (folders.ContainsKey(name))
                {
                    return folders[name];
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all user stores
        /// </summary>
        private static Stores GetStores()
        {
            var olApp = Globals.ThisAddIn.Application;
            return olApp.Session.Stores;
        }

        /// <summary>
        /// Gets all user stores as list
        /// </summary>
        public static List<Store> GetListStores()
        {
            return GetStores().Cast<Store>().ToList();
        }

        /// <summary>
        /// Gets all user stores as Dictionary where KeyValuePair is store name and store
        /// </summary>
        public static Dictionary<string, Store> GetDictionaryStores()
        {
            return GetStores().Cast<Store>().ToDictionary(store => store.DisplayName, store => store);
        }

        /// <summary>
        /// Creates new folder
        /// </summary>
        /// <param name="parentFolder">New folder parent</param>
        /// <param name="folderName">New folder name</param>
        public static Folder CreateFolder(Folder parentFolder, string folderName)
        {
            try
            {
                var newFolder = parentFolder.Folders.Add(folderName, OlDefaultFolders.olFolderInbox);
                return newFolder as Folder;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorOccured + ex.Message);
            }

            return null;
        }

        #endregion

        /// <summary>
        /// Trim texts length to 20 if longer
        /// </summary>
        public static string AdjustLabelLength(string text)
        {
            if (text.Length > Constants.MaxLabelTextLength)
            {
                return text.Substring(0, Constants.MaxLabelTextLength);
            }

            return text;
        }

        /// <summary>
        /// Return domain for given email address
        /// </summary>
        public static string GetDomainFromEmail(string emailAddress)
        {
            if (emailAddress != null && emailAddress.Contains('@'))
            {
                return emailAddress.Split('@')[1];
            }

            return "";
        }

        public static void SendEmail(string subjectEmail, string bodyEmail)
        {
            bodyEmail += $"Outlook Version - {Globals.ThisAddIn.Application.Version}" + Environment.NewLine;
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment applicationDeployment = ApplicationDeployment.CurrentDeployment;

                Version version = applicationDeployment.CurrentVersion;

                bodyEmail += $"Add-on Version - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            }
            var eMail = (MailItem)Globals.ThisAddIn.Application.CreateItem(OlItemType.olMailItem);
            eMail.Subject = subjectEmail;
            eMail.To = "Patrik.Kremecek@gmail.com";
            eMail.Body = bodyEmail;
            eMail.Importance = OlImportance.olImportanceNormal;
            eMail.Send();
        }
    }
}

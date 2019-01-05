using Fodler.Helpers;
using Fodler.Models.OutlookItem;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace Fodler
{
    public partial class ThisAddIn
    {
        private Outlook.Explorer _currentExplorer;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _currentExplorer = Application.ActiveExplorer();
            _currentExplorer.SelectionChange += CurrentExplorer_Event;
            ((Outlook.ApplicationEvents_11_Event)Application).Quit += ThisAddIn_Quit;
            MainController.OnAddinStartup(_currentExplorer.Session.SyncObjects[1]);
        }

        //Selection changed event handling
        private void CurrentExplorer_Event()
        {
            if (Application.ActiveExplorer().Selection.Count <= 0) return;
            object selObject = Application.ActiveExplorer().Selection[1];
            if (selObject != null &&( selObject is Outlook.MailItem || selObject is Outlook.MeetingItem))
            {
                MainController.SelectionChanged(OutlookItemHelpers.GetItemClass(selObject));
            }  
        }

        void ThisAddIn_Quit()
        {
            MainController.OnAddinClose();
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
        }
        
        #endregion
    }
}

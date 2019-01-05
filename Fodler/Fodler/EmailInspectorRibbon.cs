using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fodler.Forms;
using Fodler.Helpers;
using Fodler.Models.OutlookItem;
using Microsoft.Office.Tools.Ribbon;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Fodler
{
    public partial class EmailInspectorRibbon
    {
        private Dictionary<string, double> _results;
        private IOutlookItem _item;

        private async void EmailInspectorRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            var inspector = Context as Outlook.Inspector;
            object item = inspector?.CurrentItem;
            if (item != null && ( item is Outlook.MailItem || item is Outlook.MeetingItem))
            {
                _item = OutlookItemHelpers.GetItemClass(item);
                
                _results = await Task.Run(() => MainController.GetScoresForMail(_item));
                EnableButtons(_results.Count);
                btnFileEmailInspector.Label = _results.Count > 0 ? _results.First().Key : "";
                btnNdOptInspector.Label = _results.Count > 1 ? _results.ElementAt(1).Key : "";
                btnRdOptInspector.Label = _results.Count > 2 ? _results.ElementAt(2).Key : "";
                btnChoose1.Label = _results.Count > 0 ? _results.First().Key : "";
                btnChoose2.Label = _results.Count > 1 ? _results.ElementAt(1).Key : "";
                btnChoose3.Label = _results.Count > 2 ? _results.ElementAt(2).Key : "";
                btnChoose4.Label = _results.Count > 3 ? _results.ElementAt(3).Key : "";
                btnChoose5.Label = _results.Count > 4 ? _results.ElementAt(4).Key : "";
            }
            
        }

        private void btnFileEmailInspector_Click(object sender, RibbonControlEventArgs e)
        {
            MainController.FileEmail(_item,_results.First().Key, false);
            //mailItem.Display(false);
            //------------------- UZAVRE VIEW, CHCI TO?----------------
        }

        private void btnNdOptInspector_Click(object sender, RibbonControlEventArgs e)
        {
            MainController.FileEmail(_item, _results.ElementAt(1).Key, true);
        }

        private void btnRdOptInspector_Click(object sender, RibbonControlEventArgs e)
        {
            MainController.FileEmail(_item, _results.ElementAt(2).Key, true);
        }

        private void btnChooseInspector_Click(object sender, RibbonControlEventArgs e)
        {
            var lof = new WindowChooseFolder(_results, _item);
            lof.ShowDialog();
        }

        private void EnableButtons(int count)
        {
            btnFileEmailInspector.Enabled = count > 0;
            btnNdOptInspector.Enabled = count > 1;
            btnRdOptInspector.Enabled = count > 2;
            btnChoose1.Visible = count > 0;
            btnChoose2.Visible = count > 1;
            btnChoose3.Visible = count > 2;
            btnChoose4.Visible = count > 3;
            btnChoose5.Visible = count > 4;

        }

        private void btnChoose4_Click(object sender, RibbonControlEventArgs e)
        {
            MainController.FileEmail(_item, _results.ElementAt(3).Key, true);
        }

        private void btnChoose5_Click(object sender, RibbonControlEventArgs e)
        {
            MainController.FileEmail(_item, _results.ElementAt(4).Key, true);
        }
    }
}

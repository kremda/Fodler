using System.Linq;
using Fodler.Forms;
using Fodler.Helpers;
using Microsoft.Office.Tools.Ribbon;
using Office = Microsoft.Office.Core;

namespace Fodler
{
    public partial class MainRibbon
    {
        private void MyCustomRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void btnFileEmail_Click(object sender, RibbonControlEventArgs e)
        {
            MainController.FileEmail();
        }

        private void btnOptions_Click(object sender, RibbonControlEventArgs e)
        {
            using (var options = new WindowOptions(OutlookHelpers.GetDictionaryStores()))
            {
                options.ShowDialog();
            }
        }

        private void btnNdOpt_Click(object sender, RibbonControlEventArgs e)
        {
            MainController.FileEmail(MainController.SelectedMailScores.ElementAt(1).Key, true);
        }

        private void btnRdOpt_Click(object sender, RibbonControlEventArgs e)
        {
            MainController.FileEmail(MainController.SelectedMailScores.ElementAt(2).Key, true);
        }

        private void btnChoose_Click(object sender, RibbonControlEventArgs e)
        {
            WindowChooseFolder lof = new WindowChooseFolder(MainController.SelectedMailScores, MainController.SelectedItem);
            lof.ShowDialog();
        }

        private void btnChoose4_Click(object sender, RibbonControlEventArgs e)
        {
            MainController.FileEmail(MainController.SelectedMailScores.ElementAt(3).Key, true);
        }

        private void btnChoose5_Click(object sender, RibbonControlEventArgs e)
        {
            MainController.FileEmail(MainController.SelectedMailScores.ElementAt(4).Key, true);
        }
    }
}

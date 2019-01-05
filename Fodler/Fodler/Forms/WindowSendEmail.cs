using System;
using System.Windows.Forms;
using Fodler.Helpers;
using Fodler.Properties;

namespace Fodler.Forms
{
    public partial class WindowSendEmail : Form
    {
        public WindowSendEmail()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string subjectEmail = textBoxSubject.Text;
            string bodyEmail = textBoxDescription.Text;
            if (chBwithResults.Checked)
            {
                bodyEmail += Environment.NewLine + "Results:" + Environment.NewLine + MainController.GetResults();
            }
            OutlookHelpers.SendEmail(subjectEmail, bodyEmail);
            DialogResult = DialogResult.OK;
            Close();
            MessageBox.Show(Resources.EmailSentThankYou);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

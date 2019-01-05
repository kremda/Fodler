using System;
using System.Windows.Forms;
using Fodler.Helpers;
using Fodler.Properties;

namespace Fodler.Forms
{
    internal partial class MyMessageForm : Form
    {
        public MyMessageForm()
        {
            InitializeComponent();
        }

        public MyMessageForm(string description)
        {
            InitializeComponent();
            textBoxText.Text = description;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            OutlookHelpers.SendEmail(Resources.FodlerErrorReport, textBoxText.Text);
            Close();
        }
    }

    public static class MyMessageBox
    {
        public static void Show(string description)
        {
            using (var form = new MyMessageForm(description))
            {
                form.ShowDialog();
            }
        }
    }
}

namespace Fodler
{
    partial class EmailInspectorRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public EmailInspectorRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.MyGroup = this.Factory.CreateRibbonGroup();
            this.btnFileEmailInspector = this.Factory.CreateRibbonButton();
            this.btnNdOptInspector = this.Factory.CreateRibbonButton();
            this.btnRdOptInspector = this.Factory.CreateRibbonButton();
            this.button4 = this.Factory.CreateRibbonButton();
            this.button5 = this.Factory.CreateRibbonButton();
            this.menuChoose = this.Factory.CreateRibbonMenu();
            this.btnChoose1 = this.Factory.CreateRibbonButton();
            this.btnChoose2 = this.Factory.CreateRibbonButton();
            this.btnChoose3 = this.Factory.CreateRibbonButton();
            this.btnChoose4 = this.Factory.CreateRibbonButton();
            this.btnChoose5 = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.btnChooseOther = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.MyGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.ControlId.OfficeId = "TabReadMessage";
            this.tab1.Groups.Add(this.MyGroup);
            this.tab1.Label = "TabReadMessage";
            this.tab1.Name = "tab1";
            // 
            // MyGroup
            // 
            this.MyGroup.Items.Add(this.btnFileEmailInspector);
            this.MyGroup.Items.Add(this.btnNdOptInspector);
            this.MyGroup.Items.Add(this.btnRdOptInspector);
            this.MyGroup.Items.Add(this.menuChoose);
            this.MyGroup.Label = "Fodler";
            this.MyGroup.Name = "MyGroup";
            this.MyGroup.Position = this.Factory.RibbonPosition.AfterOfficeId("GroupRespond");
            // 
            // btnFileEmailInspector
            // 
            this.btnFileEmailInspector.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnFileEmailInspector.Image = global::Fodler.Properties.Resources.iconMove;
            this.btnFileEmailInspector.Label = "1st opt";
            this.btnFileEmailInspector.Name = "btnFileEmailInspector";
            this.btnFileEmailInspector.ShowImage = true;
            this.btnFileEmailInspector.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFileEmailInspector_Click);
            // 
            // btnNdOptInspector
            // 
            this.btnNdOptInspector.Image = global::Fodler.Properties.Resources.iconMove;
            this.btnNdOptInspector.Label = "2nd opt";
            this.btnNdOptInspector.Name = "btnNdOptInspector";
            this.btnNdOptInspector.ShowImage = true;
            this.btnNdOptInspector.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnNdOptInspector_Click);
            // 
            // btnRdOptInspector
            // 
            this.btnRdOptInspector.Image = global::Fodler.Properties.Resources.iconMove;
            this.btnRdOptInspector.Label = "3rd opt";
            this.btnRdOptInspector.Name = "btnRdOptInspector";
            this.btnRdOptInspector.ShowImage = true;
            this.btnRdOptInspector.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRdOptInspector_Click);
            // 
            // button4
            // 
            this.button4.Label = "Revert";
            this.button4.Name = "button4";
            this.button4.ShowImage = true;
            // 
            // button5
            // 
            this.button5.Label = "File All";
            this.button5.Name = "button5";
            this.button5.ShowImage = true;
            // 
            // menuChoose
            // 
            this.menuChoose.Image = global::Fodler.Properties.Resources.iconChoose;
            this.menuChoose.Items.Add(this.btnChoose1);
            this.menuChoose.Items.Add(this.btnChoose2);
            this.menuChoose.Items.Add(this.btnChoose3);
            this.menuChoose.Items.Add(this.btnChoose4);
            this.menuChoose.Items.Add(this.btnChoose5);
            this.menuChoose.Items.Add(this.separator1);
            this.menuChoose.Items.Add(this.btnChooseOther);
            this.menuChoose.Label = "Choose";
            this.menuChoose.Name = "menuChoose";
            this.menuChoose.ShowImage = true;
            // 
            // btnChoose1
            // 
            this.btnChoose1.Label = " ";
            this.btnChoose1.Name = "btnChoose1";
            this.btnChoose1.ShowImage = true;
            this.btnChoose1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFileEmailInspector_Click);
            // 
            // btnChoose2
            // 
            this.btnChoose2.Label = " ";
            this.btnChoose2.Name = "btnChoose2";
            this.btnChoose2.ShowImage = true;
            this.btnChoose2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnNdOptInspector_Click);
            // 
            // btnChoose3
            // 
            this.btnChoose3.Label = " ";
            this.btnChoose3.Name = "btnChoose3";
            this.btnChoose3.ShowImage = true;
            this.btnChoose3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRdOptInspector_Click);
            // 
            // btnChoose4
            // 
            this.btnChoose4.Label = " ";
            this.btnChoose4.Name = "btnChoose4";
            this.btnChoose4.ShowImage = true;
            this.btnChoose4.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnChoose4_Click);
            // 
            // btnChoose5
            // 
            this.btnChoose5.Label = " ";
            this.btnChoose5.Name = "btnChoose5";
            this.btnChoose5.ShowImage = true;
            this.btnChoose5.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnChoose5_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // btnChooseOther
            // 
            this.btnChooseOther.Label = "Other";
            this.btnChooseOther.Name = "btnChooseOther";
            this.btnChooseOther.ShowImage = true;
            this.btnChooseOther.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnChooseInspector_Click);
            // 
            // EmailInspectorRibbon
            // 
            this.Name = "EmailInspectorRibbon";
            this.RibbonType = "Microsoft.Outlook.Mail.Read, Microsoft.Outlook.MeetingRequest.Read";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.EmailInspectorRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.MyGroup.ResumeLayout(false);
            this.MyGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup MyGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFileEmailInspector;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNdOptInspector;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRdOptInspector;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button5;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuChoose;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnChoose1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnChoose2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnChoose3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnChoose4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnChoose5;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnChooseOther;
    }

    partial class ThisRibbonCollection
    {
        internal EmailInspectorRibbon EmailInspectorRibbon
        {
            get { return this.GetRibbon<EmailInspectorRibbon>(); }
        }
    }
}

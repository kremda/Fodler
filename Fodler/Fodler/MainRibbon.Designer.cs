namespace Fodler
{
    partial class MainRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public MainRibbon()
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
            this.MyTab = this.Factory.CreateRibbonTab();
            this.MyGroup = this.Factory.CreateRibbonGroup();
            this.box1 = this.Factory.CreateRibbonBox();
            this.labelInfo = this.Factory.CreateRibbonLabel();
            this.box2 = this.Factory.CreateRibbonBox();
            this.btnFileEmail = this.Factory.CreateRibbonButton();
            this.btnNdOpt = this.Factory.CreateRibbonButton();
            this.btnRdOpt = this.Factory.CreateRibbonButton();
            this.menuChoose = this.Factory.CreateRibbonMenu();
            this.btnChoose1 = this.Factory.CreateRibbonButton();
            this.btnChoose2 = this.Factory.CreateRibbonButton();
            this.btnChoose3 = this.Factory.CreateRibbonButton();
            this.btnChoose4 = this.Factory.CreateRibbonButton();
            this.btnChoose5 = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.btnChooseOther = this.Factory.CreateRibbonButton();
            this.btnOptions = this.Factory.CreateRibbonButton();
            this.MyTab.SuspendLayout();
            this.MyGroup.SuspendLayout();
            this.box1.SuspendLayout();
            this.box2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyTab
            // 
            this.MyTab.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.MyTab.ControlId.OfficeId = "TabMail";
            this.MyTab.Groups.Add(this.MyGroup);
            this.MyTab.Label = "TabMail";
            this.MyTab.Name = "MyTab";
            // 
            // MyGroup
            // 
            this.MyGroup.Items.Add(this.btnFileEmail);
            this.MyGroup.Items.Add(this.box1);
            this.MyGroup.Items.Add(this.box2);
            this.MyGroup.Label = "Fodler";
            this.MyGroup.Name = "MyGroup";
            this.MyGroup.Position = this.Factory.RibbonPosition.AfterOfficeId("GroupMailRespond");
            // 
            // box1
            // 
            this.box1.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.box1.Items.Add(this.btnNdOpt);
            this.box1.Items.Add(this.btnRdOpt);
            this.box1.Items.Add(this.labelInfo);
            this.box1.Name = "box1";
            // 
            // labelInfo
            // 
            this.labelInfo.Label = " ";
            this.labelInfo.Name = "labelInfo";
            // 
            // box2
            // 
            this.box2.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.box2.Items.Add(this.menuChoose);
            this.box2.Items.Add(this.btnOptions);
            this.box2.Name = "box2";
            // 
            // btnFileEmail
            // 
            this.btnFileEmail.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnFileEmail.Image = global::Fodler.Properties.Resources.iconMove;
            this.btnFileEmail.Label = "1st opt";
            this.btnFileEmail.Name = "btnFileEmail";
            this.btnFileEmail.ShowImage = true;
            this.btnFileEmail.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFileEmail_Click);
            // 
            // btnNdOpt
            // 
            this.btnNdOpt.Image = global::Fodler.Properties.Resources.iconMove;
            this.btnNdOpt.Label = "2nd opt";
            this.btnNdOpt.Name = "btnNdOpt";
            this.btnNdOpt.ShowImage = true;
            this.btnNdOpt.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnNdOpt_Click);
            // 
            // btnRdOpt
            // 
            this.btnRdOpt.Image = global::Fodler.Properties.Resources.iconMove;
            this.btnRdOpt.Label = "3rd opt";
            this.btnRdOpt.Name = "btnRdOpt";
            this.btnRdOpt.ShowImage = true;
            this.btnRdOpt.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRdOpt_Click);
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
            this.btnChoose1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFileEmail_Click);
            // 
            // btnChoose2
            // 
            this.btnChoose2.Label = " ";
            this.btnChoose2.Name = "btnChoose2";
            this.btnChoose2.ShowImage = true;
            this.btnChoose2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnNdOpt_Click);
            // 
            // btnChoose3
            // 
            this.btnChoose3.Label = " ";
            this.btnChoose3.Name = "btnChoose3";
            this.btnChoose3.ShowImage = true;
            this.btnChoose3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRdOpt_Click);
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
            this.btnChooseOther.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnChoose_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Image = global::Fodler.Properties.Resources.iconOptions;
            this.btnOptions.Label = "Options";
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.ShowImage = true;
            this.btnOptions.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnOptions_Click);
            // 
            // MainRibbon
            // 
            this.Name = "MainRibbon";
            this.RibbonType = "Microsoft.Outlook.Explorer, Microsoft.Outlook.Mail.Read";
            this.Tabs.Add(this.MyTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.MyCustomRibbon_Load);
            this.MyTab.ResumeLayout(false);
            this.MyTab.PerformLayout();
            this.MyGroup.ResumeLayout(false);
            this.MyGroup.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();
            this.box2.ResumeLayout(false);
            this.box2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab MyTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup MyGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFileEmail;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNdOpt;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRdOpt;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnOptions;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box2;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel labelInfo;
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
        internal MainRibbon MainRibbon
        {
            get { return this.GetRibbon<MainRibbon>(); }
        }
    }
}

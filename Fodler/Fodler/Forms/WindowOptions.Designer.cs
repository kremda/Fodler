using System.ComponentModel;
using System.Windows.Forms;

namespace Fodler.Forms
{
    partial class WindowOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowOptions));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.progressBarAnalyze = new System.Windows.Forms.ProgressBar();
            this.listStores = new System.Windows.Forms.CheckedListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.labelProgress = new System.Windows.Forms.Label();
            this.backgroundWorkerAnalyze = new System.ComponentModel.BackgroundWorker();
            this.btnReport = new System.Windows.Forms.Button();
            this.chBWaitForSync = new System.Windows.Forms.CheckBox();
            this.toolTipWaitForSync = new System.Windows.Forms.ToolTip(this.components);
            this.tTipIncludeBody = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(209, 100);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(131, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Enabled = false;
            this.btnAnalyze.Location = new System.Drawing.Point(36, 276);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(131, 23);
            this.btnAnalyze.TabIndex = 1;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // progressBarAnalyze
            // 
            this.progressBarAnalyze.Location = new System.Drawing.Point(12, 251);
            this.progressBarAnalyze.Name = "progressBarAnalyze";
            this.progressBarAnalyze.Size = new System.Drawing.Size(179, 18);
            this.progressBarAnalyze.TabIndex = 2;
            // 
            // listStores
            // 
            this.listStores.FormattingEnabled = true;
            this.listStores.Location = new System.Drawing.Point(12, 12);
            this.listStores.Name = "listStores";
            this.listStores.Size = new System.Drawing.Size(179, 199);
            this.listStores.TabIndex = 3;
            this.listStores.SelectedValueChanged += new System.EventHandler(this.listStores_SelectedValueChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(209, 72);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(131, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove saved data";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(12, 224);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(0, 13);
            this.labelProgress.TabIndex = 9;
            // 
            // backgroundWorkerAnalyze
            // 
            this.backgroundWorkerAnalyze.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerAnalyze_DoWork);
            this.backgroundWorkerAnalyze.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerAnalyze_ProgressChanged);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(209, 44);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(131, 23);
            this.btnReport.TabIndex = 12;
            this.btnReport.Text = "Send report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // chBWaitForSync
            // 
            this.chBWaitForSync.AutoSize = true;
            this.chBWaitForSync.Location = new System.Drawing.Point(196, 12);
            this.chBWaitForSync.Margin = new System.Windows.Forms.Padding(2);
            this.chBWaitForSync.Name = "chBWaitForSync";
            this.chBWaitForSync.Size = new System.Drawing.Size(163, 17);
            this.chBWaitForSync.TabIndex = 13;
            this.chBWaitForSync.Text = "Wait for data synchronization";
            this.toolTipWaitForSync.SetToolTip(this.chBWaitForSync, resources.GetString("chBWaitForSync.ToolTip"));
            this.chBWaitForSync.UseVisualStyleBackColor = true;
            this.chBWaitForSync.CheckedChanged += new System.EventHandler(this.chBWaitForSync_CheckedChanged);
            // 
            // toolTipWaitForSync
            // 
            this.toolTipWaitForSync.AutoPopDelay = 10000;
            this.toolTipWaitForSync.InitialDelay = 500;
            this.toolTipWaitForSync.ReshowDelay = 100;
            // 
            // tTipIncludeBody
            // 
            this.tTipIncludeBody.AutoPopDelay = 10000;
            this.tTipIncludeBody.InitialDelay = 500;
            this.tTipIncludeBody.ReshowDelay = 100;
            // 
            // WindowOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 311);
            this.Controls.Add(this.chBWaitForSync);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.listStores);
            this.Controls.Add(this.progressBarAnalyze);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(222, 344);
            this.Name = "WindowOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnClose;
        private Button btnAnalyze;
        private ProgressBar progressBarAnalyze;
        private CheckedListBox listStores;
        private Button btnRemove;
        private Label labelProgress;
        private BackgroundWorker backgroundWorkerAnalyze;
        private Button btnReport;
        private CheckBox chBWaitForSync;
        private ToolTip toolTipWaitForSync;
        private ToolTip tTipIncludeBody;
    }
}
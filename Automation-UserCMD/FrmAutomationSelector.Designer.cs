namespace Automation_UserCMD
{
    partial class FrmAutomationSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.lblAutomationTitleHeader = new System.Windows.Forms.Label();
            this.btnNavigateCmdAutomation = new System.Windows.Forms.Button();
            this.btnNavigateTincanAutomation = new System.Windows.Forms.Button();
            this.lblSelectEnvironment = new System.Windows.Forms.Label();
            this.CbEnvironmentSelect = new System.Windows.Forms.ComboBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // lblAutomationTitleHeader
            // 
            this.lblAutomationTitleHeader.AutoSize = true;
            this.lblAutomationTitleHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutomationTitleHeader.Location = new System.Drawing.Point(267, 9);
            this.lblAutomationTitleHeader.Name = "lblAutomationTitleHeader";
            this.lblAutomationTitleHeader.Size = new System.Drawing.Size(311, 25);
            this.lblAutomationTitleHeader.TabIndex = 3;
            this.lblAutomationTitleHeader.Text = "CMD and Tincan Validations";
            // 
            // btnNavigateCmdAutomation
            // 
            this.btnNavigateCmdAutomation.Location = new System.Drawing.Point(35, 279);
            this.btnNavigateCmdAutomation.Name = "btnNavigateCmdAutomation";
            this.btnNavigateCmdAutomation.Size = new System.Drawing.Size(203, 23);
            this.btnNavigateCmdAutomation.TabIndex = 4;
            this.btnNavigateCmdAutomation.Text = "Navigate to CMD Automation";
            this.btnNavigateCmdAutomation.UseVisualStyleBackColor = true;
            this.btnNavigateCmdAutomation.Click += new System.EventHandler(this.btnNavigateCmdAutomation_Click);
            // 
            // btnNavigateTincanAutomation
            // 
            this.btnNavigateTincanAutomation.Location = new System.Drawing.Point(597, 279);
            this.btnNavigateTincanAutomation.Name = "btnNavigateTincanAutomation";
            this.btnNavigateTincanAutomation.Size = new System.Drawing.Size(180, 23);
            this.btnNavigateTincanAutomation.TabIndex = 5;
            this.btnNavigateTincanAutomation.Text = "Navigate to TinCan Automation";
            this.btnNavigateTincanAutomation.UseVisualStyleBackColor = true;
            this.btnNavigateTincanAutomation.Click += new System.EventHandler(this.btnNavigateTincanAutomation_Click);
            // 
            // lblSelectEnvironment
            // 
            this.lblSelectEnvironment.AutoSize = true;
            this.lblSelectEnvironment.Location = new System.Drawing.Point(32, 68);
            this.lblSelectEnvironment.Name = "lblSelectEnvironment";
            this.lblSelectEnvironment.Size = new System.Drawing.Size(99, 13);
            this.lblSelectEnvironment.TabIndex = 6;
            this.lblSelectEnvironment.Text = "Select Environment";
            // 
            // CbEnvironmentSelect
            // 
            this.CbEnvironmentSelect.FormattingEnabled = true;
            this.CbEnvironmentSelect.Items.AddRange(new object[] {
            "Dev",
            "QA",
            "DCT",
            "Demo"});
            this.CbEnvironmentSelect.Location = new System.Drawing.Point(166, 60);
            this.CbEnvironmentSelect.Name = "CbEnvironmentSelect";
            this.CbEnvironmentSelect.Size = new System.Drawing.Size(151, 21);
            this.CbEnvironmentSelect.TabIndex = 7;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(32, 173);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(88, 13);
            this.lblStartDate.TabIndex = 8;
            this.lblStartDate.Text = "Select Start Date";
            this.lblStartDate.Click += new System.EventHandler(this.lblStartDate_Click);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(453, 173);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(85, 13);
            this.lblEndDate.TabIndex = 9;
            this.lblEndDate.Text = "Select End Date";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(166, 166);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 12;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(577, 166);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 13;
            // 
            // FrmAutomationSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 355);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.CbEnvironmentSelect);
            this.Controls.Add(this.lblSelectEnvironment);
            this.Controls.Add(this.btnNavigateTincanAutomation);
            this.Controls.Add(this.btnNavigateCmdAutomation);
            this.Controls.Add(this.lblAutomationTitleHeader);
            this.Name = "FrmAutomationSelector";
            this.Text = "AutomationSelector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAutomationTitleHeader;
        private System.Windows.Forms.Button btnNavigateCmdAutomation;
        private System.Windows.Forms.Button btnNavigateTincanAutomation;
        private System.Windows.Forms.Label lblSelectEnvironment;
        private System.Windows.Forms.ComboBox CbEnvironmentSelect;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
    }
}
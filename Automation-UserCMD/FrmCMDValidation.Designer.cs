namespace Automation_UserCMD
{
    partial class FrmCMDValidation
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
            this.UserValidationCMD = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Class = new System.Windows.Forms.Button();
            this.tbReqdInput = new System.Windows.Forms.TextBox();
            this.LblReqdInput = new System.Windows.Forms.Label();
            this.Submit = new System.Windows.Forms.Button();
            this.ClassProdMapping = new System.Windows.Forms.Button();
            this.Users = new System.Windows.Forms.Button();
            this.AssetSkillMapping = new System.Windows.Forms.Button();
            this.ContentContainer = new System.Windows.Forms.Button();
            this.ContentContainerMapping = new System.Windows.Forms.Button();
            this.btnSkill = new System.Windows.Forms.Button();
            this.lblClassFilter = new System.Windows.Forms.Label();
            this.txtClassFilter = new System.Windows.Forms.TextBox();
            this.btnContent = new System.Windows.Forms.Button();
            this.QuestionMetadata = new System.Windows.Forms.Button();
            this.btnFramework = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbheaderUser = new System.Windows.Forms.Label();
            this.lbGradeUser = new System.Windows.Forms.Label();
            this.lbStartSecUser = new System.Windows.Forms.Label();
            this.lbEndSecUser = new System.Windows.Forms.Label();
            this.tbGradeUser = new System.Windows.Forms.TextBox();
            this.tbStartSecUser = new System.Windows.Forms.TextBox();
            this.tbEndSecUser = new System.Windows.Forms.TextBox();
            this.btnSubmitUsers = new System.Windows.Forms.Button();
            this.btnOrganization = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // UserValidationCMD
            // 
            this.UserValidationCMD.Location = new System.Drawing.Point(190, 53);
            this.UserValidationCMD.Name = "UserValidationCMD";
            this.UserValidationCMD.Size = new System.Drawing.Size(115, 23);
            this.UserValidationCMD.TabIndex = 2;
            this.UserValidationCMD.Text = "UserClassEnrollment";
            this.UserValidationCMD.UseVisualStyleBackColor = true;
            this.UserValidationCMD.Click += new System.EventHandler(this.UserValidationCMD_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 202);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(867, 257);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(331, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "CMD Validations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output";
            // 
            // Class
            // 
            this.Class.Location = new System.Drawing.Point(101, 52);
            this.Class.Name = "Class";
            this.Class.Size = new System.Drawing.Size(75, 23);
            this.Class.TabIndex = 1;
            this.Class.Text = "Class";
            this.Class.UseVisualStyleBackColor = true;
            this.Class.Click += new System.EventHandler(this.Class_Click);
            // 
            // tbReqdInput
            // 
            this.tbReqdInput.Location = new System.Drawing.Point(174, 152);
            this.tbReqdInput.Name = "tbReqdInput";
            this.tbReqdInput.Size = new System.Drawing.Size(206, 20);
            this.tbReqdInput.TabIndex = 5;
            this.tbReqdInput.Visible = false;
            // 
            // LblReqdInput
            // 
            this.LblReqdInput.AutoSize = true;
            this.LblReqdInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReqdInput.Location = new System.Drawing.Point(15, 153);
            this.LblReqdInput.Name = "LblReqdInput";
            this.LblReqdInput.Size = new System.Drawing.Size(74, 13);
            this.LblReqdInput.TabIndex = 6;
            this.LblReqdInput.Text = "Placeholder";
            this.LblReqdInput.Visible = false;
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(662, 149);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(75, 23);
            this.Submit.TabIndex = 7;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Visible = false;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // ClassProdMapping
            // 
            this.ClassProdMapping.Location = new System.Drawing.Point(604, 53);
            this.ClassProdMapping.Name = "ClassProdMapping";
            this.ClassProdMapping.Size = new System.Drawing.Size(125, 23);
            this.ClassProdMapping.TabIndex = 3;
            this.ClassProdMapping.Text = "Class-Prod Mapping";
            this.ClassProdMapping.UseVisualStyleBackColor = true;
            this.ClassProdMapping.Click += new System.EventHandler(this.ClassProdMapping_Click);
            // 
            // Users
            // 
            this.Users.Location = new System.Drawing.Point(18, 52);
            this.Users.Name = "Users";
            this.Users.Size = new System.Drawing.Size(75, 23);
            this.Users.TabIndex = 0;
            this.Users.Text = "Users";
            this.Users.UseVisualStyleBackColor = true;
            this.Users.Click += new System.EventHandler(this.Users_Click);
            // 
            // AssetSkillMapping
            // 
            this.AssetSkillMapping.Location = new System.Drawing.Point(190, 94);
            this.AssetSkillMapping.Name = "AssetSkillMapping";
            this.AssetSkillMapping.Size = new System.Drawing.Size(115, 23);
            this.AssetSkillMapping.TabIndex = 4;
            this.AssetSkillMapping.Text = "Asset Skill Mapping";
            this.AssetSkillMapping.UseVisualStyleBackColor = true;
            this.AssetSkillMapping.Click += new System.EventHandler(this.AssetSkillMapping_Click);
            // 
            // ContentContainer
            // 
            this.ContentContainer.Location = new System.Drawing.Point(319, 53);
            this.ContentContainer.Name = "ContentContainer";
            this.ContentContainer.Size = new System.Drawing.Size(115, 23);
            this.ContentContainer.TabIndex = 8;
            this.ContentContainer.Text = "Content Container";
            this.ContentContainer.UseVisualStyleBackColor = true;
            this.ContentContainer.Click += new System.EventHandler(this.ContentContainer_Click);
            // 
            // ContentContainerMapping
            // 
            this.ContentContainerMapping.Location = new System.Drawing.Point(448, 53);
            this.ContentContainerMapping.Name = "ContentContainerMapping";
            this.ContentContainerMapping.Size = new System.Drawing.Size(145, 23);
            this.ContentContainerMapping.TabIndex = 9;
            this.ContentContainerMapping.Text = "Content Container Mapping";
            this.ContentContainerMapping.UseVisualStyleBackColor = true;
            this.ContentContainerMapping.Click += new System.EventHandler(this.ContentContainerMapping_Click);
            // 
            // btnSkill
            // 
            this.btnSkill.Location = new System.Drawing.Point(18, 94);
            this.btnSkill.Name = "btnSkill";
            this.btnSkill.Size = new System.Drawing.Size(75, 23);
            this.btnSkill.TabIndex = 10;
            this.btnSkill.Text = "Skill";
            this.btnSkill.UseVisualStyleBackColor = true;
            this.btnSkill.Click += new System.EventHandler(this.btnAsset_Click);
            // 
            // lblClassFilter
            // 
            this.lblClassFilter.AutoSize = true;
            this.lblClassFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClassFilter.Location = new System.Drawing.Point(398, 155);
            this.lblClassFilter.Name = "lblClassFilter";
            this.lblClassFilter.Size = new System.Drawing.Size(114, 13);
            this.lblClassFilter.TabIndex = 11;
            this.lblClassFilter.Text = "Course Name Filter";
            this.lblClassFilter.Visible = false;
            // 
            // txtClassFilter
            // 
            this.txtClassFilter.Location = new System.Drawing.Point(518, 149);
            this.txtClassFilter.Name = "txtClassFilter";
            this.txtClassFilter.Size = new System.Drawing.Size(122, 20);
            this.txtClassFilter.TabIndex = 12;
            this.txtClassFilter.Visible = false;
            // 
            // btnContent
            // 
            this.btnContent.Location = new System.Drawing.Point(101, 94);
            this.btnContent.Name = "btnContent";
            this.btnContent.Size = new System.Drawing.Size(75, 23);
            this.btnContent.TabIndex = 13;
            this.btnContent.Text = "Content";
            this.btnContent.UseVisualStyleBackColor = true;
            this.btnContent.Click += new System.EventHandler(this.btnContent_Click);
            // 
            // QuestionMetadata
            // 
            this.QuestionMetadata.Location = new System.Drawing.Point(319, 94);
            this.QuestionMetadata.Name = "QuestionMetadata";
            this.QuestionMetadata.Size = new System.Drawing.Size(115, 23);
            this.QuestionMetadata.TabIndex = 14;
            this.QuestionMetadata.Text = "Question Metadata";
            this.QuestionMetadata.UseVisualStyleBackColor = true;
            this.QuestionMetadata.Click += new System.EventHandler(this.QuestionMetadata_Click);
            // 
            // btnFramework
            // 
            this.btnFramework.Location = new System.Drawing.Point(744, 53);
            this.btnFramework.Name = "btnFramework";
            this.btnFramework.Size = new System.Drawing.Size(136, 23);
            this.btnFramework.TabIndex = 15;
            this.btnFramework.Text = "Framework";
            this.btnFramework.UseVisualStyleBackColor = true;
            this.btnFramework.Click += new System.EventHandler(this.btnFramework_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(756, 13);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(94, 13);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Tincan Validations";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Automation_UserCMD.Properties.Resources.imgHome;
            this.pictureBox1.Location = new System.Drawing.Point(21, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lbheaderUser
            // 
            this.lbheaderUser.AutoSize = true;
            this.lbheaderUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbheaderUser.Location = new System.Drawing.Point(12, 130);
            this.lbheaderUser.Name = "lbheaderUser";
            this.lbheaderUser.Size = new System.Drawing.Size(148, 13);
            this.lbheaderUser.TabIndex = 18;
            this.lbheaderUser.Text = "Please select filter range";
            this.lbheaderUser.Visible = false;
            // 
            // lbGradeUser
            // 
            this.lbGradeUser.AutoSize = true;
            this.lbGradeUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGradeUser.Location = new System.Drawing.Point(187, 130);
            this.lbGradeUser.Name = "lbGradeUser";
            this.lbGradeUser.Size = new System.Drawing.Size(41, 13);
            this.lbGradeUser.TabIndex = 19;
            this.lbGradeUser.Text = "Grade";
            this.lbGradeUser.Visible = false;
            // 
            // lbStartSecUser
            // 
            this.lbStartSecUser.AutoSize = true;
            this.lbStartSecUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStartSecUser.Location = new System.Drawing.Point(353, 129);
            this.lbStartSecUser.Name = "lbStartSecUser";
            this.lbStartSecUser.Size = new System.Drawing.Size(81, 13);
            this.lbStartSecUser.TabIndex = 20;
            this.lbStartSecUser.Text = "Start Section";
            this.lbStartSecUser.Visible = false;
            // 
            // lbEndSecUser
            // 
            this.lbEndSecUser.AutoSize = true;
            this.lbEndSecUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEndSecUser.Location = new System.Drawing.Point(564, 126);
            this.lbEndSecUser.Name = "lbEndSecUser";
            this.lbEndSecUser.Size = new System.Drawing.Size(76, 13);
            this.lbEndSecUser.TabIndex = 21;
            this.lbEndSecUser.Text = "End Section";
            this.lbEndSecUser.Visible = false;
            // 
            // tbGradeUser
            // 
            this.tbGradeUser.Location = new System.Drawing.Point(235, 127);
            this.tbGradeUser.Name = "tbGradeUser";
            this.tbGradeUser.Size = new System.Drawing.Size(70, 20);
            this.tbGradeUser.TabIndex = 22;
            this.tbGradeUser.Visible = false;
            // 
            // tbStartSecUser
            // 
            this.tbStartSecUser.Location = new System.Drawing.Point(448, 122);
            this.tbStartSecUser.Name = "tbStartSecUser";
            this.tbStartSecUser.Size = new System.Drawing.Size(73, 20);
            this.tbStartSecUser.TabIndex = 23;
            this.tbStartSecUser.Visible = false;
            // 
            // tbEndSecUser
            // 
            this.tbEndSecUser.Location = new System.Drawing.Point(662, 123);
            this.tbEndSecUser.Name = "tbEndSecUser";
            this.tbEndSecUser.Size = new System.Drawing.Size(67, 20);
            this.tbEndSecUser.TabIndex = 24;
            this.tbEndSecUser.Visible = false;
            // 
            // btnSubmitUsers
            // 
            this.btnSubmitUsers.Location = new System.Drawing.Point(759, 119);
            this.btnSubmitUsers.Name = "btnSubmitUsers";
            this.btnSubmitUsers.Size = new System.Drawing.Size(75, 23);
            this.btnSubmitUsers.TabIndex = 25;
            this.btnSubmitUsers.Text = "Submit";
            this.btnSubmitUsers.UseVisualStyleBackColor = true;
            this.btnSubmitUsers.Visible = false;
            this.btnSubmitUsers.Click += new System.EventHandler(this.btnSubmitUsers_Click);
            // 
            // btnOrganization
            // 
            this.btnOrganization.Location = new System.Drawing.Point(448, 94);
            this.btnOrganization.Name = "btnOrganization";
            this.btnOrganization.Size = new System.Drawing.Size(75, 23);
            this.btnOrganization.TabIndex = 26;
            this.btnOrganization.Text = "Organization";
            this.btnOrganization.UseVisualStyleBackColor = true;
            this.btnOrganization.Click += new System.EventHandler(this.btnOrganization_Click);
            // 
            // FrmCMDValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 480);
            this.Controls.Add(this.btnOrganization);
            this.Controls.Add(this.btnSubmitUsers);
            this.Controls.Add(this.tbEndSecUser);
            this.Controls.Add(this.tbStartSecUser);
            this.Controls.Add(this.tbGradeUser);
            this.Controls.Add(this.lbEndSecUser);
            this.Controls.Add(this.lbStartSecUser);
            this.Controls.Add(this.lbGradeUser);
            this.Controls.Add(this.lbheaderUser);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnFramework);
            this.Controls.Add(this.QuestionMetadata);
            this.Controls.Add(this.btnContent);
            this.Controls.Add(this.txtClassFilter);
            this.Controls.Add(this.lblClassFilter);
            this.Controls.Add(this.btnSkill);
            this.Controls.Add(this.ContentContainerMapping);
            this.Controls.Add(this.ContentContainer);
            this.Controls.Add(this.AssetSkillMapping);
            this.Controls.Add(this.Users);
            this.Controls.Add(this.ClassProdMapping);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.LblReqdInput);
            this.Controls.Add(this.tbReqdInput);
            this.Controls.Add(this.Class);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.UserValidationCMD);
            this.Name = "FrmCMDValidation";
            this.Text = "CMD Validations";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UserValidationCMD;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Class;
        private System.Windows.Forms.TextBox tbReqdInput;
        private System.Windows.Forms.Label LblReqdInput;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Button ClassProdMapping;
        private System.Windows.Forms.Button Users;
        private System.Windows.Forms.Button AssetSkillMapping;
        private System.Windows.Forms.Button ContentContainer;
        private System.Windows.Forms.Button ContentContainerMapping;
        private System.Windows.Forms.Button btnSkill;
        private System.Windows.Forms.Label lblClassFilter;
        private System.Windows.Forms.TextBox txtClassFilter;
        private System.Windows.Forms.Button btnContent;
        private System.Windows.Forms.Button QuestionMetadata;
        private System.Windows.Forms.Button btnFramework;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbheaderUser;
        private System.Windows.Forms.Label lbGradeUser;
        private System.Windows.Forms.Label lbStartSecUser;
        private System.Windows.Forms.Label lbEndSecUser;
        private System.Windows.Forms.TextBox tbGradeUser;
        private System.Windows.Forms.TextBox tbStartSecUser;
        private System.Windows.Forms.TextBox tbEndSecUser;
        private System.Windows.Forms.Button btnSubmitUsers;
        private System.Windows.Forms.Button btnOrganization;
    }
}


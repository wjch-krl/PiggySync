namespace PiggySync.WinApp
{
    partial class SettingsForm
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
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.useTcp = new System.Windows.Forms.CheckBox();
            this.useEncryption = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.selectPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.computerNameTextBox = new System.Windows.Forms.TextBox();
            this.deletedUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.bannedFiles = new System.Windows.Forms.TextBox();
            this.textFiles = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.folderSelector = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.deletedUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(12, 76);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(240, 20);
            this.pathTextBox.TabIndex = 0;
            // 
            // useTcp
            // 
            this.useTcp.AutoSize = true;
            this.useTcp.Location = new System.Drawing.Point(12, 12);
            this.useTcp.Name = "useTcp";
            this.useTcp.Size = new System.Drawing.Size(69, 17);
            this.useTcp.TabIndex = 2;
            this.useTcp.Text = "Use TCP";
            this.useTcp.UseVisualStyleBackColor = true;
            // 
            // useEncryption
            // 
            this.useEncryption.AutoSize = true;
            this.useEncryption.Location = new System.Drawing.Point(13, 36);
            this.useEncryption.Name = "useEncryption";
            this.useEncryption.Size = new System.Drawing.Size(112, 17);
            this.useEncryption.TabIndex = 3;
            this.useEncryption.Text = "Enable Encryption";
            this.useEncryption.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sync root path:";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(235, 440);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(154, 440);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // selectPath
            // 
            this.selectPath.Location = new System.Drawing.Point(258, 74);
            this.selectPath.Name = "selectPath";
            this.selectPath.Size = new System.Drawing.Size(52, 23);
            this.selectPath.TabIndex = 7;
            this.selectPath.Text = "Select";
            this.selectPath.UseVisualStyleBackColor = true;
            this.selectPath.Click += new System.EventHandler(this.selectPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Computer Name:";
            // 
            // computerNameTextBox
            // 
            this.computerNameTextBox.Location = new System.Drawing.Point(12, 115);
            this.computerNameTextBox.Name = "computerNameTextBox";
            this.computerNameTextBox.Size = new System.Drawing.Size(300, 20);
            this.computerNameTextBox.TabIndex = 9;
            // 
            // deletedUpDown
            // 
            this.deletedUpDown.Location = new System.Drawing.Point(12, 154);
            this.deletedUpDown.Name = "deletedUpDown";
            this.deletedUpDown.Size = new System.Drawing.Size(300, 20);
            this.deletedUpDown.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Keep deleted files info for:";
            // 
            // bannedFiles
            // 
            this.bannedFiles.Location = new System.Drawing.Point(11, 193);
            this.bannedFiles.Multiline = true;
            this.bannedFiles.Name = "bannedFiles";
            this.bannedFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.bannedFiles.Size = new System.Drawing.Size(301, 106);
            this.bannedFiles.TabIndex = 12;
            // 
            // textFiles
            // 
            this.textFiles.Location = new System.Drawing.Point(10, 318);
            this.textFiles.Multiline = true;
            this.textFiles.Name = "textFiles";
            this.textFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textFiles.Size = new System.Drawing.Size(300, 106);
            this.textFiles.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Synchronization exclude files:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 302);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Text files:";
            // 
            // folderSelector
            // 
            this.folderSelector.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 480);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textFiles);
            this.Controls.Add(this.bannedFiles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.deletedUpDown);
            this.Controls.Add(this.computerNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectPath);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.useEncryption);
            this.Controls.Add(this.useTcp);
            this.Controls.Add(this.pathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            ((System.ComponentModel.ISupportInitialize)(this.deletedUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.CheckBox useTcp;
        private System.Windows.Forms.CheckBox useEncryption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button selectPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox computerNameTextBox;
        private System.Windows.Forms.NumericUpDown deletedUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox bannedFiles;
        private System.Windows.Forms.TextBox textFiles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FolderBrowserDialog folderSelector;
    }
}
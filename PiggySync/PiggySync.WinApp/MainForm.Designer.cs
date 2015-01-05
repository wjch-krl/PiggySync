namespace PiggySync.WinApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.settingsButton = new System.Windows.Forms.Button();
            this.hostsButton = new System.Windows.Forms.Button();
            this.s = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.s)).BeginInit();
            this.SuspendLayout();
            // 
            // statusTextBox
            // 
            this.statusTextBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.statusTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusTextBox.Location = new System.Drawing.Point(12, 272);
            this.statusTextBox.Multiline = true;
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(246, 51);
            this.statusTextBox.TabIndex = 3;
            this.statusTextBox.Text = "Sync Status:\r\nSynchronizing";
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(12, 349);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(110, 23);
            this.settingsButton.TabIndex = 4;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // hostsButton
            // 
            this.hostsButton.Location = new System.Drawing.Point(148, 349);
            this.hostsButton.Name = "hostsButton";
            this.hostsButton.Size = new System.Drawing.Size(110, 23);
            this.hostsButton.TabIndex = 5;
            this.hostsButton.Text = "View Hosts";
            this.hostsButton.UseVisualStyleBackColor = true;
            this.hostsButton.Click += new System.EventHandler(this.hostsButton_Click);
            // 
            // s
            // 
            this.s.Image = global::PiggySync.WinApp.Properties.Resources.PiggyLogo_1024;
            this.s.Location = new System.Drawing.Point(12, 12);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(240, 240);
            this.s.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.s.TabIndex = 0;
            this.s.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 329);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(246, 12);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Value = 60;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 381);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.hostsButton);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.s);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PiggSync!";
            ((System.ComponentModel.ISupportInitialize)(this.s)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox s;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button hostsButton;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}


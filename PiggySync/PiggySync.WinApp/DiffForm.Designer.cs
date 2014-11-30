namespace PiggySync.WinApp
{
    partial class DiffForm
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
            this.localFileTextBox = new System.Windows.Forms.RichTextBox();
            this.remoteFileTextBox = new System.Windows.Forms.RichTextBox();
            this.resultFileTextBox = new System.Windows.Forms.RichTextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.localFileLabel = new System.Windows.Forms.Label();
            this.remoteFileLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // localFileTextBox
            // 
            this.localFileTextBox.Location = new System.Drawing.Point(-1, 0);
            this.localFileTextBox.Name = "localFileTextBox";
            this.localFileTextBox.Size = new System.Drawing.Size(310, 404);
            this.localFileTextBox.TabIndex = 0;
            this.localFileTextBox.Text = "";
            // 
            // remoteFileTextBox
            // 
            this.remoteFileTextBox.Location = new System.Drawing.Point(309, 0);
            this.remoteFileTextBox.Name = "remoteFileTextBox";
            this.remoteFileTextBox.Size = new System.Drawing.Size(310, 404);
            this.remoteFileTextBox.TabIndex = 1;
            this.remoteFileTextBox.Text = "";
            // 
            // resultFileTextBox
            // 
            this.resultFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultFileTextBox.Location = new System.Drawing.Point(619, 0);
            this.resultFileTextBox.Name = "resultFileTextBox";
            this.resultFileTextBox.Size = new System.Drawing.Size(310, 404);
            this.resultFileTextBox.TabIndex = 2;
            this.resultFileTextBox.Text = "";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(843, 414);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // localFileLabel
            // 
            this.localFileLabel.AutoSize = true;
            this.localFileLabel.Location = new System.Drawing.Point(12, 414);
            this.localFileLabel.Name = "localFileLabel";
            this.localFileLabel.Size = new System.Drawing.Size(157, 13);
            this.localFileLabel.TabIndex = 4;
            this.localFileLabel.Text = "Local File: 25-09-2014 13:42:22";
            // 
            // remoteFileLabel
            // 
            this.remoteFileLabel.AutoSize = true;
            this.remoteFileLabel.Location = new System.Drawing.Point(324, 414);
            this.remoteFileLabel.Name = "remoteFileLabel";
            this.remoteFileLabel.Size = new System.Drawing.Size(168, 13);
            this.remoteFileLabel.TabIndex = 5;
            this.remoteFileLabel.Text = "Remote File: 25-09-2014 13:42:22";
            // 
            // DiffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 449);
            this.Controls.Add(this.remoteFileLabel);
            this.Controls.Add(this.localFileLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.resultFileTextBox);
            this.Controls.Add(this.remoteFileTextBox);
            this.Controls.Add(this.localFileTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DiffForm";
            this.Text = "DiffForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox localFileTextBox;
        private System.Windows.Forms.RichTextBox remoteFileTextBox;
        private System.Windows.Forms.RichTextBox resultFileTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label localFileLabel;
        private System.Windows.Forms.Label remoteFileLabel;
    }
}
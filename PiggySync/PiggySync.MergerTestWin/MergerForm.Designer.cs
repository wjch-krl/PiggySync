namespace PiggySync.MergerTestWin
{
    partial class MergerForm
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
            this.fileAButton = new System.Windows.Forms.Button();
            this.fileBButton = new System.Windows.Forms.Button();
            this.mergeButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // fileAButton
            // 
            this.fileAButton.Location = new System.Drawing.Point(12, 12);
            this.fileAButton.Name = "fileAButton";
            this.fileAButton.Size = new System.Drawing.Size(75, 23);
            this.fileAButton.TabIndex = 0;
            this.fileAButton.Text = "Select file A";
            this.fileAButton.UseVisualStyleBackColor = true;
            this.fileAButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // fileBButton
            // 
            this.fileBButton.Location = new System.Drawing.Point(93, 12);
            this.fileBButton.Name = "fileBButton";
            this.fileBButton.Size = new System.Drawing.Size(75, 23);
            this.fileBButton.TabIndex = 1;
            this.fileBButton.Text = "Select file B";
            this.fileBButton.UseVisualStyleBackColor = true;
            this.fileBButton.Click += new System.EventHandler(this.fileBButton_Click);
            // 
            // mergeButton
            // 
            this.mergeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mergeButton.Location = new System.Drawing.Point(472, 12);
            this.mergeButton.Name = "mergeButton";
            this.mergeButton.Size = new System.Drawing.Size(75, 23);
            this.mergeButton.TabIndex = 2;
            this.mergeButton.Text = "Merge";
            this.mergeButton.UseVisualStyleBackColor = true;
            this.mergeButton.Click += new System.EventHandler(this.mergeButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(12, 50);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(535, 453);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "Add files and click merge to see merged file";
            // 
            // MergerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 515);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.mergeButton);
            this.Controls.Add(this.fileBButton);
            this.Controls.Add(this.fileAButton);
            this.Name = "MergerForm";
            this.Text = "Merger";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button fileAButton;
        private System.Windows.Forms.Button fileBButton;
        private System.Windows.Forms.Button mergeButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}


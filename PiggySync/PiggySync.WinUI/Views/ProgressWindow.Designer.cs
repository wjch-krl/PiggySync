namespace PiggySyncWin.WinUI.Views
{
    partial class ProgressWindow
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
            this.buttonPrzerwijButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelTrwaSynchro = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonPrzerwijButton
            // 
            this.buttonPrzerwijButton.BackColor = System.Drawing.Color.GreenYellow;
            this.buttonPrzerwijButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonPrzerwijButton.FlatAppearance.BorderColor = System.Drawing.Color.Chartreuse;
            this.buttonPrzerwijButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonPrzerwijButton.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonPrzerwijButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonPrzerwijButton.Location = new System.Drawing.Point(100, 133);
            this.buttonPrzerwijButton.Name = "buttonPrzerwijButton";
            this.buttonPrzerwijButton.Size = new System.Drawing.Size(156, 38);
            this.buttonPrzerwijButton.TabIndex = 1;
            this.buttonPrzerwijButton.Text = "Przerwij";
            this.buttonPrzerwijButton.UseVisualStyleBackColor = false;
            this.buttonPrzerwijButton.Click += new System.EventHandler(this.stopButtonClick);
            // 
            // progressBarPasekPostepu
            // 
            this.progressBar1.Location = new System.Drawing.Point(30, 61);
            this.progressBar1.Name = "progressBarPasekPostepu";
            this.progressBar1.Size = new System.Drawing.Size(297, 32);
            this.progressBar1.TabIndex = 11;
            // 
            // labelTrwaSynchro
            // 
            this.labelTrwaSynchro.AutoSize = true;
            this.labelTrwaSynchro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTrwaSynchro.Location = new System.Drawing.Point(93, 27);
            this.labelTrwaSynchro.Name = "labelTrwaSynchro";
            this.labelTrwaSynchro.Size = new System.Drawing.Size(163, 20);
            this.labelTrwaSynchro.TabIndex = 12;
            this.labelTrwaSynchro.Text = "Trwa synchronizacja...";
            // 
            // OknoPostepu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(357, 183);
            this.Controls.Add(this.labelTrwaSynchro);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonPrzerwijButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OknoPostepu";
            this.Text = "Synchronizacja plików";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPrzerwijButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelTrwaSynchro;
    }
}
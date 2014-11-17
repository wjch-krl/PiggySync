namespace PiggySync.WinApp
{
    partial class HostsViewForm
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
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "adsdas",
            "192.168.1.1",
            "12-10-2013"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "acascasdas",
            "192.168.1.2",
            "14-10-2013"}, -1);
            this.listView1 = new System.Windows.Forms.ListView();
            this.hostNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hostIpHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastSyncHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hostNameHeader,
            this.hostIpHeader,
            this.lastSyncHeader});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(390, 295);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // hostNameHeader
            // 
            this.hostNameHeader.Text = "Host Name";
            this.hostNameHeader.Width = 189;
            // 
            // hostIpHeader
            // 
            this.hostIpHeader.Text = "IP Adress";
            this.hostIpHeader.Width = 101;
            // 
            // lastSyncHeader
            // 
            this.lastSyncHeader.Text = "Last Sync Date";
            this.lastSyncHeader.Width = 114;
            // 
            // HostsViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 295);
            this.Controls.Add(this.listView1);
            this.Name = "HostsViewForm";
            this.Text = "HostsViewForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader hostNameHeader;
        private System.Windows.Forms.ColumnHeader hostIpHeader;
        private System.Windows.Forms.ColumnHeader lastSyncHeader;

    }
}
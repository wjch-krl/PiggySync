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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "adsdas",
            "192.168.1.1",
            "12-10-2013"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "acascasdas",
            "192.168.1.2",
            "14-10-2013"}, -1);
            this.hostsListView = new System.Windows.Forms.ListView();
            this.hostNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hostIpHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastSyncHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // hostsListView
            // 
            this.hostsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hostNameHeader,
            this.hostIpHeader,
            this.lastSyncHeader});
            this.hostsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hostsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.hostsListView.Location = new System.Drawing.Point(0, 0);
            this.hostsListView.Name = "hostsListView";
            this.hostsListView.Size = new System.Drawing.Size(390, 295);
            this.hostsListView.TabIndex = 1;
            this.hostsListView.UseCompatibleStateImageBehavior = false;
            this.hostsListView.View = System.Windows.Forms.View.Details;
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
            this.Controls.Add(this.hostsListView);
            this.Name = "HostsViewForm";
            this.Text = "HostsViewForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HostsViewForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView hostsListView;
        private System.Windows.Forms.ColumnHeader hostNameHeader;
        private System.Windows.Forms.ColumnHeader hostIpHeader;
        private System.Windows.Forms.ColumnHeader lastSyncHeader;

    }
}
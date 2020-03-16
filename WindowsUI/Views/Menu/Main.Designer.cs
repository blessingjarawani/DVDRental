namespace WindowsUI
{
    partial class Main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClientsDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.moviesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMoviesDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.renatlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOverDue = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStats = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientsToolStripMenuItem,
            this.moviesToolStripMenuItem,
            this.renatlsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(555, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clientsToolStripMenuItem
            // 
            this.clientsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClientsDirectory});
            this.clientsToolStripMenuItem.Name = "clientsToolStripMenuItem";
            this.clientsToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.clientsToolStripMenuItem.Text = "Clients";
            // 
            // mnuClientsDirectory
            // 
            this.mnuClientsDirectory.Name = "mnuClientsDirectory";
            this.mnuClientsDirectory.Size = new System.Drawing.Size(201, 26);
            this.mnuClientsDirectory.Text = "Clients Directory";
            this.mnuClientsDirectory.Click += new System.EventHandler(this.mnuClientsDirectory_Click);
            // 
            // moviesToolStripMenuItem
            // 
            this.moviesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMoviesDirectory});
            this.moviesToolStripMenuItem.Name = "moviesToolStripMenuItem";
            this.moviesToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.moviesToolStripMenuItem.Text = "Movies";
            // 
            // mnuMoviesDirectory
            // 
            this.mnuMoviesDirectory.Name = "mnuMoviesDirectory";
            this.mnuMoviesDirectory.Size = new System.Drawing.Size(204, 26);
            this.mnuMoviesDirectory.Text = "Movies Directory";
            this.mnuMoviesDirectory.Click += new System.EventHandler(this.mnuMoviesDirectory_Click);
            // 
            // renatlsToolStripMenuItem
            // 
            this.renatlsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOverDue,
            this.mnuStats});
            this.renatlsToolStripMenuItem.Name = "renatlsToolStripMenuItem";
            this.renatlsToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.renatlsToolStripMenuItem.Text = "Rentals";
            // 
            // mnuOverDue
            // 
            this.mnuOverDue.Name = "mnuOverDue";
            this.mnuOverDue.Size = new System.Drawing.Size(224, 26);
            this.mnuOverDue.Text = "OverDue";
            this.mnuOverDue.Click += new System.EventHandler(this.mnuOverDue_Click);
            // 
            // mnuStats
            // 
            this.mnuStats.Name = "mnuStats";
            this.mnuStats.Size = new System.Drawing.Size(224, 26);
            this.mnuStats.Text = "Statistics";
            this.mnuStats.Click += new System.EventHandler(this.mnuStats_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 358);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DV Rental";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem moviesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renatlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuMoviesDirectory;
        private System.Windows.Forms.ToolStripMenuItem mnuClientsDirectory;
        private System.Windows.Forms.ToolStripMenuItem mnuOverDue;
        private System.Windows.Forms.ToolStripMenuItem mnuStats;
    }
}


namespace Ayubo_Leisure_sys
{
    partial class view_Booking_Data
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rentBookingDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.longHireBookingDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dayHireBookingDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(615, 312);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(615, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rentBookingDataToolStripMenuItem,
            this.longHireBookingDataToolStripMenuItem,
            this.dayHireBookingDataToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // rentBookingDataToolStripMenuItem
            // 
            this.rentBookingDataToolStripMenuItem.Name = "rentBookingDataToolStripMenuItem";
            this.rentBookingDataToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.rentBookingDataToolStripMenuItem.Text = "Rent Booking Data";
            this.rentBookingDataToolStripMenuItem.Click += new System.EventHandler(this.rentBookingDataToolStripMenuItem_Click);
            // 
            // longHireBookingDataToolStripMenuItem
            // 
            this.longHireBookingDataToolStripMenuItem.Name = "longHireBookingDataToolStripMenuItem";
            this.longHireBookingDataToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.longHireBookingDataToolStripMenuItem.Text = "Long Hire Booking Data";
            this.longHireBookingDataToolStripMenuItem.Click += new System.EventHandler(this.longHireBookingDataToolStripMenuItem_Click);
            // 
            // dayHireBookingDataToolStripMenuItem
            // 
            this.dayHireBookingDataToolStripMenuItem.Name = "dayHireBookingDataToolStripMenuItem";
            this.dayHireBookingDataToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.dayHireBookingDataToolStripMenuItem.Text = "Day Hire Booking Data";
            this.dayHireBookingDataToolStripMenuItem.Click += new System.EventHandler(this.dayHireBookingDataToolStripMenuItem_Click);
            // 
            // view_Booking_Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 336);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "view_Booking_Data";
            this.Text = "View Booking Data";
            this.Load += new System.EventHandler(this.view_Booking_Data_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rentBookingDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem longHireBookingDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dayHireBookingDataToolStripMenuItem;
    }
}
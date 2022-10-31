using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Ayubo_Leisure_sys
{
    public partial class view_Booking_Data : Form
    {
        Database_Controller crtl = new Database_Controller();
      
        public view_Booking_Data()
        {
            InitializeComponent();
        }

        private void view_Booking_Data_Load(object sender, EventArgs e)
        {

        }

        private void rentBookingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            DataSet ds = new DataSet();
            this.Text = "Booking Data: Rent Booking Data";
            String rentviewSql = "select * from rent_book ";
            SqlCommand cmd = new SqlCommand(rentviewSql, Database_Controller.connection());
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void longHireBookingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Booking Data: Long Hiring Booking Data";
            DataSet ds = new DataSet();
            String rentviewSql = "select * from long_hire_book ";
            SqlCommand cmd = new SqlCommand(rentviewSql, Database_Controller.connection());
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dayHireBookingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            this.Text = "Booking Data: Day Hiring Booking Data";
            String rentviewSql = "select * from day_hire_book ";
            SqlCommand cmd = new SqlCommand(rentviewSql, Database_Controller.connection());
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}

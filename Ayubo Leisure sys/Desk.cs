using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ayubo_Leisure_sys
{
    public partial class Desk : Form
    {
        public Desk()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form rent = new Long_hire();
            rent.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form rent = new Rent_Form();
            rent.Show();
        }

        private void Desk_Load(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form days_hire = new Day_Hire_Form();
            days_hire.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void aBOUTToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Close();
            //Ay_Formula.long_hour(true,4,6,205);
        }

        private void Desk_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void Desk_FormClosed(object sender, FormClosedEventArgs e)
        {
          
            
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
          
        }

        private void button3_Click_3(object sender, EventArgs e)
        {
            Form da = new view_Booking_Data();
            da.Show();
        }
    }
}

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
    public partial class Login_form : Form
    {
        public Login_form()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool login = Database_Controller.user_login(user_txt.Text, pass.Text);
           
            if (login == true) {

             
                
                this.Hide();
                Form drive_form = new Desk();
                drive_form.ShowDialog();
                if (drive_form == null)
                {
                    
                    this.Show();
                    user_txt.Text = "";
                    pass.Text = "";
                }
                else {
                    this.Close();
                }
               
              
            } else if 
                (login == false) {
                  
                MessageBox.Show("Invalid User Login","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void login_form_Load(object sender, EventArgs e)
        {

        }
    }
}

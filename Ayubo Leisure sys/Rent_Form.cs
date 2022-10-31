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
    public partial class Rent_Form : Form
    {
        public Rent_Form()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            List<String> vech_type = Database_Controller.get_all_vechcal_type();

            foreach (String i in vech_type)
            {

                comboBox1.Items.Add(i);

            }
  
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) { MessageBox.Show("Please Select Vechical type ", "error"); }
            else
            {
                TimeSpan days_span = dateTimePicker2.Value - start.Value;
                int days = days_span.Days;

                Console.WriteLine(days);
                float rent_sum = Ay_Formula.rent_counter(days, checkBox1.Checked, comboBox1.SelectedItem.ToString());
                Console.WriteLine(rent_sum);
                sum_lbl.Text = rent_sum.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
             if (comboBox1.SelectedItem == null) { MessageBox.Show("Please Select Vechical type ", "error"); } else if (comboBox2.SelectedItem == null) { MessageBox.Show("Please Select Vechical no ", "error"); }
             else
            if (cus_name.Text.Length== 0) { MessageBox.Show("Customer name Not inserted", "Error"); }
            else
                if (mobile_no.Text.Length == 0) { MessageBox.Show("Mobile number Not inserted", "Error"); }
                else if (nic.Text.Length==0) { MessageBox.Show("NIC number Not inserted", "Error"); }
                else if (cus_name.Text.Length ==0 && nic.Text.Length == 0 && mobile_no.Text.Length == 0)
                {
                    MessageBox.Show("Please fill the Customers Details", "Error");
                }

                else
                {

                    TimeSpan days_span = dateTimePicker2.Value - start.Value;
                    int days = days_span.Days;

                    Console.WriteLine(days);
                   
                    float rent_sum = Ay_Formula.rent_counter(days, checkBox1.Checked, comboBox1.SelectedItem.ToString());
                    Console.WriteLine(rent_sum);
                    sum_lbl.Text = rent_sum.ToString();
                       long c_id=Ay_Formula.cusmomer_id_gena();
                    bool insert = Database_Controller.rent_book_insert(c_id, cus_name.Text,
                             nic.Text, mobile_no.Text, start.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString(),
                             rent_sum.ToString(), checkBox1.Checked, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString()
                             );
                    if (insert == false) { MessageBox.Show("Smothing Wrong Please Check the Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } else if (insert == true) {
                        progressBar1.Value = 100;
                        MessageBox.Show("Sucessfully inserted", "Done"); 
                        id.Text = c_id.ToString(); }
                    progressBar1.Value = 0;

                }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(comboBox1.SelectedItem.ToString());
            comboBox2.Text = "";
            comboBox2.Items.Clear();
            List<String> vech_no = Database_Controller.rent_get_vechcal_no(comboBox1.SelectedItem.ToString());

            foreach (String i in vech_no)
            {

                comboBox2.Items.Add(i);

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (cus_name.Text.Length == 0) { MessageBox.Show("Customer name Not inserted", "Error"); }
            else
                if (mobile_no.Text.Length == 0) { MessageBox.Show("Mobile number Not inserted", "Error"); }
                else if (nic.Text.Length == 0) { MessageBox.Show("NIC number Not inserted", "Error"); }
                else if (cus_name.Text.Length == 0 && nic.Text.Length == 0 && mobile_no.Text.Length == 0)
                {
                    MessageBox.Show("Please fill the Customers Details", "Error");
                }
                else
                {
                    TimeSpan days_span = dateTimePicker2.Value - start.Value;
                    int days = days_span.Days;

                    Console.WriteLine(days);

                    float rent_sum = Ay_Formula.rent_counter(days, checkBox1.Checked, comboBox1.SelectedItem.ToString());
                    sum_lbl.Text = rent_sum.ToString();
               bool rent_up=     Database_Controller.rent_book_update(long.Parse(id.Text), cus_name.Text,
                                     nic.Text, mobile_no.Text, start.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString(),
                                     rent_sum.ToString(), checkBox1.Checked, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString());

               if (rent_up == true) { progressBar1.Value = 100; 
                   MessageBox.Show("Update Sucessfully", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   progressBar1.Value = 0;
               } else if (rent_up == false) { MessageBox.Show("Something wrong please check the values", "Error"); }
                }
                }

        private void button4_Click(object sender, EventArgs e)
        {
            if (id.Text.Length == 0) { MessageBox.Show("Please insert the id", "info", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            {
                List<String> get_details = Database_Controller.get_rentbook_details(id.Text);

                if (get_details==null) { MessageBox.Show("Please insert the correct id", "info", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else
                {
                    cus_name.Text = get_details[0];
                    mobile_no.Text = get_details[1];
                    nic.Text = get_details[2];
                    start.Value = DateTime.Parse(get_details[3]);
                    dateTimePicker2.Value = DateTime.Parse(get_details[4]);
                    checkBox1.Checked = bool.Parse(get_details[5]);
                    comboBox1.Text = get_details[6];
                    comboBox2.Text = get_details[7];
                    sum_lbl.Text = get_details[8];
                }

            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id.Text.Length == 0) { MessageBox.Show("Please insert the id", "info", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            {
                bool del = Database_Controller.rent_book_delete(id.Text);
                if (del == false) { MessageBox.Show("Please insert Correct id", "info", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else if (del == true) { progressBar1.Value = 100;
                MessageBox.Show("" + id.Text + " Sucessfully deleted", "info", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    progressBar1.Value = 0;
                }
                
                }
            }
        }



    }


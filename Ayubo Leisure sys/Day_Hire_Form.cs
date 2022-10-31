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
    public partial class Day_Hire_Form : Form
    {

        public Day_Hire_Form()
        {
            InitializeComponent();
            pack_ty.Hide();

        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (validation() == true)
            {

                cal_all();
                long id = Ay_Formula.cusmomer_id_gena();
                id_no.Text = id.ToString();
                String start_time_value = start_time_picker.Value.ToShortTimeString();
                float extra_kmCharge = float.Parse(waiting_lbl.Text);
                String end_times = end_time_picker.Value.ToShortTimeString();
                String total_sum = sum.Text.ToString();
                float totalKM = float.Parse(total_km.Text);
                //
                float base_charge = float.Parse(base_char_lbl.Text.ToString());

                if (checkBox1.Checked == true)
                {




                    bool insert = Database_Controller.day_hire_book_insert(id, cus_name.Text, nic.Text, mobile_no.Text, start_time_value, end_times,
                        total_sum, checkBox1.Checked,
                        vh_ty_cb.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(),
                        pack_ty.SelectedItem.ToString(), totalKM, base_charge, float.Parse(waiting_lbl.Text.ToString()),
                         float.Parse(ex_km_lbl.Text.ToString()));
                    if (insert == true)
                    {
                        progressBar1.Value = 100;
                        MessageBox.Show("Sucessfully inserted");
                        progressBar1.Value = 0;
                    }
                    else if (insert == false)
                    {
                        MessageBox.Show("Not inserted");
                    }
                }
                else
                    if (checkBox1.Checked == false)
                    {
                        bool insert = Database_Controller.day_hire_book_insert(id, cus_name.Text, nic.Text,
                            mobile_no.Text, start_time_value, end_times, total_sum,
                            checkBox1.Checked, vh_ty_cb.SelectedItem.ToString(),
                            comboBox2.SelectedItem.ToString(), "", totalKM, base_charge, float.Parse(waiting_lbl.Text.ToString()),
                         float.Parse(ex_km_lbl.Text.ToString()));
                        if (insert == true)
                        {
                            MessageBox.Show("Sucessfully inserted");
                        }
                        else if (insert == false)
                        {
                            MessageBox.Show("Not inserted");
                        }

                    }
            }
            else if (validation() == false)
            {

                Console.WriteLine("Validation error");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cal_all();

        }
        public bool validation()
        {
            if (vh_ty_cb.SelectedItem == null)
            {
                MessageBox.Show("Please Select Vehicle type ", "error");
                return false;
            }
            else if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please Select Vehicle no ", "error");

                return false;
            }
            else
                if (cus_name.Text.Length == 0)
                {
                    MessageBox.Show("Customer name Not inserted", "Error");

                    return false;
                }
                else
                    if (mobile_no.Text.Length == 0)
                    {
                        MessageBox.Show("Mobile number Not inserted", "Error");
                        return false;
                    }
                    else if (nic.Text.Length == 0)
                    {
                        MessageBox.Show("NIC number Not inserted", "Error");
                        return false;
                    }
                    else if (cus_name.Text.Length == 0 && nic.Text.Length == 0 && mobile_no.Text.Length == 0)
                    {
                        MessageBox.Show("Please fill the Customers Details", "Error");
                        return false;
                    }
            return true;

        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (vh_ty_cb.SelectedItem == null) { MessageBox.Show("Select the Vechical type", "error"); checkBox1.Checked = false; }
            else
            {
                if (checkBox1.Checked == true)
                {
                    pack_ty.Show();
                    pack_ty.Items.Clear();
                    foreach (String packs in Database_Controller.get_all_day_package_name(vh_ty_cb.SelectedItem.ToString()))
                    {
                        Console.WriteLine(packs);
                        pack_ty.Items.Add(packs);
                    }

                }
                else
                    if (checkBox1.Checked == false)
                    {
                        pack_ty.Hide();
                    }
            }

        }

        private void pack_ty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Day_Load(object sender, EventArgs e)
        {
            foreach (String i in Database_Controller.get_all_day_vechcal_type())
            {
                vh_ty_cb.Items.Add(i);

            }

        }
     
        //Calculation function
        public void cal_all()
        {
            if (total_km.Text.Length == 0) { MessageBox.Show("Please insert total km"); }
            else if (vh_ty_cb.SelectedItem == null) { MessageBox.Show("Please select Vehicle Type"); }

            else
            {
                float mysum = 0;
                float base_total = 0;
                float waiting_charge = 0;
                float extra_charge_km = 0;

                //time
                TimeSpan hour = end_time_picker.Value - start_time_picker.Value;
                int total_hire_hours = hour.Hours;
                String vechical_type = vh_ty_cb.SelectedItem.ToString();
                float totalKM = float.Parse(total_km.Text.ToString());
                //get Day hire details from DBC
                float one_hour_price = Database_Controller.day_hire_km_and_hours_Price(vechical_type)[0];
                float one_km_price = Database_Controller.day_hire_km_and_hours_Price(vechical_type)[1];
                // Algorithm Starts here :)

                if (checkBox1.Checked == true)
                {
                    //with package
                    if (pack_ty.SelectedItem == null) { MessageBox.Show("Please select package"); }
                    else
                    {
                        Console.WriteLine("::Package base::");
                        String get_pack_id = pack_ty.SelectedItem.ToString();
                        float package_price = Database_Controller.day_packages_prices(vechical_type, get_pack_id);
                        float package_time = Database_Controller.day_hire_Packages_hours_and_km_Count(get_pack_id,vh_ty_cb.SelectedItem.ToString())[0];
                        float package_km = Database_Controller.day_hire_Packages_hours_and_km_Count(get_pack_id, vh_ty_cb.SelectedItem.ToString())[1];
                        if (totalKM > package_km && total_hire_hours > package_time)
                        {
                            Console.WriteLine("Over package");
                            float difkm = totalKM - package_km;
                            extra_charge_km = difkm * one_km_price;

                            waiting_charge = (total_hire_hours - package_time) * one_hour_price;
                            mysum = package_price + extra_charge_km + waiting_charge;
                            //Display
                            sum.Text = mysum.ToString();
                            base_char_lbl.Text = package_price.ToString();
                            waiting_lbl.Text = waiting_charge.ToString();
                            ex_km_lbl.Text = extra_charge_km.ToString();

                        }
                        else if (totalKM > package_km)
                        {
                            Console.WriteLine("Over km");
                            float difkm = totalKM - package_km;
                            extra_charge_km = difkm * one_km_price;

                            //   waiting_charge = (total_hire_hours - package_time) * one_hour_price;
                            mysum = package_price + extra_charge_km + waiting_charge;
                            //Display
                            sum.Text = mysum.ToString();
                            base_char_lbl.Text = package_price.ToString();
                            waiting_lbl.Text = waiting_charge.ToString();
                            ex_km_lbl.Text = extra_charge_km.ToString();

                        }
                        else if (total_hire_hours > package_time)
                        {
                            Console.WriteLine("Over hour");
                            float difkm = totalKM - package_km;
                            // extra_charge_km = difkm * one_km_price;
                            waiting_charge = (total_hire_hours - package_time) * one_hour_price;
                            mysum = package_price + extra_charge_km + waiting_charge;
                            //Display
                            sum.Text = mysum.ToString();
                            base_char_lbl.Text = package_price.ToString();
                            waiting_lbl.Text = waiting_charge.ToString();
                            ex_km_lbl.Text = extra_charge_km.ToString();
                        }
                        else
                        {
                            Console.WriteLine("in package");
                            Console.WriteLine(package_price);
                            waiting_lbl.Text = "0";
                            base_char_lbl.Text = package_price.ToString();
                            sum.Text = package_price.ToString();
                        }

                    }
                }
                else if (checkBox1.Checked == false)
                {
                    //without package
                    Console.WriteLine("Non Package");
                    base_total = totalKM * one_km_price;
                    waiting_charge = total_hire_hours * one_hour_price;
                    mysum = base_total + waiting_charge;
                    //Display
                    sum.Text = mysum.ToString();
                    base_char_lbl.Text = base_total.ToString();
                    waiting_lbl.Text = waiting_charge.ToString();

                }

            }


        }
        private void sum_Click(object sender, EventArgs e)
        {

        }

        private void vh_ty_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();

            foreach (String i in Database_Controller.day__get_vechcal_no(vh_ty_cb.SelectedItem.ToString()))
            {
                comboBox2.Items.Add(i);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (id_no.Text.Length == 0) { MessageBox.Show("Please insert the id"); }
            else
            {
                //Search
                List<String> search = Database_Controller.get_day_hire_book_details(id_no.Text.ToString());
                if (search == null) { MessageBox.Show("Invalid id"); }
                else
                {

                    cus_name.Text = search[0];
                    mobile_no.Text = search[1];
                    nic.Text = search[2];
                    vh_ty_cb.Text = search[7];
                    comboBox2.Text = search[8];
                    start_time_picker.Value = DateTime.Parse(search[3]);
                    end_time_picker.Value = DateTime.Parse(search[4]);
                    checkBox1.Checked = bool.Parse(search[5]);
                   
                    if (bool.Parse(search[5]) == true)
                    {
                        pack_ty.Show();
                        pack_ty.Text = search[6];
                    }
                    

                    total_km.Text = search[9];
                    base_char_lbl.Text = search[10];
                    ex_km_lbl.Text = search[11];
                    waiting_lbl.Text = search[12];
                    sum.Text = search[13];
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
      

            if (id_no.Text.Length == 0)
            {

                MessageBox.Show("please insert the id");
            }
            else
            {
                if (validation ()== true)
                {

                    cal_all();
                    Console.WriteLine(waiting_lbl.Text);
                    long id = long.Parse(id_no.Text);
                    String start_time_value = start_time_picker.Value.ToShortTimeString();
                    float extra_kmCharge = float.Parse(waiting_lbl.Text);
                    String end_times = end_time_picker.Value.ToShortTimeString();
                    String total_sum = sum.Text.ToString();
                    float totalKM = float.Parse(total_km.Text);
                    //
                    float waitingc = float.Parse(waiting_lbl.Text);
                    float exkmCha = float.Parse(ex_km_lbl.Text.ToString());
                    float base_charge = float.Parse(base_char_lbl.Text.ToString());
                    Console.WriteLine(waiting_lbl.Text);
                    if (checkBox1.Checked == false)
                    {
                        Console.WriteLine(waiting_lbl.Text);
                        bool uptade = Database_Controller.day_hire_book_update(id,
                                      cus_name.Text, nic.Text,
                                      mobile_no.Text, start_time_value, end_times, total_sum,
                                      checkBox1.Checked, vh_ty_cb.SelectedItem.ToString(),
                                      comboBox2.SelectedItem.ToString(), "", totalKM, base_charge,
                                       waitingc, exkmCha);

                        if (uptade == true)
                        {
                            progressBar1.Value = 100;
                            MessageBox.Show("Sucessfully Updated");
                            progressBar1.Value = 0;
                        }
                        else if (uptade == false)
                        {
                            MessageBox.Show("Not Updated");
                        }
                    }
                    //
                    else if (checkBox1.Checked == true)
                    {
                        Console.WriteLine(waiting_lbl.Text);
                        bool uptade = Database_Controller.day_hire_book_update(id, cus_name.Text, nic.Text,
                                          mobile_no.Text, start_time_value, end_times, total_sum,
                                          checkBox1.Checked, vh_ty_cb.SelectedItem.ToString(),
                                          comboBox2.SelectedItem.ToString(), pack_ty.SelectedItem.ToString(), totalKM, base_charge,
                                         waitingc, exkmCha);
                        if (uptade == true)
                        {
                            progressBar1.Value = 100;
                            MessageBox.Show("Sucessfully Updated");
                            progressBar1.Value = 0;
                        }
                        else if (uptade == false)
                        {
                            MessageBox.Show("Not Updated");
                        }

                    }

                }
                else { Console.WriteLine("Validation error"); }


                //
            }
                
               
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id_no.Text.Length == 0)
            {

                MessageBox.Show("Please Insert the ID");
            }
            else {
                bool delete = Database_Controller.day_hire_book_delete(id_no.Text);
                if (delete == true) {
                    progressBar1.Value = 100;
                    MessageBox.Show(id_no.Text + " Deleted"); progressBar1.Value = 0;
                }
                else 
                {
                    MessageBox.Show(id_no.Text + " Not Deleted"); 
                }
            }
        }
    }
}

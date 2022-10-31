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
    public partial class Long_hire : Form
    {
        public Long_hire()
        {
            InitializeComponent();
         
        }

        private void @long_Load(object sender, EventArgs e)
        {
 
            foreach(String vh in Database_Controller.get_all_long_vechcal_type()){
                vh_ty_cb.Items.Add(vh);
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }
        //Long hire Calculation
        private void long_hire_cal() {

            if (total_km_txt.Text.Length == 0) { MessageBox.Show("Please insert total km"); }
            else if (vh_ty_cb.SelectedItem == null) { MessageBox.Show("Please select Vehicle Type"); }
            else if (pack_ty.SelectedItem == null) { MessageBox.Show("Please select package"); }
            else
            {
                TimeSpan sumDates = end_time_picker.Value - start_time_picker.Value;
                int total_hire_days = sumDates.Days;
                float total_KM = float.Parse(total_km_txt.Text);
                float one_night_price = Database_Controller.long_hire_1km_and_1night_Price(vh_ty_cb.SelectedItem.ToString())[0];
                float km1_charge = Database_Controller.long_hire_1km_and_1night_Price(vh_ty_cb.SelectedItem.ToString())[1];
                //
                float overnightcharge = 0;
                float extra_km_charges = 0;
                float base_charge = 0;
                float total_sum = 0;
                //

                String pk_id =pack_ty.SelectedItem.ToString();
                float pack_price = Database_Controller.long_packages_prices(vh_ty_cb.SelectedItem.ToString(), pk_id);
                float pack_days = Database_Controller.long_hire_Packages_days_and_km_Count(pk_id,vh_ty_cb.SelectedItem.ToString())[0];
                float pack_km = Database_Controller.long_hire_Packages_days_and_km_Count(pk_id, vh_ty_cb.SelectedItem.ToString())[1];
                float differns_days = total_hire_days - pack_days; float differns_km = total_KM - pack_km;
                Console.WriteLine("pack KM" + pack_km);
                Console.WriteLine("Diff KM" + differns_km);
                Console.WriteLine("one 1km price" + km1_charge);
                if (pack_days < total_hire_days && pack_km < total_KM)
                {
                    overnightcharge = differns_days * one_night_price;
                    extra_km_charges = differns_km * km1_charge;
                    total_sum = pack_price + extra_km_charges + overnightcharge;
                    //display
                    base_char_lbl.Text = pack_price.ToString();
                    ov_ni8_lbl.Text = overnightcharge.ToString();
                    ex_km_lbl.Text = extra_km_charges.ToString();
                    sum.Text = total_sum.ToString();
                }
                else if (pack_days < total_hire_days)
                {
                    overnightcharge = differns_days * one_night_price;
                    //extra_km_charges = differns_km * km1_charge;
                    total_sum = pack_price + extra_km_charges + overnightcharge;
                    //display
                    base_char_lbl.Text = pack_price.ToString();
                    ov_ni8_lbl.Text = overnightcharge.ToString();
                    ex_km_lbl.Text = extra_km_charges.ToString();
                    sum.Text = total_sum.ToString();

                }
                else if (pack_km < total_KM)
                {
                    //overnightcharge = differns_days * one_night_price;
                    extra_km_charges = differns_km * km1_charge;
                    total_sum = pack_price + extra_km_charges + overnightcharge;
                    //display
                    base_char_lbl.Text = pack_price.ToString();
                    ov_ni8_lbl.Text = overnightcharge.ToString();
                    ex_km_lbl.Text = extra_km_charges.ToString();
                    sum.Text = total_sum.ToString();
                }
                else
                {
                    total_sum = pack_price;
                    base_char_lbl.Text = pack_price.ToString();
                    ov_ni8_lbl.Text = overnightcharge.ToString();
                    ex_km_lbl.Text = extra_km_charges.ToString();
                    sum.Text = total_sum.ToString();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            long_hire_cal();
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
                MessageBox.Show("Please Select Vehicle No ", "error");

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

        private void Save_Click(object sender, EventArgs e)
        {
            long_hire_cal();
            if (validation() == true) {
                long id=Ay_Formula.cusmomer_id_gena();
                String sDate=start_time_picker.Value.ToShortDateString();
                String eDate=end_time_picker.Value.ToShortDateString();
                float base_charge = float.Parse(base_char_lbl.Text);
                float over_night = float.Parse(ov_ni8_lbl.Text);
                float ex_km_charge = float.Parse(ex_km_lbl.Text);
                float total = float.Parse(sum.Text);
                float tot_km = float.Parse(total_km_txt.Text);
                //
                bool insert = Database_Controller.long_hire_book_insert(id, cus_name.Text, nic.Text, 
                    mobile_no.Text.ToString(), sDate, eDate,
                   total, vh_ty_cb.SelectedItem.ToString(), 
                    comboBox2.SelectedItem.ToString(), pack_ty.SelectedItem.ToString(), tot_km, base_charge, 
                    over_night, ex_km_charge);

                if (insert == true) {
                    id_no.Text = id.ToString();
                    progressBar1.Value = 100;
                    MessageBox.Show("Sucessfully inserted");
                    progressBar1.Value = 0;
                   
                }
                else if (insert == false) { MessageBox.Show("Fail to insert Data ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
            } else if(validation()==false){
                Console.WriteLine("Validation Error");
                Console.Beep();
            
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void vh_ty_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<String> vh_type = Database_Controller.long__get_vechcal_no(vh_ty_cb.SelectedItem.ToString());
            comboBox2.Items.Clear();
            foreach (String i in vh_type) {
                comboBox2.Items.Add(i);
            
            }
            foreach (String i in Database_Controller.get_all_long_hire_package_name(vh_ty_cb.SelectedItem.ToString()))
            {

                pack_ty.Items.Add(i);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id_no.Text.Length == 0)
            {
                MessageBox.Show("Please enter the ID");
            }
            else
            {
                if (validation() == true)
                {
                    long_hire_cal();
                    long id = long.Parse(id_no.Text);
                    String sDate = start_time_picker.Value.ToShortDateString();
                    String eDate = end_time_picker.Value.ToShortDateString();
                    float base_charge = float.Parse(base_char_lbl.Text);
                    float over_night = float.Parse(ov_ni8_lbl.Text);
                    float ex_km_charge = float.Parse(ex_km_lbl.Text);
                    float total = float.Parse(sum.Text);
                    float tot_km = float.Parse(total_km_txt.Text);

                    bool update = Database_Controller.long_hire_book_update(id, cus_name.Text, nic.Text,
                    mobile_no.Text.ToString(), sDate, eDate,
                   total, vh_ty_cb.SelectedItem.ToString(),
                    comboBox2.SelectedItem.ToString(), pack_ty.SelectedItem.ToString(), tot_km, base_charge,
                    over_night, ex_km_charge);
                    if (update == true)
                    {//202125312457
                        progressBar1.Value = 100;
                        MessageBox.Show("Sucessfully Updated");
                        progressBar1.Value = 0;
                        
                    }
                    else if (update == false) { MessageBox.Show("Fail to Update the Data ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                {
                    Console.WriteLine("Validation Error");
                    Console.Beep();
            

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id_no.Text.Length == 0)
            {
                MessageBox.Show("Please insert the ID");

            }
            else {
                bool delete = Database_Controller.long_hire_book_delete(id_no.Text);

                if (delete == true)
                {
                    progressBar1.Value = 100;
                    MessageBox.Show("Sucessfully Deleted");
                    progressBar1.Value = 0;
                }
                else if (delete == false) { MessageBox.Show("Fail to Deleted the Data ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (id_no.Text.Length == 0)
            {
                MessageBox.Show("Please insert the ID");

            }
            else {
                List<String> search = Database_Controller.get_long_hire_book_details(id_no.Text);
                if (search == null)
                {
                    MessageBox.Show("Invalid id");
                }
                else {
                    cus_name.Text = search[0];
                    mobile_no.Text = search[1];
                    nic.Text = search[2];
                    start_time_picker.Value = DateTime.Parse(search[3]);
                    end_time_picker.Value = DateTime.Parse(search[4]);
                    pack_ty.Text = search[5];
                    vh_ty_cb.Text = search[6];
                    comboBox2.Text = search[7];
                    total_km_txt.Text = search[8];
                    base_char_lbl.Text = search[9];
                    ex_km_lbl.Text = search[10];
                    ov_ni8_lbl.Text = search[11];
                    sum.Text = search[12];

                
                }
            
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}

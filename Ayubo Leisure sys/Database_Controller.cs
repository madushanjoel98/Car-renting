using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Ayubo_Leisure_sys
{
    // this class control Database
    class Database_Controller
    {
        
    
        public static SqlConnection connection()
        {

            try
            {
                SqlConnection st = new SqlConnection("Data Source=HP\\SQLEXPRESS;Initial Catalog=ayubo;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
                return st;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static bool user_login(String user_name,String password)
        {
            SqlConnection conn = connection();
            conn.Open();

            String sql = "Select * from user_log where user_name='"+user_name+"' and pass='"+password+"'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader srd = cmd.ExecuteReader();
            if (srd.Read())
            {
             return true;
            }else{
            return false;
            }
            conn.Close();
            return false;
        }

        // Rent Calculation
        public static float[] rent_prices(String car_type ){
            float daily=0;
            float week=0;
            float monthly=0;
            float driver = 0;
            SqlConnection conn = connection();
               conn.Open();
                String sql = "Select daily,week,month,driver_price from rent_price where car_type='"+car_type+"'";
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataReader srd = cmd.ExecuteReader();
                if(srd.Read())
                {
                    daily = float.Parse(srd[0].ToString());
                    week = float.Parse(srd[1].ToString());
                    monthly = float.Parse(srd[2].ToString()); 
                    driver=float.Parse(srd[3].ToString());
                }
                float[] price = { daily, week, monthly, driver };
                conn.Close();
                 return price;
        
        }
         public static List<String> rent_get_vechcal_no(String car_type ){
            SqlConnection conn = connection();
               conn.Open();
              List<String> vechical_no=new List<string>();
              String sql = "Select vechicals_no from vechicals_details where vechical_type='" + car_type + "' and service_type='rent'";
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataReader srd = cmd.ExecuteReader();
                while (srd.Read())
                {
                    vechical_no.Add(srd["vechicals_no"].ToString());
                }
                conn.Close();
                 return vechical_no;
        }
         public static List<String> get_all_vechcal_type()
         {
             SqlConnection conn = connection();
             conn.Open();
             List<String> vechical_no = new List<string>();
             String sql = "select car_type from rent_price";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             while (srd.Read())
             {
                 vechical_no.Add(srd["car_type"].ToString());
             }
             conn.Close();
             return vechical_no;
         }
         public static bool rent_book_insert(long id, String name,String nic,String mobile_no,String sDate,String endDate,String totall,bool driver,String vechi_type,String vechi_no) {
             try
             {
                 SqlConnection conn = connection();
                 conn.Open();
                 String now = DateTime.Now.ToString();
                 String sql = "INSERT INTO rent_book(rent_Id,customer_name ,nic_no,mobile_no , start_date,end_date,with_driver,vechical_type, vechical_no,total) " +
                     "Values ('" + id + "','" + name + "','" + nic + "','" + mobile_no + "','" + sDate + "','" + endDate + "','" + driver + "','" + vechi_type + "','" + vechi_no + "','" + totall + "'); ";
                 SqlCommand cmd = new SqlCommand(sql, conn);
                 int i = cmd.ExecuteNonQuery();
                 if (i == 1)
                 {
                     return true;
                 }
                
                     return false;
                 

                
             }
             catch(Exception e) {
                 Console.WriteLine(e.Message);
                 return false;
             }
             }
         public static bool rent_book_update(long id, String name, String nic, String mobile_no, String sDate, String endDate, String total, bool driver, String vehicle_type, String vechi_no)
         {
             try
             {
                 SqlConnection conn = connection();
                 conn.Open();
                 String now = DateTime.Now.ToString();
                 String sql = "UPDATE rent_book set customer_name='"+name+"' ,nic_no='"+nic+"',mobile_no='"+mobile_no+"' , start_date='"+sDate+"',end_date='"+endDate+"',with_driver='"+driver+"',vechical_type='"+vehicle_type+"', vechical_no='"+vechi_no+"',total='"+total+"' where rent_Id='"+id+"' ";
                     
                 SqlCommand cmd = new SqlCommand(sql, conn);
       int up= cmd.ExecuteNonQuery();
       if (up == 1) {
           return true;
       }
                 return false;


             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
                 return false;
             }
         }


         public static bool rent_book_delete(String id)
         {
             try
             {
                 SqlConnection conn = connection();
                 conn.Open();
                 String now = DateTime.Now.ToString();
                 String sql = "Delete from rent_book where rent_id='"+id+"'";
                 SqlCommand cmd = new SqlCommand(sql, conn);
                 int i=cmd.ExecuteNonQuery();
                 if (i == 1)
                 {
                     return true;
                 }
                
                     return false;
                 

             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
                 return false;
             }
         }

         public static List<String> get_rentbook_details(String rent_Id)
         {
             SqlConnection conn = connection();
             conn.Open();
             List<String> detials = new List<string>();
             String sql = "Select * from rent_book where rent_Id='" + rent_Id + "' ";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             if (srd.Read()==true)
             {
                
           /* get[0] = customer name get[] */ 
                 detials.Add(srd["customer_name"].ToString());
                 detials.Add(srd["mobile_no"].ToString());
                 detials.Add(srd["nic_no"].ToString());
                 detials.Add(srd["start_date"].ToString());
                 detials.Add(srd["end_date"].ToString());
                 detials.Add(srd["with_driver"].ToString());
                 detials.Add(srd["vechical_type"].ToString());
                 detials.Add(srd["vechical_no"].ToString());
                 detials.Add(srd["total"].ToString());
                 

                 //nic_no
             }
             else if (srd.Read() == false)
             {
                 return null;
             }
             conn.Close();
             return detials;
         }

         // Day hire details from database


         public static List<String> day__get_vechcal_no(String car_type)
         {
             SqlConnection conn = connection();
             conn.Open();
             List<String> vechical_no = new List<string>();
             String sql = "Select vechicals_no from vechicals_details where vechical_type='" + car_type + "' and service_type='day'";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             while (srd.Read())
             {
                 vechical_no.Add(srd["vechicals_no"].ToString());
             }
             conn.Close();
             return vechical_no;
         }

 //packages names
         public static List<String> get_all_day_package_name(String vechical)
         {
             SqlConnection conn = connection();
             try
             {
                 conn.Open();
                 List<String> vechical_no = new List<string>();
                 String sql = "Select DISTINCT package_name from packer where package_type='day' and vehi_type='" + vechical + "'";
                 SqlCommand cmd = new SqlCommand(sql, conn);
                 SqlDataReader srd = cmd.ExecuteReader();
                 
                     while (srd.Read())
                     {
                         vechical_no.Add(srd["package_name"].ToString());
                     }
                 
                 conn.Close();
                 return vechical_no;
             }
             catch (Exception ex){
                 Console.WriteLine(ex.Message);
                 return null;
             }
         }
       
  //get vechicel names
         public static List<String> get_all_day_vechcal_type()
         {
             SqlConnection conn = connection();
             conn.Open();
             List<String> vechical_no = new List<string>();
             String sql = "Select vechical_type from day_hire";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             while (srd.Read())
             {
                 vechical_no.Add(srd["vechical_type"].ToString());
             }
             conn.Close();
             return vechical_no;
         }

         public static float day_packages_prices(String vechical_name, String pack_name)
         {
             SqlConnection conn = connection();
             conn.Open();
             float price = 0;
             String sql = "Select pack_price from packer where package_type='day' and vehi_type='" + vechical_name + "' and package_name='" + pack_name + "';";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             while (srd.Read())
             {
                 price = float.Parse(srd["pack_price"].ToString());
             }
             conn.Close();

             return price;
         }
         public static float[] day_hire_km_and_hours_Price(String car_type)
         {
             float one = 0;
             float km1 = 0;
             
             SqlConnection conn = connection();
             conn.Open();
             String sql = "select one_hour_price,km1_price from day_hire where vechical_type='"+car_type+"'";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             if (srd.Read())
             {
                 one = float.Parse(srd[0].ToString());
                 km1 = float.Parse(srd[1].ToString());
                 
             }
             float[] price = { one, km1};
             conn.Close();
             return price;

         }

         public static float[] day_hire_Packages_hours_and_km_Count(String pk_id,String Vechical)
         {
             float one = 0;
             float km1 = 0;

             SqlConnection conn = connection();
             conn.Open();
             String sql = "Select hours,km from packer where  package_type='day' and vehi_type='"+Vechical+"' and package_name='"+pk_id+"';";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             if (srd.Read())
             {
                 one = float.Parse(srd[0].ToString());
                 km1 = float.Parse(srd[1].ToString());

             }
             float[] price = { one, km1 };
             conn.Close();
             return price;

         }

         public static bool day_hire_book_insert(long id, String name, String nic, String mobile_no, String start_time, String endD_time, String totall, bool with_package, String vechi_type, String vechi_no, String pack_name, float total_km, float base_charge, float waiting_charge, float extra_km_charge)
         {
             try
             {
                 SqlConnection conn = connection();
                 conn.Open();
                 String now = DateTime.Now.ToString();
                 String sql = "INSERT INTO day_hire_book (rent_Id , customer_name ,nic_no ,mobile_no ,start_time,end_time,with_package,vechical_type, vechical_no,total,package_name,total_km,base_charge,extra_km_charge,waiting_charge) " +
                     "Values ('" + id + "','" + name + "','" + nic + "','" + mobile_no + "','" + start_time + "','" + endD_time + "','" + with_package + "','" + vechi_type + "','" + vechi_no + "','" + totall + "','"+pack_name+"' ,'"+total_km+"',"
                 +"'"+base_charge+"','"+extra_km_charge+"','"+waiting_charge+"'); ";
                 SqlCommand cmd = new SqlCommand(sql, conn);
                 int i = cmd.ExecuteNonQuery();
                 if (i == 1)
                 {
                     return true;
                 }

                 return false;



             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
                 return false;
             }
         }
         public static bool day_hire_book_update(long id, String name, String nic, String mobile_no, String start_time, String endD_time, String total, bool with_package, String vechi_type, String vechi_no, String pack_name, float total_km, float base_charge, float waiting_charge, float extra_km_charge)
         {
             try
             {
                 SqlConnection conn = connection();
                 conn.Open();
                 String now = DateTime.Now.ToString();
                 String sql = "UPDATE day_hire_book set customer_name='" + name + "' ,nic_no='" + nic + "',mobile_no='" + mobile_no + "' ," +
                     "start_time='" + start_time + "',end_time='" + endD_time + "',with_package='" + with_package + "',vechical_type='" + vechi_type + "'," +
                     " vechical_no='" + vechi_no + "',total='" + total + "',package_name='" + pack_name + "',total_km='" + total_km + "',base_charge='" + base_charge + "',extra_km_charge='" + extra_km_charge + "',waiting_charge='"+waiting_charge+"' where rent_Id='" + id + "' ";

                 SqlCommand cmd = new SqlCommand(sql, conn);
                 int up = cmd.ExecuteNonQuery();
                 if (up == 1)
                 {
                     return true;
                 }
                 return false;


             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
                 return false;
             }
         }


         public static bool day_hire_book_delete(String id)
         {
             try
             {
                 SqlConnection conn = connection();
                 conn.Open();
                 String now = DateTime.Now.ToString();
                 String sql = "Delete from day_hire_book where rent_id='" + id + "'";
                 SqlCommand cmd = new SqlCommand(sql, conn);
                 int i = cmd.ExecuteNonQuery();
                 if (i == 1)
                 {
                     return true;
                 }

                 return false;


             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
                 return false;
             }
         }

         public static List<String> get_day_hire_book_details(string day_book_Id)
         {
             try
             {
                 SqlConnection conn = connection();
                 conn.Open();
                 List<String> detials = new List<String>();
                 String sql = "Select * from day_hire_book where rent_Id='" + day_book_Id + "' ";
                 SqlCommand cmd = new SqlCommand(sql, conn);
                 SqlDataReader srd = cmd.ExecuteReader();
                 if (srd.Read()==true)
                 {
                     //customer_name ,nic_no,mobile_no , start_date,end_date,with_driver,vechical_type, vechical_no,total

                     /* get[0] = customer name get[] */
                     detials.Add(srd["customer_name"].ToString());
                     /* get[1]*/
                     detials.Add(srd["mobile_no"].ToString());
                     /* get[2]*/
                     detials.Add(srd["nic_no"].ToString());
                     detials.Add(srd["start_time"].ToString());
                     detials.Add(srd["end_time"].ToString());
                     detials.Add(srd["with_package"].ToString());
                     detials.Add(srd["package_name"].ToString());
                     detials.Add(srd["vechical_type"].ToString());
                     detials.Add(srd["vechical_no"].ToString());
                     detials.Add(srd["total_km"].ToString());
                     detials.Add(srd["base_charge"].ToString());
                     detials.Add(srd["extra_km_charge"].ToString());
                     detials.Add(srd["waiting_charge"].ToString());
                     detials.Add(srd["total"].ToString());


                     //nic_no
                 }
                 else if (srd.Read() == false)
                 {
                     return null;
                 }
                 conn.Close();
                 return detials;
             }
             catch (Exception ex) {
                 Console.WriteLine(ex.Message);
                 return null;
             
             }
         }
        
        
        
        
        // Get long Hire details from database

         public static List<String> get_all_long_hire_package_name(String vechical)
         {
             SqlConnection conn = connection();
             conn.Open();
             List<String> vechical_no = new List<string>();
             String sql = "Select DISTINCT package_name from packer where package_type='long' and vehi_type='" + vechical + "'";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             while (srd.Read())
             {
                 vechical_no.Add(srd["package_name"].ToString());
             }
             conn.Close();
             return vechical_no;
         }
     

         public static List<String> get_all_long_vechcal_type()
         {
             SqlConnection conn = connection();
             conn.Open();
             List<String> vechical_no = new List<string>();
             String sql = "Select vechical_type from long_day_hire_price";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             while (srd.Read())
             {
                 vechical_no.Add(srd["vechical_type"].ToString());
             }
             conn.Close();
             return vechical_no;
         }

         public static float long_packages_prices(String vechical_name, String pack_name)
         {
             SqlConnection conn = connection();
             conn.Open();
             float price = 0;
             String sql = "Select pack_price from packer where package_type='long' and vehi_type='" + vechical_name + "' and package_name='" + pack_name + "';";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             while (srd.Read())
             {
                 price = float.Parse(srd["pack_price"].ToString());
             }
             conn.Close();

             return price;
         }
         public static float[] long_hire_1km_and_1night_Price(String car_type)
         {
             float one = 0;
             float km1 = 0;
           
             SqlConnection conn = connection();
             conn.Open();
             String sql = "select over_night_charge,extra_km_charge from long_day_hire_price where vechical_type='" + car_type + "'";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             if (srd.Read())
             {
                 one = float.Parse(srd[0].ToString());
                 km1 = float.Parse(srd[1].ToString());

             }
             float[] price = { one, km1 };
             conn.Close();
             return price;

         }

         public static float[] long_hire_Packages_days_and_km_Count(String pk_id, String Vechical)
         {
             float one = 0;
             float km1 = 0;

             SqlConnection conn = connection();
             conn.Open();
             String sql = "Select days,km from packer where  package_type='long' and vehi_type='" + Vechical + "' and package_name='" + pk_id + "';";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             if (srd.Read())
             {
                 one = float.Parse(srd[0].ToString());
                 km1 = float.Parse(srd[1].ToString());

             }
             float[] price = { one, km1 };
             conn.Close();
             return price;

         }


         public static bool long_hire_book_insert(long id, String name, String nic, String mobile_no, String start_time, String endD_time, float total,String vechi_type,
             String vechi_no, String pack_name, float total_km, float base_charge, float over_night, float extra_km_charge)
         {
             try
             {
    
                 SqlConnection conn = connection();
                 conn.Open();
                 String now = DateTime.Now.ToString();
                 String sql = "INSERT INTO long_hire_book (hire_Id , customer_name ,nic_no ,mobile_no ,start_date,end_date,vechical_type,"+" vechical_no,total,package_name,total_km,base_charge,extra_km_charge,over_night_charge)" +
                     "Values ('" + id + "','" + name + "','" + nic + "','" + mobile_no + "','" + start_time + "','" + endD_time + "','" + vechi_type + "','" + vechi_no + "','" + total + "','" + pack_name + "' ,'" + total_km + "','" + base_charge + "','" + extra_km_charge + "','" + over_night + "')";
                 SqlCommand cmd = new SqlCommand(sql, conn);
                 int i = cmd.ExecuteNonQuery();
                 if (i == 1)
                 {
                     return true;
                 }

                 return false;



             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
                 return false;
             }
         }
         public static bool long_hire_book_update(long id, String name, String nic, String mobile_no, String start_time, String endD_time, float total, String vechi_type, String vechi_no, String pack_name, float total_km, float base_charge, float over_night, float extra_km_charge)
         {
             try
             {
                 SqlConnection conn = connection();
                 conn.Open();
                 String now = DateTime.Now.ToString();
                 String sql = "UPDATE long_hire_book set customer_name='" + name + "' ,nic_no='" + nic + "',mobile_no='" + mobile_no + "' ," +
                     "start_date='" + start_time + "',end_date='" + endD_time + "',vechical_type='" + vechi_type + "'," +
                     " vechical_no='" + vechi_no + "',total='" + total + "',package_name='" + pack_name + "',total_km='" + total_km + "',base_charge='" + base_charge + "',extra_km_charge='" + extra_km_charge + "',over_night_charge='" + over_night + "' where hire_Id='" + id + "' ";

                 SqlCommand cmd = new SqlCommand(sql, conn);
                 int up = cmd.ExecuteNonQuery();
                 if (up == 1)
                 {
                     return true;
                 }
                 return false;


             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
                 return false;
             }
         }

         public static List<String> long__get_vechcal_no(String car_type)
         {
             SqlConnection conn = connection();
             conn.Open();
             List<String> vechical_no = new List<string>();
             String sql = "Select vechicals_no from vechicals_details where vechical_type='" + car_type + "' and service_type='long'";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader srd = cmd.ExecuteReader();
             while (srd.Read())
             {
                 vechical_no.Add(srd["vechicals_no"].ToString());
             }
             conn.Close();
             return vechical_no;
         }
         public static bool long_hire_book_delete(String id)
         {
             try
             {
                 SqlConnection conn = connection();
                 conn.Open();
                 String now = DateTime.Now.ToString();
                 String sql = "Delete from long_hire_book where hire_Id='" + id + "'";
                 SqlCommand cmd = new SqlCommand(sql, conn);
                 int i = cmd.ExecuteNonQuery();
                 if (i == 1)
                 {
                     return true;
                 }

                 return false;


             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
                 return false;
             }
         }
         public static List<String> get_long_hire_book_details(String hire_Id)
         {
             try
             {
                 SqlConnection conn = connection();
                 conn.Open();
                 List<String> detials = new List<string>();
                 String sql = "Select * from long_hire_book where hire_Id='" + hire_Id + "' ";
                 SqlCommand cmd = new SqlCommand(sql, conn);
                 SqlDataReader srd = cmd.ExecuteReader();
                 if (srd.Read()==true)
                 {
                     //customer_name ,nic_no,mobile_no , start_date,end_date,with_driver,vechical_type, vechical_no,total

                     /* get[0] = customer name get[] */
                     detials.Add(srd["customer_name"].ToString());
                     /* get[1]*/
                     detials.Add(srd["mobile_no"].ToString());
                     /* get[2]*/
                     detials.Add(srd["nic_no"].ToString());
                     detials.Add(srd["start_date"].ToString());
                     detials.Add(srd["end_date"].ToString());
                     detials.Add(srd["package_name"].ToString());
                     detials.Add(srd["vechical_type"].ToString());
                     detials.Add(srd["vechical_no"].ToString());
                     detials.Add(srd["total_km"].ToString());
                     detials.Add(srd["base_charge"].ToString());
                     detials.Add(srd["extra_km_charge"].ToString());
                     detials.Add(srd["over_night_charge"].ToString());
                     detials.Add(srd["total"].ToString());


                     //nic_no
                 }
                 else if (srd.Read() == false) {
                     return null;
                 }
                 conn.Close();
                 return detials;
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.Message);
                 return null;

             }
         }

         public static SqlDataAdapter adp() {

             SqlConnection conn = connection();
             conn.Open();
          
             String sql = "Select * from  vechicals_details ";
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataAdapter ss = new SqlDataAdapter(cmd);
             SqlDataReader srd = cmd.ExecuteReader();  
         return ss;
         }
    
    
    
    }

 


}




    
           
    
  


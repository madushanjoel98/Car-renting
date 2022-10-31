using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 this code done by myself with my present and previous C# skill
 * ::::::::madushan joel::::::::::
 * this Class shows formulas and algorithms, which use in ayubo Drive system.
 * rent_counter=Cars Rent Calculation
 */

namespace Ayubo_Leisure_sys
{
    class Ay_Formula
    {

        public static long cusmomer_id_gena(){
            String date = DateTime.Now.Year + DateTime.Now.Day.ToString() + 
                DateTime.Now.Month.ToString() + DateTime.Now.Hour +
                DateTime.Now.Minute + DateTime.Now.Second;
       return long.Parse(date);
        }
        public static float rent_counter(int days, bool driver, String car_type)
        {
            // Get Data from DBC
float[] rentarry=Database_Controller.rent_prices(car_type);
            float daily =rentarry[0];
            float weekly = rentarry[1];
            float monthly = rentarry[2];
            float driver_rent =Convert.ToInt32(rentarry[3]);
            float totall_cost = 0;

            Console.WriteLine("DRIVER COST:"+ driver_rent);

            //week
            int week_remain = days % 7;
            int weeks_count = (days - week_remain) / 7;
            //month formula
            int month_remain = days % 30;
            int month_counter = (days - month_remain) / 30;

            // Algorithm Starts here

            if(driver==true){
               //:::::::With driver:::::::
                Console.WriteLine(":::::::With driver:::::::");
                if (days < 1)
                {

                    Console.WriteLine("Daily base:" + daily.ToString());

                    totall_cost = daily * 1 + driver_rent;
         
                }
                else
                    if (days < 7)
                    {
                        Console.WriteLine("Daily base:" + daily.ToString());
                        totall_cost = daily * days + driver_rent * days;
                    }
                    else

                        if (days < 30)
                        {
                            Console.WriteLine("Week base" + weekly.ToString());
                            totall_cost = weekly * weeks_count + daily * week_remain + driver_rent * days;
                            //return totall_cost;
                        }
                        else
                            if (days >= 30)
                            {
                                Console.WriteLine("month base:" + monthly.ToString());
                                if (month_remain > 7)
                                {
                                    totall_cost = month_counter * monthly + weekly * (month_remain / 7) + daily
                                        * (month_remain % 7) + driver_rent * days;

                                }
                                else if (month_remain < 7)
                                {
                                    totall_cost = monthly * month_counter + daily * month_remain + driver_rent * days;

                                }
                            }    
            }
            else if(days<1){
                //:::::::With driver:::::::
                Console.WriteLine("Daily base:"+daily.ToString());
              
                totall_cost = daily * 1;
            }else
            if (days < 7)
            {
                Console.WriteLine("Daily base:"+daily.ToString());
                totall_cost = daily * days;
            }
            else

                if (days < 30)
                {
                    Console.WriteLine("Week base" + weekly.ToString());
                    totall_cost = weekly * weeks_count + daily * week_remain;
                    //return totall_cost;
                }
                else
                    if (days >= 30)
                    {
                        Console.WriteLine("month base:"+monthly.ToString());
                        if (month_remain > 7)
                        {
                            totall_cost = month_counter * monthly + weekly * (month_remain / 7) + daily * (month_remain % 7);

                        }
                        else if (month_remain < 7)
                        {
                            totall_cost = monthly * month_counter + daily * month_remain;

                        }

                    }
            
            
            return totall_cost;
            // END
        }

        public static float day_hire_calculation(  bool select_package) {
            int start_Time=8;int end_time=16;
            int Start_km=0; int end_km=325;
            int pakage_hour=8;int pakage_km=200; float package_price=5000;
            float one_hour_price = 50; float one_km_price = 100;
            int totall_hire_time = end_time - start_Time; int totall_km = end_km - Start_km;
            // price Calculation
 float base_price = (one_hour_price * totall_hire_time) + (one_km_price * totall_km);
  float waiting_carge=(totall_hire_time-pakage_hour)*one_hour_price;
   float extra_km_price = (totall_km - pakage_km) * one_km_price; float total = package_price + waiting_carge + extra_km_price;

            if(select_package==false){

                Console.WriteLine("none Package base");
                return base_price;
             
            }
            else if (select_package == true) {
                if (totall_hire_time > pakage_hour && totall_km > pakage_km)
                {
                    Console.WriteLine("Package base with extra price");
                    Console.WriteLine("Extra km:" + totall_km);
                    Console.WriteLine(" Exra hours:" + totall_hire_time);
                    Console.WriteLine(" Waiting Charge:" + waiting_carge);
                    Console.WriteLine(" Extra km price:" + extra_km_price);
                    return total;
                }
                else 
                {
                    Console.WriteLine("Package base");
                    return package_price;
                }
            } return 0;
        }

        public static void long_hour(bool package,int start_date, int end_date, float total_km) {
            //package details
            float package_price =1500; float package_km=200; int package_days=2;
            float one_night_price =950;
            float km1_price=50;
            float extra_kms = total_km - package_km;
            int totall_days = end_date - start_date;
             float extra_dates=totall_days-package_days;
             Console.WriteLine(extra_dates);
            float overnight_charge = 0;
            float extra_km_charge = 0;
            float base_charge = km1_price * total_km;
            //
            float total=0;
        if(package==true){
           if (package_km < total_km || package_days < totall_days)
            {
                Console.WriteLine("Both over");
                overnight_charge = extra_dates *one_night_price;
                Console.WriteLine("over night:" + overnight_charge);
                extra_km_charge = extra_kms * km1_price;
                Console.WriteLine("Extra Km Charge:" + extra_km_charge);
                total = package_price + overnight_charge + extra_km_charge;
                Console.WriteLine("Total:" + total);
            }
            else
            {
                Console.WriteLine("over night:" + overnight_charge);
                Console.WriteLine("Extra Km Charge:" + extra_km_charge);
                Console.WriteLine("Total:" + package_price);
            }
        }
        else if (package == false) {
            Console.WriteLine("::non package::");
            overnight_charge = totall_days * one_night_price;
            Console.WriteLine(overnight_charge);
            base_charge = km1_price * total_km;
        
        }
        
        }

        public static int rent_counter(int days, bool driver)
        {
            int daily = 1000;int weekly = 7500; int monthly = 25000;int driver_rent = 2500;int totall_cost = 0;
            int remen_week = days % 7; int weeks_count = (days - remen_week) / 7;
            //month
            int month_remain = days % 30; int month_counter = (days - month_remain) / 30;

            if (days < 7)
            {
                Console.WriteLine("Daily base");
                totall_cost = daily * days;
            }
            else if (days < 30){Console.WriteLine("Week base");
                    totall_cost = weekly * weeks_count + daily * remen_week;
                    return totall_cost;
                }
                else  if (days >= 30) { Console.WriteLine("month base");if (month_remain > 7)
                        {
                            totall_cost = month_counter * monthly + weekly * (month_remain / 7) + daily * (month_remain % 7);

                        }
                        else if (month_remain < 7)
                        {
                            totall_cost = monthly * month_counter + daily * month_remain;

                        }
                        return totall_cost;

                    }
            return totall_cost;
        }
    
    }
}
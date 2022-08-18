using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFlight.viewmodel
{
    public class HolidayVM : INotifyPropertyChanged
    {
        model.HolidatModel model;

        public HolidayVM()
        {
            model = new model.HolidatModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

   



        public async Task<string> isHoliday(string date)
        {
            DateTime dateTime=new DateTime();
            try
            {
                 dateTime = DateTime.Parse(date);
                DateTime date1 = dateTime;
                string a = DateTime.Today.ToShortDateString();
                string b = dateTime.Date.ToShortDateString();
                bool isT = false;
                
                for (int i = 0; i < 8; i++)
                {
                    date1 = date1.AddDays((double)1);
                    isT = await model.isHoliday(date1);
                    if (isT)
                    {
                        if (i == 0)
                        {
                            if (DateTime.Today.ToShortDateString() != dateTime.Date.ToShortDateString())
                                return dateTime.Date.ToShortDateString() + " is a " + "ערב חג";
                            else return "Today is a " + "ערב חג";
                        }
                        if (DateTime.Today.ToShortDateString() != dateTime.Date.ToShortDateString())
                            return dateTime.Date.ToShortDateString() + " is " + i + " days befor holiday";
                        else  return "Today is " + i + " days befor holiday";
                    }
                    if (DateTime.Today.ToShortTimeString()!=dateTime.Date.ToShortDateString()&&i==7)
                        return dateTime.Date.ToShortDateString() + " is a Regular day";

                }
            }
            catch (FormatException e) { }
           
           

            return "Today is a Regular day";
        }
    }
       
}

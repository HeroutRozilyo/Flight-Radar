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
        #region varable
        model.HolidatModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public HolidayVM()
        {
            model = new model.HolidatModel();
        }
       





        /// <summary>
        /// checking if the daate is holiday or befor
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<string> isHoliday(string date)
        {
            DateTime dateTime=new DateTime();
            try
            {
                 dateTime = DateTime.Parse(date);
                DateTime date1 = dateTime;
                string a = DateTime.Today.ToShortDateString();
                string b = dateTime.Date.ToShortDateString();
                string isT;
                
                for (int i = 0; i < 8; i++)
                {
                    date1 = dateTime.AddDays((double)i);
                    isT =  await model.isHoliday(date1);
                    if (isT != "no holiday")
                    {
                        if (i == 0)
                        {
                            if (a != b)
                                return dateTime.Date.ToShortDateString() + " is a " + isT; 
                            else return "Today is a " + isT; 
                        }
                        if (a != b)
                            return dateTime.Date.ToShortDateString() + " is " + i + " days befor " + isT; 
                        else return "Today is " + i + " days befor "+ isT;
                    }
                    if (a!=b&&i==7)
                        return dateTime.Date.ToShortDateString() + " is a Regular day";

                }
            }
            catch (FormatException e) { }
           
  
            return "Today is a Regular day";
        }
    }
       
}

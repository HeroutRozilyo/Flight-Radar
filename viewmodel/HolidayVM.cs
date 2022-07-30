using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFlight.viewmodel
{
    public class HolidayVM
    {
        model.HolidatModel model;

        public HolidayVM()
        {
            model = new model.HolidatModel();
        }

        //get a date.. and search with 7 days from now f we have holiday to.
        //public string isHoliday(string date)
        //{
        //    try
        //    {
        //        DateTime dateTime = DateTime.Parse(date);
        //        DateTime date1 = dateTime;
        //        bool isT = false;
        //        for (int i = 0; i < 8; i++)
        //        {
        //            date1 = date1.AddDays((double)i);
        //            isT =  model.isHoliday(date1);
        //            if (isT)
        //            {
        //                if (i == 0)
        //                    return "Today is a " + "ערב חג";
        //                return "This date is " + i + " days befor holiday";
        //            }
        //        }
        //    }
        //    catch (FormatException e) { }
        //    return "Today is a Regular day";
        //}



        public async Task<string> isHoliday(string date)
        {
            try
            {
                DateTime dateTime = DateTime.Parse(date);
                DateTime date1 = dateTime;
                bool isT = false;
                for (int i = 0; i < 8; i++)
                {
                    date1 = date1.AddDays((double)i);
                    isT = await model.isHoliday(date1);
                    if (isT)
                    {
                        if (i == 0)
                            return "Today is a " + "ערב חג";
                        return "This date is " + i + " days befor holiday";
                    }
                }
            }
            catch (FormatException e) { }
            return "Today is a Regular day";
        }
    }
       
}

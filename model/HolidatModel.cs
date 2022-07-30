using HebDates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFlight.model
{
    public class HolidatModel
    {
        HebAdapter heb = new HebAdapter();

        //public bool isHoliday(DateTime date)
        //{
        //    return  heb.IfHoliday(date);
        //}

        public async Task<bool> isHoliday(DateTime date)
        {
            return await heb.IfHoliday(date);
        }
    }
}

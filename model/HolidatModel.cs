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

       

        public async Task<string> isHoliday(DateTime date)
        {
            return await heb.IfHoliday(date);
        }
    }
}

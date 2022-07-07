using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightModel
{
    public class HelperClass
    {
        //convert from unix time to humane datetime
        public DateTime GetDateTimeFromEpohc(double Epoch)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0);// start epoch time
            start = start.AddSeconds(Epoch);//add the second to the start time
            return start;
        }

    }
}

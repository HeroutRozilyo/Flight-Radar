using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEF
{
    public static class Config
    {
        public static int IdFlightCounter;


        static Config()
        {
            IdFlightCounter = 0;
        }
    }
}

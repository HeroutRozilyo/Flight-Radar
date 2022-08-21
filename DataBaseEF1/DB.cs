using FlightModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEF1
{
    public class DB : DbContext
    {
        /// <summary>
        /// Data Base
        /// </summary>
        public DbSet<FlightData> Flights { get; set; }


        public DB() : base()
        {

        }



    }
}

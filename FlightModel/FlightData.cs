using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightModel
{
    public class FlightData
    {
        public int Id { get; set; }
        public string SourceId { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public DateTime DataAndTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        [Key]
        public string FlightCode { get; set; }

    }
}

using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Reader.Models
{
    public class Taxi
    {
        public string Id { get; set; }
        public string PickUpTime { get; set; }
        public string DropOffTime { get; set; }
        public string? PassengerCount { get; set; }
        public double TripDistance { get; set; }
        public string? StoreAndFwdFlag { get; set; }
        public ushort PickUpLocationId { get; set; }
        public ushort DropOffLocationId { get; set; }
        public decimal FareAmount { get; set; }
        public decimal TipAmount { get; set; }

    }
}

using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CSV_Reader.Models
{
    public class Cab
    {
        [Name("tpep_pickup_datetime")]
        public DateTime PickUpTime { get; set; }
        [Name("tpep_dropoff_datetime")]
        public DateTime DropOffTime { get; set; }
        [Name("passenger_count")]
        public string? PassengerCount { get; set; }
        [Name("trip_distance")]
        public double TripDistance { get; set; }
        [Name("store_and_fwd_flag")]
        public string? StoreAndFwdFlag { get; set; }
        [Name("PULocationID")]
        public ushort PickUpLocationId { get; set; }
        [Name("DOLocationID")]
        public ushort DropOffLocationId { get; set; }
        [Name("fare_amount")]
        public decimal FareAmount { get; set; }
        [Name("tip_amount")]
        public decimal TipAmount { get; set; }

    }
}

using CsvHelper.Configuration.Attributes;

namespace CSV_Reader.Models
{
    public class CabRecord
    {
        [Name("tpep_pickup_datetime")]
        public string PickUpTime { get; set; }
        [Name("tpep_dropoff_datetime")]
        public string DropOffTime { get; set; }
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

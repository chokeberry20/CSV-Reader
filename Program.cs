using CSV_Reader.Models;
using CsvHelper;
using System.Globalization;

namespace CSV_Reader
{
    public class Program
    {
        //Change this to your absolute path to .csv file
        public const string absolutePath = "Csv_Reader\\Files\\sample-cab-data.csv";
        //Chage this to your destination folder
        public const string duplicatePath = "Csv_Reader\\Files\\duplicate.csv";

        public static void Main(string[] args)
        {
            var allRecordsFromCsv = CsvRead(absolutePath);

            CsvWriteDuplicate(allRecordsFromCsv);

            var taxiUniqueList = FindUniqueCabs(allRecordsFromCsv);

            ConvertStoreAndFwdFlagData(taxiUniqueList);

            var taxisWithIdList = AddId(taxiUniqueList);

            using (ApplicationContext db = new ApplicationContext())
            {
                db.AddRange(taxisWithIdList);
                db.SaveChanges();
            }
        }

        static public IEnumerable<TaxiRecord> CsvRead(string path)
        {
            var list = new List<TaxiRecord>();

            using (var reader = new StreamReader(path))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<TaxiRecord>();

                foreach (var record in records)
                {
                    list.Add(record);
                }
            }

            return list;
        }

        static public IEnumerable<Taxi> AddId(IEnumerable<TaxiRecord> cabRecords)
        {
            var list = new List<Taxi>();

            foreach (var cabRecord in cabRecords)
            {
                var cab = new Taxi();
                cab.Id = Guid.NewGuid().ToString().Trim();
                cab.PickUpTime = cabRecord.PickUpTime.Trim();
                cab.DropOffTime = cabRecord.DropOffTime.Trim();
                cab.PassengerCount = cabRecord.PassengerCount;
                cab.TripDistance = cabRecord.TripDistance;
                cab.StoreAndFwdFlag = cabRecord.StoreAndFwdFlag;
                cab.PickUpLocationId = cabRecord.PickUpLocationId;
                cab.DropOffLocationId = cabRecord.DropOffLocationId;
                cab.FareAmount = cabRecord.FareAmount;
                cab.TipAmount = cabRecord.TipAmount;

                list.Add(cab);
            }

            return list;
        }

        static public IEnumerable<TaxiRecord> ConvertStoreAndFwdFlagData(IEnumerable<TaxiRecord> list)
        {
            foreach (var cab in list)
            {
                cab.StoreAndFwdFlag = cab.StoreAndFwdFlag.Replace("N", "No").Trim();
                cab.StoreAndFwdFlag = cab.StoreAndFwdFlag.Replace("Y", "Yes").Trim();
            }

            return list;
        }

        public static IEnumerable<TaxiRecord> FindUniqueCabs(IEnumerable<TaxiRecord> list)
        {
            var uniqueList = list.GroupBy(c => new { c.PickUpTime, c.DropOffTime, c.PassengerCount })
                       .Where(g => g.Count() >= 1)
                       .Select(g => g.First())
                       .ToList();

            return uniqueList;
        }

        public static void CsvWriteDuplicate(IEnumerable<TaxiRecord> allRecords)
        {
            var duplicateList = allRecords.GroupBy(c => new { c.PickUpTime, c.DropOffTime, c.PassengerCount })
                       .Where(g => g.Count() > 1)
                       .Select(g => g.First());

            using (StreamWriter writer = new StreamWriter(duplicatePath))
            using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(duplicateList);
            }
        }
    }
}

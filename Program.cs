using CSV_Reader.Models;
using CsvHelper;
using System.Globalization;

namespace CSV_Reader
{
    public class Program
    {
        public const string absolutePath = "C:\\Programming\\project\\CSV_Reader\\Files\\sample-cab-data.csv";
        public const string duplicatePath = "C:\\Programming\\project\\CSV_Reader\\Files\\duplicate.csv";

        public static void Main(string[] args)
        {
            var allRecords = CsvRead(absolutePath);

            CsvWriteDuplicate(allRecords);

            var cabUniqueList = FindUniqueCabs(allRecords);

            ConvertStoreAndFwdFlagData(cabUniqueList);

            var cabsWithIdList = AddId(cabUniqueList);

            using (ApplicationContext db = new ApplicationContext())
            {
                db.AddRange(cabsWithIdList);
                db.SaveChanges();
            }
        }

        static public IEnumerable<CabRecord> CsvRead(string path)
        {
            var list = new List<CabRecord>();

            using (var reader = new StreamReader(path))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<CabRecord>();

                foreach (var record in records)
                {
                    list.Add(record);
                }
            }

            return list;
        }

        static public IEnumerable<Cab> AddId(IEnumerable<CabRecord> cabRecords)
        {
            var list = new List<Cab>();

            foreach (var cabRecord in cabRecords)
            {
                var cab = new Cab();
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

        static public IEnumerable<CabRecord> ConvertStoreAndFwdFlagData(IEnumerable<CabRecord> list)
        {
            foreach (var cab in list)
            {
                cab.StoreAndFwdFlag = cab.StoreAndFwdFlag.Replace("N", "No").Trim();
                cab.StoreAndFwdFlag = cab.StoreAndFwdFlag.Replace("Y", "Yes").Trim();
            }

            return list;
        }

        public static IEnumerable<CabRecord> FindUniqueCabs(IEnumerable<CabRecord> list)
        {
            var uniqueList = list.GroupBy(c => new { c.PickUpTime, c.DropOffTime, c.PassengerCount })
                       .Where(g => g.Count() >= 1)
                       .Select(g => g.First())
                       .ToList();

            return uniqueList;
        }

        public static void CsvWriteDuplicate(IEnumerable<CabRecord> allRecords)
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

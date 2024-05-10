using CSV_Reader.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;

namespace CSV_Reader
{
    public class Program
    {
        static void Main(string[] args)
        {
            const string absolutePath = "C:\\Users\\andri\\Downloads\\sample-cab-data.csv";           

            var list = CsvRead(absolutePath);

            
        }

        static public IEnumerable<Cab> CsvRead(string path)
        {
            var list = new List<Cab>();

            using (var reader = new StreamReader(path))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<Cab>();

                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var item in records)
                    {
                        list.Add(item);
                    }
                    foreach (var item in list)
                    {
                        db.Cabs.Add(item);
                    }
                    db.SaveChanges();
                }

                
            }

            return list;
        }
    }
}

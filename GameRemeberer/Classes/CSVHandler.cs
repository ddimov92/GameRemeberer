using CsvHelper.Configuration.Attributes;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace GameRemeberer
{
    public class Game
    {
        public int ID { get; set; }
        [Index(0)]
        public string Name { get; set; }
        [Index(1)]
        public string Platform { get; set; }
        [Index(2)]
        public string ReleaseDate { get; set; }
        [Index(4)]
        public string Rating { get; set; }
        [Index(7)]
        public string Owned { get; set; }

    }
    
    public static class CSVHandler
    {

        public static void ReadFromCSV(List<Game> games)
        {
            string url = $@"{AppDomain.CurrentDomain.BaseDirectory}\Files\allgames.csv";
            using (var reader = new StreamReader(url))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = csv.GetRecord<Game>();
                    games.Add(record);
                }
            }
        }

    }      
}

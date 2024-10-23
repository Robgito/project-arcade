using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project_3___Arcade
{
    internal class Cooldown
    {
        private int _idGebruiker;
        private DateTime _startDatum;

        public Cooldown() { }
        public Cooldown(int idGebruiker, DateTime startDatum)
        {
            IDGebruiker = idGebruiker;
            StartDatum = startDatum;
        }

        public int IDGebruiker { get { return _idGebruiker; } set { _idGebruiker = value; } }
        public DateTime StartDatum { get { return _startDatum; } set { _startDatum = value; } }

        public static Cooldown GetDatum(int eenGebruikersID)
        {
            List<Cooldown> lijstDatums = GetDatums();
            Cooldown eenDatum = null;

            if (lijstDatums != null)
            {
                foreach (Cooldown datum in lijstDatums)
                {
                    if (datum.IDGebruiker == eenGebruikersID)
                    {
                        eenDatum = datum;
                        break;
                    }
                }
            }
            return eenDatum;
        }
        public static List<Cooldown> GetDatums()
        {
            List<Cooldown> lijstDatums = new List<Cooldown>();

            if (System.IO.File.Exists("Cooldowns.json"))
            {
                using (StreamReader r = new StreamReader("Cooldowns.json"))
                {
                    string json = r.ReadToEnd();
                    lijstDatums = JsonSerializer.Deserialize<List<Cooldown>>(json);
                }
            }
            else
            {
                /*throw new Exception("Er zijn geen gebruikers gevonden.");*/
                lijstDatums = null;
            }
            return lijstDatums;
        }

        public static bool UpdateDatum(Cooldown eenDatum)
        {
            bool updateSucceeded = false;

            if (System.IO.File.Exists("Cooldowns.json"))
            {
                List<Cooldown> lijstDatums = GetDatums();

                foreach (Cooldown datum in lijstDatums)
                {
                    if (datum.IDGebruiker == eenDatum.IDGebruiker)
                    {
                        datum.StartDatum = eenDatum.StartDatum;

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstDatums, options);
                        File.WriteAllText("Cooldowns.json", json);

                        updateSucceeded = true;
                    }
                }
            }
            return updateSucceeded;
        }

        public static bool DeleteDatum(Cooldown eenDatum)
        {
            bool deleteSucceeded = false;

            if (System.IO.File.Exists("Cooldowns.json"))
            {
                List<Cooldown> lijstDatums = GetDatums();

                foreach (Cooldown datum in lijstDatums)
                {
                    if (datum.IDGebruiker == eenDatum.IDGebruiker)
                    {
                        lijstDatums.Remove(datum);

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstDatums, options);
                        File.WriteAllText("Cooldowns.json", json);

                        deleteSucceeded = true;
                        break;
                    }
                }
            }
            return deleteSucceeded;
        }

        public static bool InsertDatum(Cooldown eenDatum)
        {
            bool insertSucceeded = false;

            if (System.IO.File.Exists("Cooldowns.json"))
            {
                List<Cooldown> lijstDatums = GetDatums();
                lijstDatums.Add(eenDatum);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstDatums, options);
                File.WriteAllText("Cooldowns.json", json);

                insertSucceeded = true;
            }
            else
            {
                List<Cooldown> lijstDatums = new List<Cooldown>();
                lijstDatums.Add(eenDatum);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstDatums, options);
                File.WriteAllText("Cooldowns.json", json);

                insertSucceeded = true;
            }

            return insertSucceeded;
        }
    }
}

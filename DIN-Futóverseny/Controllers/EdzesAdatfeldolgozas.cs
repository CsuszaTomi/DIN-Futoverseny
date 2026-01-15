using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIN_Futóverseny.Models;

namespace DIN_Futóverseny.Controllers
{
    internal class EdzesekAdatfeldolgozas
    {
        public static void EdzesSave(List<Edzes_adatok> adatok,Users user)
        {
            List<string> sorok = new List<string>();
            foreach (Edzes_adatok edzes in adatok)
            {
                sorok.Add($"{user.Nev};{edzes.Datum};{edzes.Tavolsag};{edzes.Idotartam};{edzes.Max_pulzus}");
            }
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\.."));
            string filePath = Path.Combine(projectPath, "adatok.txt");
            File.WriteAllLines(filePath, sorok, System.Text.Encoding.UTF8);
        }
        public static void VersenyAdafelvetel(List<Edzes_adatok> adatok,Users user)
        {
            try
            {
                Text.WriteLine("Felhasználó adatatinak felvitele!: ");
                Text.WriteLine("Kérem a dátumot (yyyy-mm-dd) formátumban: ");
                //DateTime datum = DateTime.Parse(Console.ReadLine());
                string datum = Console.ReadLine();
                while(!Ellenorzo.DateTimeEllenorzo(datum))
                {
                    Text.Write("Hibás formátum! Add meg újra: ");
                    datum = Console.ReadLine();
                }

                Text.WriteLine("Kérem a tavolságot: ");
                string tav = (Console.ReadLine());
                while (!Ellenorzo.DecimalSzamEllenorzo(tav))
                {
                    Text.Write("Hibás formátum! Add meg újra: ");
                    tav = Console.ReadLine();
                }

                Text.WriteLine("Kérem az időtatamot (óó:pp:mm) formátumban: ");
                string idotartam = Console.ReadLine();
                while(!Ellenorzo.TimeSpanEllenorzo(idotartam))
                {
                    Text.Write("Hibás formátum! Add meg újra: ");
                    idotartam = Console.ReadLine();
                }

                Text.WriteLine("Kérem a maximális pulzus adatot: ");
                string m_pulzus = Console.ReadLine();
                while (!Ellenorzo.IntSzamEllenorzo(m_pulzus))
                {
                    Text.Write("Hibás formátum! Add meg újra: ");
                    m_pulzus = Console.ReadLine();
                }

                Edzes_adatok adat = new Edzes_adatok(DateTime.Parse(datum), Decimal.Parse(tav), TimeSpan.Parse(idotartam),int.Parse( m_pulzus));

                adatok.Add(adat);
                EdzesSave(adatok, user);
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba történt: ", e);
            }
        }
    }
}

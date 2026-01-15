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
                Console.WriteLine("Felhasználó adatatinak felvitele!: ");
                Console.WriteLine("Kérem a dátumot (yyyy-mm-dd) formátumban: ");
                DateTime datum = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Kérem a tavolságot: ");
                decimal tav = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Kérem az időtatamot (óó:pp:mm) formátumban: ");
                TimeSpan idotartam = TimeSpan.Parse(Console.ReadLine());

                Console.WriteLine("Kérem a maximális pulzus adatot: ");
                int m_pulzus = int.Parse(Console.ReadLine());

                Edzes_adatok adat = new Edzes_adatok(datum, tav, idotartam, m_pulzus);

                EdzesSave(adatok, user);
                adatok.Add(adat);
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba történt: ", e);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIN_Futóverseny.Models;

namespace DIN_Futóverseny.Controllers
{
    internal class EdzesekAdatfeldolgozás
    {
        public static void VersenyAdafelvetel(List<Edzes_adatok> adatok)
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

                string kiirni = $"{datum};{tav};{idotartam};{m_pulzus}";
                File.AppendAllText("adatok.txt", kiirni);
                adatok.Add(adat);
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba történt: ", e);
            }
        }
    }
}

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
        /// <summary>
        /// Edzés adatok mentése fájlba
        /// </summary>
        /// <param name="adatok">Az edzés adatok lista</param>
        /// <param name="user">A bejelentkezett felhasználó</param>
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

        /// <summary>
        /// Edzés adatok beolvasása fájlból
        /// </summary>
        /// <returns>A beolvasott sorokat</returns>

        public static string[] EdzesBeolvasó()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\.."));
            string filePath = Path.Combine(projectPath, "adatok.txt");
            string[] sorok = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
            return sorok;
        }

        /// <summary>
        /// Edzés adatainak felvétele
        /// </summary>
        /// <param name="adatok">Az edzés adatok lista</param>
        /// <param name="user">A bejelentkezett felhasználó</param>
        public static void VersenyAdafelvetel(List<Edzes_adatok> adatok,Users user)
        {
            try
            {
                Console.Clear();
                Text.WriteLine("Futás adatainak felvétele", "red");
                Text.WriteLine("--------------------");
                Text.Write("Kérem a dátumot (yyyy-mm-dd) formátumban: ");
                //DateTime datum = DateTime.Parse(Console.ReadLine());
                string datum = Console.ReadLine();
                while(!Ellenorzo.DateTimeEllenorzo(datum))
                {
                    Text.Write("Hibás formátum! Add meg újra: ","red");
                    datum = Console.ReadLine();
                }

                Text.Write("Kérem a tavolságot: ");
                string tav = (Console.ReadLine());
                while (!Ellenorzo.DecimalSzamEllenorzo(tav))
                {
                    Text.Write("Hibás formátum! Add meg újra: ", "red");
                    tav = Console.ReadLine();
                }

                Text.Write("Kérem az időtatamot (óó:pp:mm) formátumban: ");
                string idotartam = Console.ReadLine();
                while(!Ellenorzo.TimeSpanEllenorzo(idotartam))
                {
                    Text.Write("Hibás formátum! Add meg újra: ", "red");
                    idotartam = Console.ReadLine();
                }

                Text.Write("Kérem a maximális pulzus adatot: ");
                string m_pulzus = Console.ReadLine();
                while (!Ellenorzo.IntSzamEllenorzo(m_pulzus))
                {
                    Text.Write("Hibás formátum! Add meg újra: ", "red");
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

        /// <summary>
        /// Edzés adatok megjelenítése
        /// </summary>
        /// <param name="username">A bejelentkezett felhasználó neve</param>
        public static void Megjelenites(string username)
        {
            try
            {
               
                string[] sorok = EdzesBeolvasó();
                List<string> adatok = new List<string>();
                foreach (string sor in sorok)
                {
                    string[] adatok_egysorban = sor.Split(';');
                    if (adatok_egysorban[0] == username)
                    {
                        adatok.Add(adatok_egysorban[1]);
                        adatok.Add(adatok_egysorban[2]);
                        adatok.Add(adatok_egysorban[3]);
                    }




                }
                
                foreach (string s in adatok)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine(username);
                Console.WriteLine();

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba történt: ", e);
                Console.ReadLine();
            }

        }
    }
}

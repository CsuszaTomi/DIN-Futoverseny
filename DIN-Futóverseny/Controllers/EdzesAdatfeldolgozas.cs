using DIN_Futóverseny.Models;
using DIN_Futóverseny.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
                sorok.Add($"{edzes.Nev};{edzes.Datum.ToShortDateString()};{edzes.Tavolsag};{edzes.Idotartam};{edzes.Max_pulzus};{edzes.Nyug_pulzus};{edzes.Testsuly}");
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
        /// Létrehozza a futás objekutumokat a beolvasott adatok alapján
        /// </summary>
        /// <returns>A beolvasott edzési adatokat tartalmazó listát</returns>
        public static List<Edzes_adatok> EdzesFeldolgozo()
        {
            string[] sorok = EdzesBeolvasó();
            List<Edzes_adatok> edzesek = new List<Edzes_adatok>();
            foreach (string sor in sorok)
            {
                string[] adatok = sor.Split(';');
                string nev = adatok[0];
                DateTime datum = DateTime.Parse(adatok[1]);
                decimal tavolsag = Decimal.Parse(adatok[2]);
                TimeSpan idotartam = TimeSpan.Parse(adatok[3]);
                int max_pulzus = int.Parse(adatok[4]);
                int nyug_pulzus = int.Parse(adatok[5]);
                double testsuly = double.Parse(adatok[6]);
                edzesek.Add(new Edzes_adatok(nev,datum, tavolsag, idotartam, max_pulzus, nyug_pulzus, testsuly));
            }
            return edzesek;
        }

        /// <summary>
        /// Edzés adatainak felvétele
        /// </summary>
        /// <param name="adatok">Az edzés adatok lista</param>
        /// <param name="user">A bejelentkezett felhasználó</param>
        public static List<Edzes_adatok> VersenyAdafelvetel(List<Edzes_adatok> adatok,Users user)
        {
            try
            {
                Console.Clear();
                Text.WriteLine("Futás adatainak felvétele", "red");
                Text.WriteLine("====================");
                Text.Write("Kérem a futás dátumát (yyyy-mm-dd) formátumban: ");
                //DateTime datum = DateTime.Parse(Console.ReadLine());
                string datum = Console.ReadLine();
                while(!Ellenorzo.DateTimeEllenorzo(datum))
                {
                    Text.Write("Hibás formátum! Add meg újra: ","red");
                    datum = Console.ReadLine();
                }

                Text.Write("Kérem a futott távolságot (km): ");
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

                Text.Write("Kérem a futás utáni nyugalmi pulzust: ");
                string n_pulzus = Console.ReadLine();
                while (!Ellenorzo.IntSzamEllenorzo(n_pulzus))
                {
                    Text.Write("Hibás formátum! Add meg újra: ", "red");
                    n_pulzus = Console.ReadLine();
                }

                Text.Write("Kérem a futás utáni testsúlyt (kg): ");
                string testsuly = Console.ReadLine();
                while (!Ellenorzo.DoubleEllenorzo(testsuly))
                {
                    Text.Write("Hibás formátum! Add meg újra: ", "red");
                    testsuly = Console.ReadLine();
                }

                Edzes_adatok adat = new Edzes_adatok(user.Nev,DateTime.Parse(datum), Decimal.Parse(tav), TimeSpan.Parse(idotartam), int.Parse(m_pulzus), int.Parse(n_pulzus), double.Parse(testsuly));
                adatok.Add(adat);
                return adatok;
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba történt: ", e);
                return null;
            }
        }

        /// <summary>
        /// Atlagsebesség kiszámolása egy felhasználó edzései alapján
        /// </summary>
        /// <param name="edzesek">A felhasználó edzései</param>
        /// <param name="user">A bejelentkezett felhasználó</param>
        /// <returns>A felhasználó átlagsebessége</returns>
        public static double AtlagSebesseg(List<Edzes_adatok> edzesek, Users user)
        {
            List<Edzes_adatok> useredzesek = EdzesFeldolgozo();
            foreach (Edzes_adatok edzes in edzesek)
            {
                if(user.Nev == edzes.Nev)
                    useredzesek.Add(edzes);
            }
            double osszSebesseg = 0;
            foreach (Edzes_adatok edzes in useredzesek)
            {
                double tavolsag = (double)edzes.Tavolsag;
                double idotartram = edzes.Idotartam.TotalHours;
                double sebesseg = tavolsag / idotartram;
                osszSebesseg += sebesseg;
            }
            return osszSebesseg / useredzesek.Count;
        }

        /// <summary>
        /// Kiszámolja egy edzés átlagsebességét
        /// </summary>
        /// <param name="edzes">A futás adatai</param>
        /// <returns>A futás átlagsebessége</returns>
        public static double EdzesAtlagSebesseg(Edzes_adatok edzes)
        {
            double tavolsag = (double)edzes.Tavolsag;
            double idotartram = edzes.Idotartam.TotalHours;
            double sebesseg = tavolsag / idotartram;
            return sebesseg;
        }

        public static int EdzesSzamlalo(Users user)
        {
            List<Edzes_adatok> osszesEdzes = EdzesFeldolgozo();
            int szamlalo = 0;
            foreach(Edzes_adatok edzes in osszesEdzes)
            {
                if(edzes.Nev == user.Nev)
                {
                    szamlalo++;
                }
            }
            return szamlalo;
        }

        /// <summary>
        /// Kiírja a felhasználó statisztikáit
        /// </summary>
        /// <param name="edzesek">A felhasználó edzései</param>
        /// <param name="user">A bejelentkezett felhasználó</param>
        public static void Statisztikak(List<Edzes_adatok> edzesek, Users user)
        {
            Console.Clear();
            Text.WriteLine("Statisztikák", "red");
            Text.WriteLine("====================");
            double atlagSebesseg = AtlagSebesseg(edzesek, user);
            Text.WriteLine($"Az átlagos futási sebességed: {atlagSebesseg:F2} km/h");
            Text.WriteLine($"A célodnak({user.Altcel} km/futás) ennyiszer feleltél meg: {Szamlalo(user)}/{EdzesSzamlalo(user)}");
            Text.WriteLine($"Összesen ennyit futottál: {Ossz(user.Nev)} km");
            Text.WriteLine($"Összes edzésidőd(nap:óra:perc:másodperc): {OsszEdzesIdo(user.Nev)}");
            Text.WriteLine("Enterrel vissza.....", "yellow");
            Console.ReadLine();
        }

        /// <summary>
        /// Edzés adatok megjelenítése
        /// </summary>
        /// <param name="username">A bejelentkezett felhasználó neve</param>
        public static void Megjelenites(string username, List<Edzes_adatok> useredzesek)
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
                        adatok.Add(adatok_egysorban[4]);
                        adatok.Add(adatok_egysorban[5]);
                        adatok.Add(adatok_egysorban[6]);
                        adatok.Add(EdzesAtlagSebesseg(new Edzes_adatok(adatok_egysorban[0], DateTime.Parse(adatok_egysorban[1]), Decimal.Parse(adatok_egysorban[2]), TimeSpan.Parse(adatok_egysorban[3]), int.Parse(adatok_egysorban[4]), int.Parse(adatok_egysorban[5]), double.Parse(adatok_egysorban[6]))).ToString("F2"));
                    }
                }
                //Console.WriteLine("---------------------------------------------------------------------");
                //for (int i = 0; i < adatok.Count / 4; i++)
                //{
                //    string adat1 = adatok[i * 4 + 0]; 
                //    string adat2 = adatok[i * 4 + 1]; 
                //    string adat3 = adatok[i * 4 + 2]; 
                //    string adat4 = adatok[i * 4 + 3]; 
                //    // Kiíratás táblázatosan
                //    Console.WriteLine($"| {adat1,-15} | {adat2,-15} | {adat3,-10} | {adat4,-10} |");
                //}
                //Console.WriteLine("---------------------------------------------------------------------");
                Console.Clear();
                Text.WriteLine("Futások", "red");
                Text.WriteLine("====================");
                Tables.EdzesTablazatMegjelenites(adatok);
                Text.WriteLine("");
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba történt: ", e);
                Console.WriteLine(e);
                Console.ReadLine();
            }

        }

        /// <summary>
        /// Adott felhasználó edzésének törlése
        /// </summary>
        /// <param name="username">A törölni kívánt edzés felhasználóneve</param>
        /// <param name="useredzesek">A felhasználó edzései</param>
        public static void Torol(string username, List<Edzes_adatok> useredzesek)
        {
            try
            {
          
                Megjelenites(username, null);

                Text.Write("\nAdd meg a törölni kívánt sorszámot: ");
               
                int index=int.Parse(Console.ReadLine());
                index = index - 1;

            
                List<Edzes_adatok> osszesEdzes = EdzesFeldolgozo();

          
                List<Edzes_adatok> sajatEdzesek = new List<Edzes_adatok>();
                foreach (var edzes in osszesEdzes)
                {
                    if (edzes.Nev == username)
                    {
                        sajatEdzesek.Add(edzes);
                    }
                }

               
                    Edzes_adatok torlendoElem = sajatEdzesek[index];

                    osszesEdzes.Remove(torlendoElem);

                
                    EdzesSave(osszesEdzes, null);

                    Text.WriteLine("Sikeres törlés!");
              
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba történt: " + e.Message);
            }
        }

        public static string OsszEdzesIdo(string username)
        {
            TimeSpan osszIdo = TimeSpan.Zero;
            List<Edzes_adatok> osszesEdzes = EdzesFeldolgozo();
            foreach (var edzes in osszesEdzes)
            {
                if (username == edzes.Nev)
                {
                    osszIdo += edzes.Idotartam;
                }
            }
            return string.Format("{0}:{1:D2}:{2:D2}:{3:D2}", (int)osszIdo.TotalDays, osszIdo.Hours, osszIdo.Minutes, osszIdo.Seconds);
        }

        /// <summary>
        /// Adott felhasználó edzésének módosítása
        /// </summary>
        /// <param name="username">A módosítani kívánt edzés felhasználóneve</param>
        public static void Modosit(string username)
        {
            try
            {
             
                Megjelenites(username, null);

                Text.Write("\nAdd meg a módosítani kívánt edzés sorszámát: ");
                if (!int.TryParse(Console.ReadLine(), out int index))
                {
                    Text.WriteLine("Hibás adat! Számot adj meg.");
                    return;
                }
                index = index - 1;

            
                List<Edzes_adatok> osszesEdzes = EdzesFeldolgozo();

               
                List<Edzes_adatok> sajatEdzesek = new List<Edzes_adatok>();
                foreach (var edzes in osszesEdzes)
                {
                    if (edzes.Nev == username)
                    {
                        sajatEdzesek.Add(edzes);
                    }
                }

              
           
                    Edzes_adatok szerkesztendo = sajatEdzesek[index];
                   
                    bool kilepes = false;

                    while (!kilepes)
                    {
                        
                        Console.Clear();
                        int modositchoice = Text.ArrowMenu(new string[] { "Dátum", "Távolság", "Időtartam", "Max pulzus", "Nyugalmi pulzus", "Testsúly", "MENTÉS és Kilépés" }, "Futás módosítás");

                        switch (modositchoice)
                        {
                            case 0:
                                Text.WriteLine($"Jelenlegi: {szerkesztendo.Datum.ToShortDateString()}.");
                                Text.Write("Add meg az újat (yyyy-mm-dd): ");
                                string ujDatum = Console.ReadLine();
                                while (!Ellenorzo.DateTimeEllenorzo(ujDatum))
                                {
                                    Text.WriteLine("");
                                    Text.Write("Hibás formátum! Add meg újra: ", "red");
                                    ujDatum = Console.ReadLine();
                                }
                                szerkesztendo.Datum = DateTime.Parse(ujDatum);
                             
                                break;

                            case 1: 
                                Text.WriteLine($"Jelenlegi: {szerkesztendo.Tavolsag} km.");
                                Text.Write("Add meg az újat: ");
                                string ujTav = Console.ReadLine();
                                while (!Ellenorzo.DecimalSzamEllenorzo(ujTav))
                                {
                                    Text.WriteLine("");
                                    Text.Write("Hibás formátum! Add meg újra: ", "red");
                                    ujTav = Console.ReadLine();
                                }
                                szerkesztendo.Tavolsag = decimal.Parse(ujTav);
                           
                                break;

                            case 2: 
                                Text.WriteLine($"Jelenlegi: {szerkesztendo.Idotartam}.");
                                Text.Write("Add meg az újat (óó:pp:mm): ");
                                string ujIdo = Console.ReadLine();
                                while (!Ellenorzo.TimeSpanEllenorzo(ujIdo))
                                {
                                    Text.WriteLine("");
                                    Text.Write("Hibás formátum! Add meg újra: ", "red");
                                    ujIdo = Console.ReadLine();
                                }
                                szerkesztendo.Idotartam = TimeSpan.Parse(ujIdo);
                            
                                break;

                            case 3: 
                                Text.WriteLine($"Jelenlegi: {szerkesztendo.Max_pulzus}.");
                                Text.Write("Add meg az újat: ");
                                string ujMaxP = Console.ReadLine();
                                while (!Ellenorzo.IntSzamEllenorzo(ujMaxP))
                                {
                                    Text.WriteLine("");
                                    Text.Write("Hibás formátum! Add meg újra: ", "red");
                                    ujMaxP = Console.ReadLine();
                                }
                                szerkesztendo.Max_pulzus = int.Parse(ujMaxP);
                             
                                break;

                            case 4: 
                                Text.WriteLine($"Jelenlegi: {szerkesztendo.Nyug_pulzus}.");
                                Text.Write("Add meg az újat: ");
                                string ujNyugP = Console.ReadLine();
                                while (!Ellenorzo.IntSzamEllenorzo(ujNyugP))
                                {
                                    Text.WriteLine("");
                                    Text.Write("Hibás formátum! Add meg újra: ", "red");
                                    ujNyugP = Console.ReadLine();
                                }
                                szerkesztendo.Nyug_pulzus = int.Parse(ujNyugP);
                               
                                break;

                            case 5: 
                                Text.WriteLine($"Jelenlegi: {szerkesztendo.Testsuly} kg.");
                                Text.Write("Add meg az újat: ");
                                string ujSuly = Console.ReadLine();
                                while (!Ellenorzo.DoubleEllenorzo(ujSuly))
                                {
                                    Text.Write("Hibás formátum! Add meg újra: ", "red");
                                    ujSuly = Console.ReadLine();
                                }
                                szerkesztendo.Testsuly = double.Parse(ujSuly);
                           
                                break;

                            case 6: 
                                kilepes = true;
                                break;

                            default:
                                Text.WriteLine("Nincs ilyen opció.");
                                break;
                        }
                    }
                Users user = UserActions.GetUser(username);
                osszesEdzes.Remove(szerkesztendo);
                osszesEdzes.Add(szerkesztendo);
                EdzesSave(osszesEdzes, user);
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba történt: " + e.Message);
            }
        }

        /// <summary>
        /// Adott felhasználó összes futott távjának kiszámolása
        /// </summary>
        /// <param name="username">A felhasználó neve</param>
        /// <returns>A felhasználó összes futott távja</returns>
        public static decimal Ossz(string username)
        {
            decimal osszeg = 0;
            List<Edzes_adatok> osszesEdzes = EdzesFeldolgozo();
            foreach (var edzes in osszesEdzes)
            {
                if (username == edzes.Nev)
                {
                    osszeg += edzes.Tavolsag;
                }
            }
            return osszeg;

        }

        /// <summary>
        /// Adott felhasználó edzéseinek átlagsebesség változásának kiíratása
        /// </summary>
        /// <param name="username">A felhasználó neve</param>
        public static void Atlagsebessegvaltozasa(string username)
        {
            List<Edzes_adatok> osszesEdzes = EdzesFeldolgozo();

            Console.Clear();
            Text.WriteLine("Átlagos sebesség változás", "red");
            Text.WriteLine("========================");

            List<Edzes_adatok> sajatEdzesek = new List<Edzes_adatok>();
            foreach (var edzes in osszesEdzes)
            {
                if (edzes.Nev == username) sajatEdzesek.Add(edzes);
            }

            for (int i = 0; i < sajatEdzesek.Count; i++) 
            {
                double mostani = EdzesAtlagSebesseg(sajatEdzesek[i]);
                Text.WriteLine($"{i+1}. futás átlagsebessége: {mostani:F2} km/h");
                if (i > 0)
                {
                    double elozo = EdzesAtlagSebesseg(sajatEdzesek[i - 1]);
                    if (mostani > elozo)
                    {
                        Text.WriteLine($"Ez az előzőhöz képest: {mostani-elozo:F2} km/h-val jobb","green");
                    }
                    else if (mostani < elozo)
                    {
                        Text.WriteLine($"Ez az előzőhöz képest: {(mostani - elozo) * -1:F2} km/h-val rosszabb","red");
                    }
                    else
                    {

                        Text.WriteLine("megegyezik az előzővel" +
                            "", "blue");
                    }

                }
            }
            Text.WriteLine("Enterrel vissza...","yellow");
            Console.ReadLine() ;    
        }

        public static List<Edzes_adatok> EdzesIdobeliRendezo(List<Edzes_adatok> edzesek)
        {
            List<Edzes_adatok> rendezettEdzesek = edzesek.OrderByDescending(edzes => edzes.Datum).ToList();
            return rendezettEdzesek;
        }

        /// <summary>
        /// Adott felhasználó hány alkalommal érte el az adott céltávot
        /// </summary>
        /// <param name="user">A felhasználó</param>
        /// <returns>A felhasználó által elért céltávok száma</returns>
        public static int Szamlalo(Users user)
        {
            List<Edzes_adatok> osszesEdzes = EdzesFeldolgozo();

            int hanyszor = 0;
            
            foreach(var edzes in osszesEdzes)
            {
                if(edzes.Nev == user.Nev)
                {
                    if ((decimal)user.Altcel <= edzes.Tavolsag)
                        {
                            hanyszor++;
                        }
                }
                
            }
            return hanyszor;
        }
    }
}

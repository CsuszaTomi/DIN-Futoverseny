using DIN_Futóverseny.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIN_Futóverseny.Controllers
{
    internal class UserActions
    {
        /// <summary>
        /// Felhasználók beolvasása a fájlból
        /// </summary>
        /// <returns>A felhasználók listája</returns>
        public static List<Users> GetUsers()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\.."));
            string filePath = Path.Combine(projectPath, "users.txt");
            List<Users> users = new List<Users>();
            string[] sorok = File.ReadAllLines(filePath);
            foreach (string sor in sorok)
            {
                if (string.IsNullOrWhiteSpace(sor)) continue;

                try
                {
                    users.Add(new Users(sor));
                }
                catch
                {
                    Text.WriteLine($"Hibás adat a felhasználók között: {sor}", "yellow");
                }
            }

            return users;
        }

        /// <summary>
        /// Új felhasználó regisztrációja
        /// </summary>
        /// <param name="users">A felhasználók listája</param>
        /// <returns>A regisztrált felhasználók listája</returns>
        public static List<Users> Register(List<Users> users)
        {
            Console.Clear();
            Text.WriteLine("Regisztráció", "red");
            Text.WriteLine("====================");
            Text.Write("Név: ");
            string nev = Console.ReadLine();
            while(!Ellenorzo.UressegEllenorzo(nev))
            {
                Text.WriteLine("A név nem lehet üres!", "red");
                Text.Write("Név: ");
                nev = Console.ReadLine();
            }
            foreach (Users user in users)
            {
                while (user.Nev == nev)
                {
                    Text.WriteLine("Ez a név már foglalt!", "red");
                    Text.Write("Név: ");
                    nev = Console.ReadLine();
                }
            }
            Text.Write("Jelszó: ");
            string jelszo = Console.ReadLine();
            while (!Ellenorzo.UressegEllenorzo(jelszo))
            {
                Text.WriteLine("A jelszó nem lehet üres!", "red");
                Text.Write("Jelszó: ");
                jelszo = Console.ReadLine();
            }
            Text.Write("Születési dátum (yyyy-mm-dd): ");
            string szuldatum = Console.ReadLine();
            while (!Ellenorzo.DateTimeEllenorzo(szuldatum))
            {
                Text.WriteLine("Érvénytelen születési dátum!", "red");
                Text.Write("Születési dátum (yyyy-mm-dd): ");
                szuldatum = Console.ReadLine();
            }
            Text.Write("Testsúly (kg): ");
            string testsuly = Console.ReadLine();
            while (!Ellenorzo.DoubleEllenorzo(testsuly))
            {
                Text.WriteLine("Érvénytelen testsúly!", "red");
                Text.Write("Testsúly (kg): ");
                testsuly = Console.ReadLine();
            }
            Text.Write("Magasság (cm): ");
            string magassag = Console.ReadLine();
            while (!Ellenorzo.DoubleEllenorzo(magassag))
            {
                Text.WriteLine("Érvénytelen magasság!", "red");
                Text.Write("Magasság (cm): ");
                magassag = Console.ReadLine();
            }
            Text.Write("Nyugalmi pulzus (bpm): ");
            string nyugpul = Console.ReadLine();
            while (!Ellenorzo.DoubleEllenorzo(nyugpul))
            {
                Text.WriteLine("Érvénytelen nyugalmi pulzus!", "red");
                Text.Write("Nyugalmi pulzus (bpm): ");
                nyugpul = Console.ReadLine();
            }
            Text.Write("Általános futás cél km-ben: ");
            string altcel = Console.ReadLine();
            while (!Ellenorzo.DoubleEllenorzo(altcel))
            {
                Text.WriteLine("Érvénytelen általános futás cél!", "red");
                Text.Write("Általános futás cél km-ben: ");
                altcel = Console.ReadLine();
            }
            Users newUser = new Users(nev,jelszo, DateTime.Parse(szuldatum), double.Parse(testsuly), double.Parse(magassag), double.Parse(nyugpul), double.Parse(altcel));
            users.Add(newUser);
            Text.WriteLine("Sikeres regisztráció!", "green");
            UserSave(users);
            return users;
        }

        /// <summary>
        /// Bejelentkezés a rendszerbe
        /// </summary>
        /// <param name="users">A felhasználók listája</param>
        /// <returns>A bejelentkezett felhasználó</returns>
        public static Users Login(List<Users> users)
        {
            Console.Clear();
            Text.WriteLine("Belépés", "red");
            Text.WriteLine("====================");
            Text.Write("Add meg a nevet: ");
            string bnev = Console.ReadLine();
            Text.Write("Add meg a jelszót: ");
            string bjelszo = Console.ReadLine();
            foreach (Users user in users)
            {
                if (bnev == "admin" && bjelszo == "123")
                {
                    LoginAdmin(new string[2] { bnev, bjelszo });
                    return null;
                }
                if (user.Jelszo == bjelszo && user.Nev == bnev)
                {
                    return user;
                }
            }
            return null;
        }

        /// <summary>
        /// Admin bejelentkezés ellenőrzése
        /// </summary>
        /// <param name="stringek"></param>
        public static void LoginAdmin(string[] stringek)
        {
            if (stringek[0] == "admin" && stringek[1] == "123")
                Program.AdminLogin = true;
        }

        /// <summary>
        /// Elmenti a felhasználók adatait a fájlba
        /// </summary>
        /// <remarks>
        /// A felhasználók adatait elmenti a "users.txt" fájlba, UTF-8 kódolással.
        /// </remarks>
        /// <param name="users">A felhasználók listája</param>
        public static void UserSave(List<Users> users)
        {
            List<string> sorok = new List<string>();
            foreach (Users user in users)
            {
                sorok.Add($"{user.Nev};{user.Jelszo};{user.Szuldatum.ToString("yyyy-MM-dd")};{user.Testsuly};{user.Magassag};{user.Nyugpul};{user.Altcel}");
            }
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\.."));
            string filePath = Path.Combine(projectPath, "users.txt");
            File.WriteAllLines(filePath, sorok, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// A felhasználó fiók adatainak módosítása
        /// </summary>
        /// <param name="users">A felhasználók listája</param>
        /// <param name="loggeduser">A bejelentkezett felhasználó</param>
        /// <returns>A módosított felhasználók listája</returns>
        public static List<Users> FiokAdatModositas(List<Users> users, Users loggeduser)
        {
            Console.Clear();
            Text.WriteLine("Fiók adatok módosítása", "red");
            Text.WriteLine("====================");
            Text.WriteLine("Ha valami adatot nem szeretnél módosítani, hagyd üresen és nyomj entert!", "yellow");
            Text.Write("Új jelszó: ");
            string ujJelszo = Console.ReadLine();
            if (ujJelszo != "")
            {
                loggeduser.Jelszo = ujJelszo;
            }
            Text.Write("Új születési dátum: ");
            string ujszuldat = Console.ReadLine();
            if(ujszuldat != "")
            {
                while(!Ellenorzo.DateTimeEllenorzo(ujszuldat))
                { 
                    Text.WriteLine("Érvénytelen születési dátum!", "red");
                    Text.Write("Új születési dátum: ");
                    ujszuldat = Console.ReadLine();
                }
                loggeduser.Szuldatum = DateTime.Parse(ujszuldat);
            }
            Text.Write("Új testsúly (kg): ");
            string ujtestsuly = Console.ReadLine();
            if(ujtestsuly != "")
            {
                while(!Ellenorzo.DoubleEllenorzo(ujtestsuly))
                {
                    Text.WriteLine("Érvénytelen testsúly!", "red");
                    Text.Write("Új testsúly (kg): ");
                    ujtestsuly = Console.ReadLine();
                }
                loggeduser.Testsuly = double.Parse(ujtestsuly);
            }
            Text.Write("Új magasság (cm): ");
            string ujmagassag = Console.ReadLine();
            if(ujmagassag != "")
            {
                while(!Ellenorzo.DoubleEllenorzo(ujmagassag))
                {
                    Text.WriteLine("Érvénytelen magasság!", "red");
                    Text.Write("Új magasság (cm): ");
                    ujmagassag = Console.ReadLine();
                }
                loggeduser.Magassag = double.Parse(ujmagassag);
            }
            Text.Write("Új nyugalmi pulzus (bpm): ");
            string ujnyugpul = Console.ReadLine();
            if(ujnyugpul != "")
            {
                while(!Ellenorzo.DoubleEllenorzo(ujnyugpul))
                {
                    Text.WriteLine("Érvénytelen nyugalmi pulzus!", "red");
                    Text.Write("Új nyugalmi pulzus (bpm): ");
                    ujnyugpul = Console.ReadLine();
                }
                loggeduser.Nyugpul = double.Parse(ujnyugpul);
            }
            Text.Write("Új általános futás cél km-ben: ");
            string ujaltcel = Console.ReadLine();
            if(ujaltcel != "")
            {
                while(!Ellenorzo.DoubleEllenorzo(ujaltcel))
                {
                    Text.WriteLine("Érvénytelen általános futás cél!", "red");
                    Text.Write("Új általános futás cél km-ben: ");
                    ujaltcel = Console.ReadLine();
                }
                loggeduser.Altcel = double.Parse(ujaltcel);
            }
            UserSave(users);
            Text.WriteLine("Sikeres módosítás!", "green");
            Settings.Delay();
            return users;
        }
    }
}

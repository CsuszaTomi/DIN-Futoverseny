using DIN_Futóverseny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIN_Futóverseny.Controllers
{
    internal class AdminActions
    {
        /// <summary>
        /// Kiírja a felhasználók listáját
        /// </summary>
        /// <param name="users">A felhasználók listája</param>
        public static void ListUsers(List<Users> users)
        {
            Console.Clear();
            Text.WriteLine("Felhasználók listája","red");
            Text.WriteLine("====================");
            foreach (var u in users)
            {
                Text.WriteLine($"{u.Nev}");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Felhasználó törlése
        /// </summary>
        /// <param name="users">A felhasználók listája</param>
        /// <returns>Az új felhasználók listája</returns>
        public static List<Users> DeleteUser(List<Users> users, List<Edzes_adatok> osszesedzes)
        {
            Console.Clear();
            ListUsers(users);
            Text.Write("Add meg a törölni kívánt felhasználó nevét: ");
            string nev = Console.ReadLine();
            int szamlalo = 0;
            foreach (Users user in users)
            {
                if (user.Nev == nev)
                {
                    DeleteAllUserRuns(nev, osszesedzes);
                    users.Remove(user);
                    UserActions.UserSave(users);
                    Text.WriteLine("Felhasználó törölve!");
                    Settings.Delay();
                    return users;
                }
                else
                {
                    szamlalo++;
                    if (szamlalo == users.Count)
                    {
                        Text.WriteLine("Nincs ilyen nevű felhasználó!");
                        Settings.Delay();
                        return users;
                    }
                }
            }
            return users;
        }

        public static void DeleteAllUserRuns(string username, List<Edzes_adatok> osszesEdzes)
        {
            osszesEdzes.RemoveAll(edzes => edzes.Nev == username);
            Users user = UserActions.GetUser(username);
            EdzesekAdatfeldolgozas.EdzesSave(osszesEdzes, user);
            Text.WriteLine($"Sikeresen törölted {username} összes futását!", "green");
        }

        /// <summary>
        /// Adott futás törlése
        /// </summary>
        /// <param name="osszesEdzes">Az összes futás listája</param>
        public static void DeleteRuns(List<Edzes_adatok> osszesEdzes)
        {
            Text.WriteLine("Összes futás:", "red");
            for (int i = 0; i < osszesEdzes.Count; i++)
            {
                var edzes = osszesEdzes[i];
                Text.WriteLine($"{i + 1}. {edzes.Nev} - {edzes.Datum.ToShortDateString()} - {edzes.Tavolsag} km", "yellow");
            }

            Text.Write("\nAdd meg a törölni kívánt futás sorszámát: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (index < 0 || index >= osszesEdzes.Count)
            {
                Text.WriteLine("Hibás sorszám!", "red");
                return;
            }

            Edzes_adatok torlendo = osszesEdzes[index];
            osszesEdzes.Remove(torlendo);

            EdzesekAdatfeldolgozas.EdzesSave(osszesEdzes, null);

            Text.WriteLine($"Sikeresen törölted {torlendo.Nev} {torlendo.Datum.ToShortDateString()} futását!", "green");
        }

        /// <summary>
        /// Adott felhasználó futásainak megtekintése
        /// </summary>
        /// <param name="users">A felhasználók listája</param>
        /// <param name="osszesEdzes">Az összes edzés adat</param>
        public static void UserRuns(List<Users> users, List<Edzes_adatok> osszesEdzes)
        {
            Console.Clear();
            Text.WriteLine("Felhasználó futások","red");
            Text.WriteLine("====================");
            Text.Write("Add meg a felhasználó nevét: ");
            string username = Console.ReadLine();
            foreach (Users user in users)
            {
                if(user.Nev == username)
                {
                    Console.Clear();
                    Text.WriteLine($"{user.Nev} futásai", "red");
                    Text.WriteLine("====================");
                    foreach (Edzes_adatok edzes in osszesEdzes)
                    {
                        if(edzes.Nev == user.Nev)
                        {
                            Text.WriteLine($"{edzes.Datum.ToShortDateString()} - {edzes.Tavolsag} km - {edzes.Idotartam}", "blue");
                        }
                    }
                }
            }
            Text.WriteLine("Enterrel vissza...", "yellow");
            Console.ReadLine();
        }

        /// <summary>
        /// Adott felhasználó futásainak módosítása
        /// </summary>
        public static void ModifyUserRun()
        {
            Console.Clear();
            Text.WriteLine("Felhasználó futás módosítása", "red");
            Text.WriteLine("====================");
            Text.Write("Add meg a felhasználó nevét: ");
            string username = Console.ReadLine();
            EdzesekAdatfeldolgozas.Modosit(username);
        }

        /// <summary>
        /// adott felhasználó adatainak módosítása
        /// </summary>
        /// <param name="users">a felhasználók listája</param>
        public static void ModifyUser(List<Users> users)
        {
            Console.Clear();
            Text.WriteLine("Felhasználó módosítása", "red");
            Text.WriteLine("====================");
            Text.Write("Add meg a felhasználó nevét: ");
            string username = Console.ReadLine();
            foreach (Users user in users)
            {
                if (user.Nev == username)
                {
                    Users loggeduser = user;
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
                    if (ujszuldat != "")
                    {
                        while (!Ellenorzo.DateTimeEllenorzo(ujszuldat))
                        {
                            Text.WriteLine("Érvénytelen születési dátum!", "red");
                            Text.Write("Új születési dátum: ");
                            ujszuldat = Console.ReadLine();
                        }
                        loggeduser.Szuldatum = DateTime.Parse(ujszuldat);
                    }
                    Text.Write("Új testsúly (kg): ");
                    string ujtestsuly = Console.ReadLine();
                    if (ujtestsuly != "")
                    {
                        while (!Ellenorzo.DoubleEllenorzo(ujtestsuly))
                        {
                            Text.WriteLine("Érvénytelen testsúly!", "red");
                            Text.Write("Új testsúly (kg): ");
                            ujtestsuly = Console.ReadLine();
                        }
                        loggeduser.Testsuly = double.Parse(ujtestsuly);
                    }
                    Text.Write("Új magasság (cm): ");
                    string ujmagassag = Console.ReadLine();
                    if (ujmagassag != "")
                    {
                        while (!Ellenorzo.DoubleEllenorzo(ujmagassag))
                        {
                            Text.WriteLine("Érvénytelen magasság!", "red");
                            Text.Write("Új magasság (cm): ");
                            ujmagassag = Console.ReadLine();
                        }
                        loggeduser.Magassag = double.Parse(ujmagassag);
                    }
                    Text.Write("Új nyugalmi pulzus (bpm): ");
                    string ujnyugpul = Console.ReadLine();
                    if (ujnyugpul != "")
                    {
                        while (!Ellenorzo.DoubleEllenorzo(ujnyugpul))
                        {
                            Text.WriteLine("Érvénytelen nyugalmi pulzus!", "red");
                            Text.Write("Új nyugalmi pulzus (bpm): ");
                            ujnyugpul = Console.ReadLine();
                        }
                        loggeduser.Nyugpul = double.Parse(ujnyugpul);
                    }
                    Text.Write("Új általános futás cél km-ben: ");
                    string ujaltcel = Console.ReadLine();
                    if (ujaltcel != "")
                    {
                        while (!Ellenorzo.DoubleEllenorzo(ujaltcel))
                        {
                            Text.WriteLine("Érvénytelen általános futás cél!", "red");
                            Text.Write("Új általános futás cél km-ben: ");
                            ujaltcel = Console.ReadLine();
                        }
                        loggeduser.Altcel = double.Parse(ujaltcel);
                    }
                    UserActions.UserSave(users);
                    Text.WriteLine("Sikeres módosítás!", "green");
                    Settings.Delay();
                    break;
                }
            }
        }

        /// <summary>
        /// Kiírja az admin menüt és kezeli a választásokat
        /// </summary>
        /// <param name="users">A felhasználók listája</param>
        /// <param name="osszesEdzes">Az összes edzés adat</param>
        public static void AdminMenu(List<Users> users, List<Edzes_adatok> osszesEdzes)
        {
            while (true)
            {
                users = UserActions.GetUsers();
                osszesEdzes = EdzesekAdatfeldolgozas.EdzesFeldolgozo();
                Console.Clear();
                int choice = Text.ArrowMenu(new string[] { "Felhasználók listázása","Felhasználó módosítása", "Felhasználó törlése","Felhasználó futás megnézése","Futás módosítása", "Futások törlése", "Kilépés" }, "Adminisztrációs Menü");
                switch (choice)
                {
                    case 0:
                        ListUsers(users);
                        break;
                    case 1:
                        ModifyUser(users);
                        break;
                    case 2:
                        users = DeleteUser(users, osszesEdzes);
                        UserActions.UserSave(users);
                        break;
                    case 3:
                        UserRuns(users, osszesEdzes);
                        break;
                    case 4:
                        ModifyUserRun();
                        break;
                    case 5:
                        DeleteRuns(osszesEdzes);
                        break;
                    case 6:
                        return;
                    default:
                        Text.WriteLine("Érvénytelen választás!", "red");
                        Settings.Delay();
                        break;
                }
            }
        }
    }
}

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
            Text.WriteLine("Felhasználók listája");
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
        public static List<Users> DeleteUser(List<Users> users)
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

    }
}

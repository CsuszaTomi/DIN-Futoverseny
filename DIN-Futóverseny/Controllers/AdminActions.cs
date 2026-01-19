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
        public static void ListUsers(List<Users> users)
        {
            Console.Clear();
            Console.WriteLine("Felhasználók listája:");
            foreach (var u in users)
            {
                Console.WriteLine($"{u.Nev} {(u.IsAdmin ? "(Admin)" : "")}");
            }
            Console.ReadLine();
        }
        public static void DeleteUser(List<Users> users)
        {
            Console.Clear();
            ListUsers(users);
            Console.Write("\nAdd meg a törölni kívánt felhasználó nevét: ");
            string nev = Console.ReadLine();

            var user = users.FirstOrDefault(u => u.Nev == nev && !u.IsAdmin);
            if (user != null)
            {
                users.Remove(user);
                UserActions.UserSave(users);
                Console.WriteLine("Felhasználó törölve!");
            }
            else
            {
                Console.WriteLine("Nem található vagy admin felhasználó nem törölhető.");
            }
            Console.ReadLine();
        }
    }
}

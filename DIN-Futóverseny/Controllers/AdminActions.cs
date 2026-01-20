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
            Text.WriteLine("Felhasználók listája");
            Text.WriteLine("====================");
            foreach (var u in users)
            {
                Text.WriteLine($"{u.Nev}");
            }
            Console.ReadLine();
        }
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
    }
}

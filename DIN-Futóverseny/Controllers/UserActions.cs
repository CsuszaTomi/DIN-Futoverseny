using DIN_Futóverseny.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIN_Futóverseny.Controllers
{
    internal class UserActions
    {
        public static List<Users> GetUsers()
        {
            List<Users> users = new List<Users>();
            string[] sorok = File.ReadAllLines("users.txt");
            foreach (string sor in sorok)
            {
                  users.Add(new Users(sor));
            }
            return users;
        }

        public static List<Users> Register(List<Users> users)
        {
            Console.Clear();
            Text.WriteLine("Regisztráció", "red");
            Text.WriteLine("--------------------");
            Text.Write("Név: ");
            string nev = Console.ReadLine();
            Text.Write("Jelszó: ");
            string jelszo = Console.ReadLine();
            Text.Write("Születési dátum (yyyy-mm-dd): ");
            DateTime szuldatum = DateTime.Parse(Console.ReadLine());
            Text.Write("Testsúly (kg): ");
            double testsuly = double.Parse(Console.ReadLine());
            Text.Write("Magasság (cm): ");
            double magassag = double.Parse(Console.ReadLine());
            Text.Write("Nyugalmi pulzus (bpm): ");
            double nyugpul = double.Parse(Console.ReadLine());
            Text.Write("Általános futás cél km-ben: ");
            double altcel = double.Parse(Console.ReadLine());
            Users newUser = new Users(nev,jelszo, szuldatum, testsuly, magassag, nyugpul, altcel);
            users.Add(newUser);
            string kiirni = $"{nev};{jelszo};{szuldatum.ToString("yyyy-MM-dd")};{testsuly};{magassag};{nyugpul};{altcel}";
            File.AppendAllText("users.txt", kiirni);
            Text.WriteLine("Sikeres regisztráció!", "green");
            return users;
        }

        public static bool Login(List<Users> users)
        {
            Console.Clear();
            Text.WriteLine("Belépés", "red");
            Text.WriteLine("--------------------");
            Text.Write("Add meg a nevet: ");
            string bnev = Console.ReadLine();
            Text.Write("Add meg a jelszót: ");
            string bjelszo = Console.ReadLine();
            foreach (Users user in users)
            {
                if (user.Jelszo == bjelszo && user.Nev == bnev)
                    return true;
            }
            return false;
        }
    }
}

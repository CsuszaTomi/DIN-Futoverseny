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
            Text.WriteLine("Sikeres regisztráció!", "green");
            UserSave(users);
            return users;
        }

        public static Users Login(List<Users> users)
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
                {
                    return user;
                }
            }
            return null;
        }

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
    }
}

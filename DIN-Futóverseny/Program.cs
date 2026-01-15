using DIN_Futóverseny.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIN_Futóverseny.Controllers;
using System.Threading;

namespace DIN_Futóverseny
{
    internal class Program
    {
        static List<Edzes_adatok> adatok = new List<Edzes_adatok>();
        static Users loggeduser = null;
        static List<Users> Users = UserActions.GetUsers();
        static void Main(string[] args)
        {
            while (true)
            {
                int menu = Text.ArrowMenu(new string[] { "Új fiók", "Bejelentkezés", "Kilépés" }, "Futás Nyilvántartó");
                switch(menu)
                {
                    case 0:
                        Users = UserActions.Register(Users);
                        break;
                    case 1:
                        loggeduser = UserActions.Login(Users);
                        if (loggeduser != null)
                        {
                            Text.WriteLine("Sikeres belépés!", "green");
                            
                            Thread.Sleep(2000);
                            bool exit = false;
                            while (!exit)
                            {
                                int edzesMenu = Text.ArrowMenu(new string[] { "Új edzés rögzítése", "Edzés statisztika", "Kilépés" }, $"Üdvözöljük {loggeduser.Nev}!");
                                switch (edzesMenu)
                                {
                                    case 0:
                                        EdzesekAdatfeldolgozás.VersenyAdafelvetel(adatok);
                                        break;
                                    case 1:
                                        //Statisztika megjelenítése
                                        break;
                                    case 2:
                                        exit = true;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Text.WriteLine("Sikertelen belépés!", "red");
                            Thread.Sleep(2000);
                        }
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}

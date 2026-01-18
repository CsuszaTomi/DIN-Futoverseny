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
        static List<Edzes_adatok> adatok = EdzesekAdatfeldolgozas.EdzesFeldolgozo();
        static Users loggeduser = null;
        static List<Users> Users = UserActions.GetUsers();
        static void Main(string[] args)
        {
            while (true)
            {
                int menu = Text.ArrowMenu(new string[] { "Új fiók", "Bejelentkezés","Beállítások" ,"Kilépés" }, "Futás Nyilvántartó");
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
                            
                            Settings.Delay();
                            bool exit = false;
                            while (!exit)
                            {
                                int edzesMenu = Text.ArrowMenu(new string[] { "Új futás rögzítése", "Futások","Statisztikák","Adatok kezelése","Adatok törlése", "Módosíás","Kilépés" }, $"Üdvözöljük {loggeduser.Nev}!");
                                switch (edzesMenu)
                                {
                                    case 0:
                                        adatok = EdzesekAdatfeldolgozas.VersenyAdafelvetel(adatok,loggeduser);
                                        EdzesekAdatfeldolgozas.EdzesSave(adatok,loggeduser);
                                        break;
                                    case 1:
                                        //Statisztika megjelenítése 
                                        EdzesekAdatfeldolgozas.Megjelenites(loggeduser.Nev, adatok);
                                        break;
                                    case 2:
                                        EdzesekAdatfeldolgozas.Statisztikak(adatok, loggeduser);
                                        break;
                                    case 3:
                                        break;
                                    case 4:
                                        EdzesekAdatfeldolgozas.Torol(loggeduser.Nev, adatok);
                                        break;
                                    case 5:
                                        EdzesekAdatfeldolgozas.Modosit(loggeduser.Nev);
                                        break;
                                    case 6:
                                        exit = true;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Text.WriteLine("Sikertelen belépés!", "red");
                            Settings.Delay();
                        }
                        break;
                    case 2:
                        Settings.Menu();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}

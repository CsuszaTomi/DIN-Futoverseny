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
        public static bool AdminLogin = false;
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
                        if (loggeduser != null && AdminLogin == false)
                        {
                            Text.WriteLine("Sikeres belépés!", "green");
                            Settings.Delay();
                            bool exit = false;
                            while (!exit)
                            {
                                int edzesMenu = Text.ArrowMenu(new string[] { "Új futás rögzítése", "Futások", "Statisztikák", "Futások kezelése", "Átlag sebesség változása", "Fiók kezelés", "Kilépés" }, $"Üdvözöljük {loggeduser.Nev}!");
                                switch (edzesMenu)
                                {
                                    case 0:
                                        adatok = EdzesekAdatfeldolgozas.VersenyAdafelvetel(adatok, loggeduser);
                                        EdzesekAdatfeldolgozas.EdzesSave(adatok, loggeduser);
                                        break;
                                    case 1:
                                        //Statisztika megjelenítése 
                                        EdzesekAdatfeldolgozas.Megjelenites(loggeduser.Nev, adatok);
                                        Console.ReadLine();
                                        break;
                                    case 2:
                                        EdzesekAdatfeldolgozas.Statisztikak(adatok, loggeduser);
                                        break;
                                    case 3:
                                        int kezelomenu = Text.ArrowMenu(new string[] { "Futás törlése", "Futás módosítása", "Vissza" }, "Futás szerkesztés");
                                        switch (kezelomenu)
                                        {
                                            case 0:
                                                EdzesekAdatfeldolgozas.Torol(loggeduser.Nev, adatok);
                                                break;
                                            case 1:
                                                EdzesekAdatfeldolgozas.Modosit(loggeduser.Nev);
                                                break;
                                            case 2:
                                                break;
                                        }
                                        break;
                                    case 4:
                                        EdzesekAdatfeldolgozas.Atlagsebessegvaltozasa(loggeduser.Nev);
                                        break;
                                    case 5:
                                        Users = UserActions.FiokAdatModositas(Users, loggeduser);
                                        break;
                                    case 6:
                                        exit = true;
                                        break;
                                }
                            }
                        }
                        else if(AdminLogin == true)
                        {
                            bool exitAdmin = false;
                            while (!exitAdmin)
                            {
                                int adminMenu = Text.ArrowMenu(new string[] { "Felhasználók listázása", "Felhasználó törlése", "Kilépés" }, "Admin felület");
                                switch (adminMenu)
                                {
                                    case 0:
                                        AdminActions.ListUsers(Users);
                                        break;
                                    case 1:
                                        Users = AdminActions.DeleteUser(Users);
                                        UserActions.UserSave(Users);
                                        break;
                                    case 2:
                                        exitAdmin = true;
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

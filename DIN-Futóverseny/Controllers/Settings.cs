using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIN_Futóverseny.Models;

namespace DIN_Futóverseny.Controllers
{
    internal class Settings
    {
        int actionDelay = 2000;
        bool delayEnabled = false;

        public Settings(int actionDelay, bool delayEnabled)
        {
            ActionDelay = actionDelay;
            DelayEnabled = delayEnabled;
        }

        public int ActionDelay { get => actionDelay; set => actionDelay = value; }
        public bool DelayEnabled { get => delayEnabled; set => delayEnabled = value; }

        public static void Menu(Users loggeduser, List<Users> Users)
        {
            if (loggeduser == null)
            {
                Console.Clear();
                Text.WriteLine("Kérem jelentkezzen be a beállítások eléréséhez!", "yellow");
                Text.WriteLine("Vissza a főmenübe...", "yellow");
                System.Threading.Thread.Sleep(2000);
                return;
            }
            bool exit = false;
            while (!exit)
            {
                int settingsMenu = Text.ArrowMenu(new string[] { "Delay beállítások", "Kilépés" }, "Beállítások");
                switch (settingsMenu)
                {
                    case 0:
                        Console.Clear();
                        Text.WriteLine("Delay beállítások", "red");
                        Text.WriteLine("--------------------");
                        Text.Write("Ha nem akar valamit módosítani akkor nyomjon arra szóközt!", "yellow");
                        //Text.Write($"Kérem adja meg a delay időt milliszekundumban(Mostani: {ActionDelay}): ");
                        break;
                    case 1:
                        exit = true;
                        break;
                }
            }
        }
    }
}

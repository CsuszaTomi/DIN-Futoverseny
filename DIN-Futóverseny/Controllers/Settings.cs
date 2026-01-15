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
        static int actionDelay = 2000;
        static bool delayEnabled = false;

        public Settings(int actionDelay, bool delayEnabled)
        {
            ActionDelay = actionDelay;
            DelayEnabled = delayEnabled;
        }

        static public  int ActionDelay { get => actionDelay; set => actionDelay = value; }
        static public bool DelayEnabled { get => delayEnabled; set => delayEnabled = value; }

        /// <summary>
        /// A beállítások menü kezelő függvénye
        /// </summary>
        public static void Menu()
        {
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
                        Text.WriteLine("Ha nem akar valamit módosítani akkor nyomjon arra szóközt!", "yellow");
                        Text.Write("Szeretné engedélyezni a delay-t? (i/n): ");
                        string delayInput = Console.ReadLine();
                        if (delayInput.ToLower() == "i")
                        {
                            DelayEnabled = true;
                            Text.WriteLine("Delay engedélyezve.", "green");
                        }
                        else if (delayInput.ToLower() == "n")
                        {
                            DelayEnabled = false;
                            Text.WriteLine("Delay letiltva.", "green");
                        }
                        else if (delayInput != " " && delayInput != "")
                        {
                            break;
                        }
                        else
                        {
                            Text.WriteLine("Hibás bemenet! Kérem 'i' vagy 'n' értéket adjon meg!", "red");
                        }
                        if (delayEnabled)
                        {
                            Text.Write($"Kérem adja meg a delay időt milliszekundumban(Mostani: {ActionDelay}): ");
                            string input = Console.ReadLine();
                            if (input != " " && input != "")
                            {
                                if (int.TryParse(input, out int newDelay))
                                {
                                    ActionDelay = newDelay;
                                    Text.WriteLine($"Sikeres módosítás! Új delay idő: {ActionDelay} ms", "green");
                                }
                                else
                                {
                                    Text.WriteLine("Hibás bemenet! Kérem számot adjon meg!", "red");
                                }
                            }
                        }
                        break;
                    case 1:
                        exit = true;
                        break;
                }
            }
        }

        /// <summary>
        /// A thread.delay függvény meghívása, ha a delay engedélyezve van
        /// </summary>
        public static void Delay()
        {
            if (DelayEnabled)
            {
                System.Threading.Thread.Sleep(ActionDelay);
            }
        }
    }
}

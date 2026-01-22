using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIN_Futóverseny.Models
{
    internal class Text
    {
        /// <summary>
        /// A Console.WriteLine középre íratása
        /// </summary>
        /// <param name="szoveg">A kiírandó szöveg</param>
        /// <param name="szin">A kiírandó szöveg színe</param>
        public static void WriteLine(string szoveg, string szin = "")
        // Console.WriteLine középre íratása
        {
            int balszokoz = (Console.WindowWidth - szoveg.Length) / 2;
            if (balszokoz < 0)
            {
                balszokoz = 0;
            }
            if (szin != "")
            {
                if (szin == "green")
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (szin == "red")
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (szin == "yellow")
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else if (szin == "blue")
                    Console.ForegroundColor = ConsoleColor.Blue;
                else if (szin == "cyan")
                    Console.ForegroundColor = ConsoleColor.Cyan;
                else if (szin == "yellow2")
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string(' ', balszokoz) + szoveg);
        }
        /// <summary>
        /// A Console.Write középre íratása
        /// </summary>
        /// <param name="szoveg">A kiírandó szöveg</param>
        /// <param name="szin">A kiírandó szöveg színe</param>
        public static void Write(string szoveg, string szin = "")
        {
            int ballszokoz = (Console.WindowWidth - szoveg.Length) / 2;
            if (ballszokoz < 0)
            {
                ballszokoz = 0;
            }
            if (szin != "")
            {
                if (szin == "green")
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (szin == "red")
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (szin == "yellow")
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else if (szin == "blue")
                    Console.ForegroundColor = ConsoleColor.Blue;
                else if (szin == "cyan")
                    Console.ForegroundColor = ConsoleColor.Cyan;
                else if (szin == "yellow2")
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.Write(new string(' ', ballszokoz) + szoveg);
        }
        /// <summary>
        /// Menü kezelő függvény
        /// </summary>
        /// <param name="menupontok">Menüpontok amiket létrehoz</param>
        /// <param name="cím">A menü címe</param>
        /// <returns></returns>
        public static int ArrowMenu(string[] menupontok, string cím)
        {
            int kivalasztottmenupont = 0;
            bool valasztott = false;
            do
            {
                Console.Clear();
                Text.WriteLine(cím, "red");
                Text.WriteLine("====================");
                for (int i = 0; i < menupontok.Length; i++)
                {
                    if (i == kivalasztottmenupont)
                    {
                        Text.WriteLine($"> {menupontok[i]}", "green");
                    }
                    else
                    {
                        Text.WriteLine($"  {menupontok[i]}");
                    }
                }
                Text.WriteLine("====================");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        valasztott = true;
                        break;
                    case ConsoleKey.E:
                        valasztott = true;
                        break;
                    case ConsoleKey.UpArrow:
                        if (kivalasztottmenupont > 0) kivalasztottmenupont--;
                        break;
                    case ConsoleKey.W:
                        if (kivalasztottmenupont > 0) kivalasztottmenupont--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (kivalasztottmenupont < menupontok.Length - 1) kivalasztottmenupont++;
                        break;
                    case ConsoleKey.S:
                        if (kivalasztottmenupont < menupontok.Length - 1) kivalasztottmenupont++;
                        break;
                    default:
                        Console.Beep();
                        break;
                }
            } while (!valasztott);
            return kivalasztottmenupont;
        }

        public static int ArrowMenuTable(string[] menupontok, string cím,Action tabledraw)
        {
            int kivalasztottmenupont = 0;
            bool valasztott = false;
            do
            {
                Console.Clear();
                tabledraw();
                Text.WriteLine(cím, "red");
                Text.WriteLine("====================");
                for (int i = 0; i < menupontok.Length; i++)
                {
                    if (i == kivalasztottmenupont)
                    {
                        Text.WriteLine($"> {menupontok[i]}", "green");
                    }
                    else
                    {
                        Text.WriteLine($"  {menupontok[i]}");
                    }
                }
                Text.WriteLine("====================");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        valasztott = true;
                        break;
                    case ConsoleKey.E:
                        valasztott = true;
                        break;
                    case ConsoleKey.UpArrow:
                        if (kivalasztottmenupont > 0) kivalasztottmenupont--;
                        break;
                    case ConsoleKey.W:
                        if (kivalasztottmenupont > 0) kivalasztottmenupont--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (kivalasztottmenupont < menupontok.Length - 1) kivalasztottmenupont++;
                        break;
                    case ConsoleKey.S:
                        if (kivalasztottmenupont < menupontok.Length - 1) kivalasztottmenupont++;
                        break;
                    default:
                        Console.Beep();
                        break;
                }
            } while (!valasztott);
            return kivalasztottmenupont;
        }
    }
}

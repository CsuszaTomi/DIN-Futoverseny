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
        /// A függvény a Console.WriteLine középre íratását valósítja meg
        /// </summary>
        /// <param name="text">A megadott szöveget írja ki középre</param>
        public static void WriteLineCentered(string text, string szin = "")
        // Console.WriteLine középre íratása
        {
            int leftPadding = (Console.WindowWidth - text.Length) / 2;
            if (leftPadding < 0)
            {
                leftPadding = 0;
            }
            if (szin != "")
            {
                if (szin == "green")
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (szin == "red")
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (szin == "yellow")
                    Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string(' ', leftPadding) + text);
        }
        public static void WriteCentered(string text, string szin = "")
        // Console.Write középre íratása
        {
            int leftPadding = (Console.WindowWidth - text.Length) / 2;
            if (leftPadding < 0)
            {
                leftPadding = 0;
            }
            if (szin != "")
            {
                if (szin == "green")
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (szin == "red")
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (szin == "yellow")
                    Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.Write(new string(' ', leftPadding) + text);
        }

        public static string CenterText(string text, int width)
        {
            if (text.Length > width)
                text = text.Substring(0, width);

            int left = (width - text.Length) / 2;
            int right = width - text.Length - left;

            return new string(' ', left) + text + new string(' ', right);
        }
        /// <summary>
        /// Menü kezelő függvény
        /// </summary>
        /// <param name="menupoints">Menüpontok amiket létrehoz</param>
        /// <param name="title">A menü címe</param>
        /// <returns></returns>
        public static int ArrowMenu(string[] menupoints, string title)
        {
            int currentPoint = 0;
            bool selected = false;
            do
            {
                Console.Clear();
                Text.WriteLineCentered(title, "red");
                Text.WriteLineCentered("------------------");
                for (int i = 0; i < menupoints.Length; i++)
                {
                    if (i == currentPoint)
                    {
                        Text.WriteLineCentered($"> {menupoints[i]}", "green");
                    }
                    else
                    {
                        Text.WriteLineCentered($"  {menupoints[i]}");
                    }
                }
                Text.WriteLineCentered("------------------");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        selected = true;
                        break;
                    case ConsoleKey.E:
                        selected = true;
                        break;
                    case ConsoleKey.UpArrow:
                        if (currentPoint > 0) currentPoint--;
                        break;
                    case ConsoleKey.W:
                        if (currentPoint > 0) currentPoint--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentPoint < menupoints.Length - 1) currentPoint++;
                        break;
                    case ConsoleKey.S:
                        if (currentPoint < menupoints.Length - 1) currentPoint++;
                        break;
                    default:
                        Console.Beep();
                        break;
                }
            } while (!selected);
            return currentPoint;
        }
    }
}

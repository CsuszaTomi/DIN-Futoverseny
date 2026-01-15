using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIN_Futóverseny.Models;

namespace DIN_Futóverseny.View
{
    internal class Tables
    {
        /// <summary>
        /// Egy formázott táblázatot rajzol ki a konzolra a megadott címsorok és értékek alapján.
        /// A metódus automatikusan kiszámolja az oszlopok szükséges szélességét a tartalom alapján.
        /// </summary>
        /// <param name="cimsorok">A táblázat oszlopainak fejléceit tartalmazó tömb. Ennek hossza határozza meg az oszlopok számát.</param>
        /// <param name="ertekek">A táblázat adatait tartalmazó egydimenziós tömb. Az értékek sorfolytonosan kerülnek a táblázatba. A tömb elemszámának oszthatónak kell lennie a címsorok számával.</param>
        public static void Table(string[] cimsorok, string[] ertekek)
        {
            int oszlopokSzama = cimsorok.Length;
            if (ertekek.Length % oszlopokSzama != 0)
            {
                Text.WriteLine("Az értékek száma nem jön ki a címsorok számával!", "red");
                return;
            }
            int sorokSzama = ertekek.Length / oszlopokSzama;
            int[] szelessegek = new int[oszlopokSzama];
            //szélességek kiszámolása
            for (int i = 0; i < oszlopokSzama; i++)
            {
                szelessegek[i] = cimsorok[i].Length; 
                for (int j = 0; j < sorokSzama; j++)
                {
                    string adat = ertekek[j * oszlopokSzama + i];
                    if (adat.Length > szelessegek[i])
                    {
                        szelessegek[i] = adat.Length;
                    }
                }
            }
            //vonal rajzoló függvény
            string VonalRajzolas(string bal, string kozep, string jobb, string kitolto)
            {
                string vonal = bal;
                for (int i = 0; i < oszlopokSzama; i++)
                {
                    vonal += new string(kitolto[0], szelessegek[i] + 2);
                    if (i < oszlopokSzama - 1) vonal += kozep;
                    else vonal += jobb;
                }
                return vonal;
            }

            //felso szegely
            Text.WriteLine(VonalRajzolas("╔", "╦", "╗", "═"));

            //fejlec tartalom
            string fejlecSor = "║";
            for (int i = 0; i < oszlopokSzama; i++)
            {
                
                fejlecSor += " " + cimsorok[i].PadRight(szelessegek[i]) + " ║";
            }
            Text.WriteLine(fejlecSor);

            //kozepso resz
            if (sorokSzama > 0)
            {
                Text.WriteLine(VonalRajzolas("╠", "╬", "╣", "═"));
            }
            else
            {
                //adatzaro
                Text.WriteLine(VonalRajzolas("╚", "╩", "╝", "═"));
                return;
            }

            //adat beiro
            for (int i = 0; i < sorokSzama; i++)
            {
                string sor = "║";
                for (int j = 0; j < oszlopokSzama; j++)
                {
                    string adat = ertekek[i * oszlopokSzama + j];
                    sor += " " + adat.PadRight(szelessegek[j]) + " ║";
                }
                Text.WriteLine(sor);
            }

            //also szegely
            Text.WriteLine(VonalRajzolas("╚", "╩", "╝", "═"));
        }   
    }
}

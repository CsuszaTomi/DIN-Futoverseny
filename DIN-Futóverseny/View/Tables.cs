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
        /// Adott edzésadatokat táblázatos formában jeleníti meg a konzolon.
        /// </summary>
        /// <param name="adatok">A megjelenítendő edzésadatok listája.</param>
        public static void EdzesTablazatMegjelenites(List<string> adatok)
        {
            Text.WriteLine("╔═══════════════╦══════════╦════════════╦══════════════╦════════════════╦══════════╦═══════════════════════╗");
            Text.WriteLine("║      Dátum    ║ Táv (km) ║    Idő     ║  MaxPulzus   ║ NyugalmiPulzus ║  Súly    ║ Átlag sebesség (km/h) ║");
            Text.WriteLine("╠═══════════════╬══════════╬════════════╬══════════════╬════════════════╬══════════╬═══════════════════════╣");
            for (int i = 0; i < adatok.Count; i += 7)
            {
                string datum = adatok[i].PadRight(13);     
                string tav = adatok[i + 1].PadLeft(8);    
                string ido = adatok[i + 2].PadLeft(10);    
                string maxP = adatok[i + 3].PadLeft(12);   
                string nyugP = adatok[i + 4].PadLeft(14);  
                string suly = adatok[i + 5].PadLeft(8);     
                string seb = adatok[i + 6].PadLeft(21);     
                Text.WriteLine($"║ {datum} ║ {tav} ║ {ido} ║ {maxP} ║ {nyugP} ║ {suly} ║ {seb} ║");
            }
            Text.WriteLine("╚═══════════════╩══════════╩════════════╩══════════════╩════════════════╩══════════╩═══════════════════════╝");
        }
    }
}

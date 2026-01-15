using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIN_Futóverseny.Controllers
{
    internal class Ellenorzo
    {
        public static bool IntSzamEllenorzo(string szam)
        {
            int ki = 0;
            if(int.TryParse(szam, out ki))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DecimalSzamEllenorzo(string szam)
        {
            decimal ki = 0;
            if (decimal.TryParse(szam, out ki))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DateTimeEllenorzo(string date)
        {
            DateTime ki = DateTime.Now;
            if(DateTime.TryParse(date, out ki))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

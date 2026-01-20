using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIN_Futóverseny.Controllers
{
    internal class Ellenorzo
    {
        /// <summary>
        /// Ellenőrzi, hogy a bemenet üres-e
        /// </summary>
        /// <param name="input">A bemenet szövege</param>
        /// <returns>True, ha a bemenet nem üres; egyébként False</returns>
        public static bool UressegEllenorzo(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input == "" || input == " ")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Ellenőrzi, hogy a bemenet egész szám-e
        /// </summary>
        /// <param name="szam">A bemenet szövege</param>
        /// <returns>True, ha a bemenet egész szám; egyébként False</returns>
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

        /// <summary>
        /// Ellenőrzi, hogy a bemenet tizedes szám-e
        /// </summary>
        /// <param name="szam">A bemenet szövege</param>
        /// <returns>True, ha a bemenet tizedes szám; egyébként False</returns>
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

        /// <summary>
        /// Ellenőrzi, hogy a bemenet double szám-e
        /// </summary>
        /// <param name="szam">A bemenet szövege</param>
        /// <returns>True, ha a bemenet double szám; egyébként False</returns>
        public static bool DoubleEllenorzo(string szam)
        {
            double ki = 0;
            if (double.TryParse(szam, out ki))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Ellenőrzi, hogy a bemenet dátum-e
        /// </summary>
        /// <param name="date">A bemenet szövege</param>
        /// <returns>True, ha a bemenet dátum; egyébként False</returns>
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

        /// <summary>
        /// Ellenőrzi, hogy a bemenet TimeSpan-e
        /// </summary>
        /// <param name="span">A bemenet szövege</param>
        /// <returns>True, ha a bemenet TimeSpan; egyébként False</returns>
        public static bool TimeSpanEllenorzo(string span)
        {
            TimeSpan ki = TimeSpan.Zero;
            if (TimeSpan.TryParse(span, out ki))
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

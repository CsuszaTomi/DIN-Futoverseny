using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIN_Futóverseny.Models
{
    internal class Edzes_adatok
    {
        private DateTime datum;
        private decimal tavolsag;
        private TimeSpan idotartam;
        private int max_pulzus;

        public DateTime Datum { get => datum; set => datum = value; }
        public decimal Tavolsag { get => tavolsag; set => tavolsag = value; }
        public TimeSpan Idotartam { get => idotartam; set => idotartam = value; }
        public int Max_pulzus { get => max_pulzus; set => max_pulzus = value; }

        public Edzes_adatok(DateTime datum, decimal tavolsag, TimeSpan idotartam, int max_pulzus)
        {
            this.Datum = datum;
            this.Tavolsag = tavolsag;
            this.Idotartam = idotartam;
            this.Max_pulzus = max_pulzus;
        }
    }
}

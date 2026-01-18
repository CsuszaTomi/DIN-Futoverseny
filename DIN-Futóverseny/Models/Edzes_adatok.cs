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
        private string nev;
        private DateTime datum;
        private decimal tavolsag;
        private TimeSpan idotartam;
        private int max_pulzus;
        private int nyug_pulzus;
        private double testsuly;

        public string Nev { get => nev; set => nev = value; }
        public DateTime Datum { get => datum; set => datum = value; }
        public decimal Tavolsag { get => tavolsag; set => tavolsag = value; }
        public TimeSpan Idotartam { get => idotartam; set => idotartam = value; }
        public int Max_pulzus { get => max_pulzus; set => max_pulzus = value; }
        public int Nyug_pulzus { get => nyug_pulzus; set => nyug_pulzus = value; }
        public double Testsuly { get => testsuly; set => testsuly = value; }

        public Edzes_adatok(string nev, DateTime datum, decimal tavolsag, TimeSpan idotartam, int max_pulzus, int nyug_pulzus, double testsuly)
        {
            this.Nev = nev;
            this.Datum = datum;
            this.Tavolsag = tavolsag;
            this.Idotartam = idotartam;
            this.Max_pulzus = max_pulzus;
            this.Nyug_pulzus = nyug_pulzus;
            this.Testsuly = testsuly;
        }
    }
}

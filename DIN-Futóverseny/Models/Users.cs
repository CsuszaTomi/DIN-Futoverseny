using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIN_Futóverseny.Models
{
    internal class Users
    {
        string nev = "";
        string jelszo = "";
        DateTime szuldatum = DateTime.MinValue;
        double testsuly = 0;
        double magassag = 0;
        double nyugpul = 0;
        double altcel = 0;

        public Users(string nev, string jelszo, DateTime szuldatum, double testsuly, double magassag, double nyugpul, double altcel)
        {
            Nev = nev;
            Jelszo = jelszo;
            Szuldatum = szuldatum;
            Testsuly = testsuly;
            Magassag = magassag;
            Nyugpul = nyugpul;
            Altcel = altcel;
        }
        
        public Users()
        {
        }

        public Users(string sor)
        {
            string[] adatok = sor.Split(';');
            Nev = adatok[0];
            Jelszo = adatok[1];
            Szuldatum = DateTime.Parse(adatok[2]);
            Testsuly = double.Parse(adatok[3]);
            Magassag = double.Parse(adatok[4]);
            Nyugpul = double.Parse(adatok[5]);
            Altcel = double.Parse(adatok[6]);
        }

        public string Nev { get => nev; set => nev = value; }
        public string Jelszo { get => jelszo; set => jelszo = value; }
        public DateTime Szuldatum { get => szuldatum; set => szuldatum = value; }
        public double Testsuly { get => testsuly; set => testsuly = value; }
        public double Magassag { get => magassag; set => magassag = value; }
        public double Nyugpul { get => nyugpul; set => nyugpul = value; }
        public double Altcel { get => altcel; set => altcel = value; }
        public bool IsAdmin { get; set; } = false;
    }
}

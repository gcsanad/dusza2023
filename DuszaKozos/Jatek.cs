// Index out of range csapat

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuszaKozos
{
    internal class Jatek
    {

        string szervezo;
        string jatekNeve;
        string alanyok;
        string esemenyek;
        string joEredmeny;
        bool lezart;
        int fogadasokSzama;
        int osszPontszam;
        int feltettPontok;

        public Jatek(string szervezo, string jatekNeve, string alanyok, string esemenyek)
        {
            this.szervezo = szervezo;
            this.jatekNeve = jatekNeve;
            this.alanyok = alanyok;
            this.esemenyek = esemenyek;
            this.lezart = false;
        }

        public string Szervezo  => szervezo;
        public string JatekNeve  => jatekNeve;
        public string[] Esemenyek => this.esemenyek.Split(";"); 
        public string[] Alanyok => this.alanyok.Split(";");

        public int OsszPontszam => lezart == false ? 0 : fogadasokSzama;
        public bool Lezart { get => lezart; set => lezart = value; }
        public int FogadasokSzama { get => fogadasokSzama; set => fogadasokSzama = value; }

        public string JoEredmeny { get => joEredmeny; set => joEredmeny = value; }
        public int FeltettPontok { get => feltettPontok; set => feltettPontok = value; }

        public override string? ToString() => $"{szervezo},{jatekNeve},{Alanyok.Length},{Esemenyek.Length}" +
            $"\nAlanyok: {alanyok[0..]}" +
            $"\nEsemények: {esemenyek[0..]}" +
            $"\n{new string('-',45)}";

    }
}

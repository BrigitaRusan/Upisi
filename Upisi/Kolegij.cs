using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upisi
{
    internal class Kolegij : IUsporedivo<Kolegij>
    {
        public readonly string Sifra; //jedinstvena
        public readonly string Naziv;
        public readonly int ECTSBodovi;

        public Kolegij(string sifra, string naziv, int ectsBodovi)
        {
            Sifra = sifra;
            Naziv = naziv;
            ECTSBodovi = ectsBodovi;
        }
        public int Usporedi(Kolegij other)
        {
            return string.Compare(this.Sifra, other.Sifra, StringComparison.Ordinal);
        }
        public override string ToString()
        {
            return $"{Naziv} ({Sifra}, {ECTSBodovi} ECTS)";
        }
    }
}

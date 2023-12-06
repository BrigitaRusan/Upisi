using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upisi
{
    internal class Fakultet
    {
        public readonly string Naziv;
        private readonly Dictionary<string, Kolegij> kolegiji;
        private readonly Dictionary<Kolegij, List<Student>> studentiNaKolegijima;
        private readonly HashSet<string> sviJMBAGovi;
        public Fakultet(string naziv)
        {
            Naziv = naziv;
            kolegiji = new Dictionary<string, Kolegij>();
            studentiNaKolegijima = new Dictionary<Kolegij, List<Student>>();
            sviJMBAGovi = new HashSet<string>();
        }

        public int BrojStudenata => sviJMBAGovi.Count;


        public List<Student> DajUpisaneStudenteZaKolegij(Kolegij kolegij)
        {
            if (studentiNaKolegijima.ContainsKey(kolegij))
            {
                return studentiNaKolegijima[kolegij];
            }

            return new List<Student>(); // Vraćamo praznu listu ako kolegij nema upisanih studenata
        }

        public List<Kolegij> DohvatiKolegije() => kolegiji.Values.ToList();


        // Metoda za dodavanje kolegija fakultetu
        public void DodajKolegij(Kolegij kolegij)
        {
            if (!kolegiji.ContainsKey(kolegij.Sifra))
            {
                kolegiji.Add(kolegij.Sifra, kolegij); // Dodavanje kolegija s ključem kolegij.Sifra
                studentiNaKolegijima.Add(kolegij, new List<Student>());
            }
            else
            {
                throw new Exception("Kolegij već postoji na fakultetu.");
            }
        }

        // Preopterećen operator += za dodavanje kolegija fakultetu
        public static Fakultet operator +(Fakultet fakultet, Kolegij kolegij)
        {
            fakultet.DodajKolegij(kolegij);
            return fakultet;
        }


        // Preopterećen operator -= za uklanjanje studenta s kolegija
        public static Fakultet operator -(Fakultet fakultet, (string sifraKolegija, Student student) data)
        {
            fakultet.UkloniStudentaSaKolegija(data.sifraKolegija, data.student);
            return fakultet;
        }


        // Preopterećen operator -= za uklanjanje studenta s određenog kolegija
        public void UkloniStudentaSaKolegija(string sifraKolegija, Student student)
        {
            if (kolegiji.ContainsKey(sifraKolegija))
            {
                if (studentiNaKolegijima[kolegiji[sifraKolegija]].Contains(student))
                {
                    studentiNaKolegijima[kolegiji[sifraKolegija]].Remove(student);
                }
            }
            else
            {
                throw new Exception("Kolegij s tom šifrom ne postoji na fakultetu.");
            }
        }


        public void DodajStudentaNaKolegij(string sifraKolegija, Student student)
        {
            if (kolegiji.ContainsKey(sifraKolegija))
            {
                if (!studentiNaKolegijima[kolegiji[sifraKolegija]].Contains(student))
                {
                    studentiNaKolegijima[kolegiji[sifraKolegija]].Add(student);
                    sviJMBAGovi.Add(student.JMBAG);
                }
                else
                {
                    throw new Exception("Student je već upisan na ovaj kolegij!");
                }

            }
            else
            {
                throw new Exception("Kolegij s tom šifrom ne postoji na fakultetu.");
            }

        }
        // Indeksiranje za dodavanje studenata na kolegije
        public List<Student> this[string sifraKolegija]
        {
            get
            {
                if (kolegiji.ContainsKey(sifraKolegija))
                {
                    return studentiNaKolegijima[kolegiji[sifraKolegija]];
                }
                else
                {
                    throw new KeyNotFoundException("Kolegij s tom šifrom ne postoji na fakultetu.");

                }
            }
            set
            {
                if (!kolegiji.ContainsKey(sifraKolegija)) //Kolegij.Sifra
                {
                    Console.WriteLine("Kolegij s tom šifrom ne postoji!");
                    return;
                }

                studentiNaKolegijima[kolegiji[sifraKolegija]] = value; //kolegij
                foreach (var student in value)
                {
                    sviJMBAGovi.Add(student.JMBAG);
                }
            }
        }

        public string this[Student student]
        {
            get
            {
                string result = $"Ukupno ECTS: 0\nUpisani kolegiji:\n";
                foreach (var kvp in studentiNaKolegijima)
                {
                    if (kvp.Value.Contains(student))
                    {
                        Kolegij currentKolegij = kvp.Key;
                        result += $"{currentKolegij}\n";
                    }
                }
                return result;
            }
        }

        public string GetKolegijiStudenta(Student student)
        {
            var upisaniKolegiji = studentiNaKolegijima.Where(pair => pair.Value.Contains(student)).Select(pair => pair.Key.Naziv).OrderBy(name => name).ToList();
            int ukupniECTS = studentiNaKolegijima.Where(pair => pair.Value.Contains(student)).Select(pair => pair.Key.ECTSBodovi).Sum();

            return string.Join(", ", upisaniKolegiji) + "\nUkupni ECTS: " + ukupniECTS;
        }
        public void IspisiStudentaSaKolegija(string sifraKolegija, Student student)
        {
            if (kolegiji.ContainsKey(sifraKolegija))
            {
                studentiNaKolegijima[kolegiji[sifraKolegija]].Remove(student);
            }
        }

        public override string ToString()
        {
            List<Kolegij> kolegijiNaFakultetu = kolegiji.Values.ToList();
            kolegijiNaFakultetu.Sort((x, y) => string.Compare(x.Sifra, y.Sifra, StringComparison.Ordinal));

            string result = $"{Naziv} kolegiji:\n";
            foreach (var kolegij in kolegijiNaFakultetu)
            {
                result += $"{kolegij}\n";
            }

            return result;
        }
    }
}

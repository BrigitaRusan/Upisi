using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Upisi
{
    internal class Student : IUsporedivo<Student>
    {
        public readonly string JMBAG;  //jedinstven
        public readonly string Ime;
        public readonly string Prezime;

        public Student(string jmbag, string ime, string prezime)
        {
            JMBAG = jmbag;
            Ime = ime;
            Prezime = prezime;
        }
        public Student()
        {

            Regex regex = new Regex(@"^\d{10}, [A-Z][a-z]+( [A-Z][a-z]+)*$");
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Unesite podatke o studentu u formatu: JMBAG, Ime Prezime");
                string input = Console.ReadLine();
                if (input != null)
                {
                    if (regex.IsMatch(input))
                    {
                        string[] podaci = input.Split(", ");

                        JMBAG = podaci[0];
                        Ime = podaci[1];
                        Prezime = podaci[2];

                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Neispravan format unosa! Molimo ponovno unesite podatke.");
                    }
                }
            }

        }
        public int Usporedi(Student other)
        {
            return string.Compare(this.JMBAG, other.JMBAG, StringComparison.Ordinal);
        }
        public override string ToString()
        {
            return $"{JMBAG} ({Prezime}, {Ime})";
        }
    }
}

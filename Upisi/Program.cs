//using System.Collections.Immutable;
using Upisi;

//fakultet te neki studenti i kolegiji za testiranje
// (zakomentirano je unos korisnika nakon pokretanja):
Fakultet PMF = new("Prirodoslovno-matematicki fakultet");
Student A = new("1234567890", "John", "Doe"),
    B = new(), //1324356479, Ana Marija, Garcia Gomes Delevigne
    C = new(); //0012401424, Clara, Del Horvat
Kolegij[] kolegiji = {
    new("21509", "Linearna algebra 1", 8),
    new("36901", "Programiranje 1", 6),
    new("21508", "Matematicka analiza 2", 9)
};

//dodavanje kolegija fakultetu
Array.Sort(kolegiji); //sortiranje kolegija po sifri (leksikografski)
foreach (Kolegij kolegij in kolegiji)
    PMF += kolegij;
PMF += new Kolegij("36904", "Diskretna matematika", 5);

//probamo dodati kolegij koji se vec drzi
try
{
    PMF += new Kolegij("21508", "TZK", 0); //ista sifra kao kod Mat. an. 2
}
catch (Exception e)
{
    Console.WriteLine(e.Message); //Kolegij s tom sifrom vec postoji!
}

//ispis svih kolegija tog fakulteta
Console.WriteLine("Ispis kolegija na PMF-u:");
Console.WriteLine(PMF);

//upis studenata na neke kolegije
PMF["21509"] += A;
PMF["21509"] += B;
PMF["21509"] += C;
PMF["36901"] += C;
PMF["21508"] += A;
PMF["21508"] += C;

//pokusamo upisati na Lin. alg. 1 vec tamo upisanog studenta (isti JMBAG!)
try
{
    PMF["21509"] += new Student("1234567890", "Neki", "Student");
}
catch (Exception e)
{
    Console.WriteLine(e.Message); //Student je vec upisan na ovaj kolegij!
}

//pokusamo upisati studenta na kolegij koji se ne drzi na tom fakultetu
try
{
    PMF["00000"] += new Student("1234567890", "Neki", "Student");
}
catch (Exception e)
{
    Console.WriteLine(e.Message); //Nema kolegija s tom sifrom!
}

//ispis podataka o studentima koji su upisani na kolegij sa sifrom 21501
Ispis(PMF, "21509"); //pomocna funkcija Ispis() je implementirana ispod
PMF["21509"] -= A;
PMF["21509"] -= A; //ovaj drugi pokusaj izbacivanja nema nikakav efekt
//ispis podataka nakon promjene
Ispis(PMF, "21509");

//ispis broja studenata s bar jednim upisanim kolegijem na fakultetu
Console.WriteLine("Ukupno studenata na fakultetu: " + PMF.BrojStudenata);

//ispis podataka o studentu C (ukupan broj ECTS-a i upisani kolegiji)
Console.WriteLine($"Ispis podataka o studentu {C}:");
Console.WriteLine(PMF[C]);

//pomocna funkcija - sortira i ispise studente upisane na
//  kolegij sa sifrom sifra na fakultetu fakultet
static void Ispis(Fakultet fakultet, string sifra)
{
    Console.WriteLine(new string('-', 24)); //za ljepsi ispis
    Console.WriteLine("Ispis studenata na kolegiju sa " +
        $"sifrom {sifra}:");
    Console.WriteLine(new string('-', 24)); //za ljepsi ispis
    fakultet[sifra].Sort(); //sort po JMBAG-u (leksikografski)
    foreach (Student student in fakultet[sifra])
    {
        Console.WriteLine('\t' + student.ToString());
    }
}
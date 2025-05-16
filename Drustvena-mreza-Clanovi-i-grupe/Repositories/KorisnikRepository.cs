using Drustvena_mreza_Clanovi_i_grupe.Models;

namespace Drustvena_mreza_Clanovi_i_grupe.Repositories
{
    public class KorisnikRepository
    {
        private const string filePath = "../data/korisnici.csv";
        public static Dictionary<int, Korisnik> Data = new Dictionary<int, Korisnik>();

        public KorisnikRepository()
        {
            if (Data == null)
            {
                Load();
            }
        }

        public void Load()
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] attributes = line.Split(',');
                int id = int.Parse(attributes[0]);
                string korisnickoIme = attributes[1];
                string ime = attributes[2];
                string prezime = attributes[3];
                DateTime datumRodjenja = DateTime.Parse(attributes[4]);
                Korisnik korisnik = new Korisnik(id, korisnickoIme, ime, prezime, datumRodjenja);
                Data[id] = korisnik;
            }

        }
        public void Save()
        {
            List<string> lines = new List<string>();
            foreach (Korisnik k in Data.Values)
            {
                lines.Add($"{k.Id},{k.KorisnickoIme},{k.Ime},{k.Prezime},{k.DatumRodjenja:yyyy-MM-dd}");
            }
            File.WriteAllLines(filePath, lines);
        }
    }
}

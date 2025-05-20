using Drustvena_mreza_Clanovi_i_grupe.Models;

namespace Drustvena_mreza_Clanovi_i_grupe.Repositories
{
    public class GrupaRepository
    {
        private const string filePath = "data/grupe.csv";
        private const string clanstvaPath = "data/clanstva.csv";

        public Dictionary<int, Grupa> Data { get; set; }

        public GrupaRepository()
        {
            Data = new Dictionary<int, Grupa>();
            Load();

            //Kreiramo korisnik repozitorijum da bismo učitali članstva
            KorisnikRepository korisnikRepo = new KorisnikRepository();
            UcitajClanstva(clanstvaPath, korisnikRepo.Data);
        }

        private void UcitajClanstva(string clanstvaPath, Dictionary<int, Korisnik> korisnici)
        {
            if (!File.Exists(clanstvaPath))
            {
                return;
            }

            string[] lines = File.ReadAllLines(clanstvaPath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                int korisnikId = int.Parse(parts[0]);
                int grupaId = int.Parse(parts[1]);

                if (Data.ContainsKey(grupaId) && korisnici.ContainsKey(korisnikId))
                {
                    Data[grupaId].Korisnici.Add(korisnici[korisnikId]);
                }
            }
        }

        private void Load()
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                int id = int.Parse(parts[0]);
                string ime = parts[1];
                DateTime datumOsnivanja = DateTime.Parse(parts[2]);

                Grupa grupa = new Grupa(id, ime, datumOsnivanja);
                Data[id] = grupa;
            }
        }

        public void Save()
        {
            // Čuvanje informacija o grupama u grupe.csv
            List<string> grupeLines = new List<string>();
            foreach (var grupa in Data.Values)
            {
                grupeLines.Add($"{grupa.Id},{grupa.Ime},{grupa.DatumOsnivanja:yyyy-MM-dd}");
            }
            File.WriteAllLines(filePath, grupeLines);

            //Čuvanje članstva korisnika u clanstva.csv
            List<string> clanstvaLines = new List<string>();
            foreach (var grupa in Data.Values)
            {
                foreach (var korisnik in grupa.Korisnici)
                {
                    clanstvaLines.Add($"{korisnik.Id},{grupa.Id}");
                }
            }
            File.WriteAllLines(clanstvaPath, clanstvaLines);
        }
    }
}

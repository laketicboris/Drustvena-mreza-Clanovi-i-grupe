using Drustvena_mreza_Clanovi_i_grupe.Models;

namespace Drustvena_mreza_Clanovi_i_grupe.Repositories
{
    public class GrupaRepository
    {
        private const string filePath = "data/grupe.csv";
        private const string clanstvaPath = "data/clanstva.csv";
        public static Dictionary<int, Grupa> Data;

        public GrupaRepository()
        {
            if (Data == null)
            {
                Load();
                UcitajClanstva(clanstvaPath, KorisnikRepository.Data);
            }

        }

        private void UcitajClanstva(string clanstvaPath, Dictionary<int, Korisnik> korisnici)
        {
            if(!File.Exists(clanstvaPath))
            {
                return;
            }
            string[] lines = File.ReadAllLines(clanstvaPath);
            foreach(string line in lines)
            {
                string[] parts = line.Split(',');
                int korisnikId = int.Parse(parts[0]);
                int grupaId = int.Parse(parts[1]);

                if(Data.ContainsKey(grupaId) && korisnici.ContainsKey(korisnikId))
                {
                    Data[grupaId].Korisnici.Add(korisnici[korisnikId]);
                }
            }
        }

        private void Load()
        {
            Data = new Dictionary<int, Grupa>();
            string[] lines = File.ReadAllLines(filePath);
            foreach(string line in lines)
            {
                string[] parts = line.Split(',');
                int id = int.Parse(parts[0]);
                string ime = parts[1];
                DateTime datumOsnivanja = DateTime.Parse(parts[2]);

                Grupa grupa = new Grupa(id, ime, datumOsnivanja);
                Data[id] = grupa;
            }
        }

    }
}

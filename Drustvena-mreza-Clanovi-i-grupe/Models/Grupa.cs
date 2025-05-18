namespace Drustvena_mreza_Clanovi_i_grupe.Models
{
    public class Grupa
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public DateTime DatumOsnivanja { get; set; }
        public List<Korisnik> Korisnici { get; set; } = new List<Korisnik>();
        //Lista dodata zbog funkcionalnosti
        public Grupa(int id, string ime, DateTime datumOsnivanja)
        {
            Id = id;
            Ime = ime;
            DatumOsnivanja = datumOsnivanja;
        }
    }
}

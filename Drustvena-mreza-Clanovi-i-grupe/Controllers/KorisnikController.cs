using Drustvena_mreza_Clanovi_i_grupe.Models;
using Drustvena_mreza_Clanovi_i_grupe.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drustvena_mreza_Clanovi_i_grupe.Controllers
{
    [Route("api/korisnik")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private KorisnikRepository korisnikRepo = new KorisnikRepository();

        [HttpGet]
        public ActionResult<List<Korisnik>> GetAll()
        {
            List<Korisnik> korisnik = korisnikRepo.Data.Values.ToList();
            return Ok(korisnik);
        }

        [HttpGet("{id}")]
        public ActionResult<Korisnik> GetById(int id)
        {
            if (!korisnikRepo.Data.ContainsKey(id))
            {
                return NotFound($"Korisnik sa datim IDem {id} ne postoji");
            }
            return Ok(korisnikRepo.Data[id]);
        }

        [HttpPost]
        public ActionResult<Korisnik> Create([FromBody] Korisnik noviKorisnik)
        {
            if (string.IsNullOrWhiteSpace(noviKorisnik.KorisnickoIme) || string.IsNullOrWhiteSpace(noviKorisnik.Ime))
            {
                return BadRequest("Nedostaju obavezna polja.");
            }
            noviKorisnik.Id = SracunajNoviId(korisnikRepo.Data.Keys.ToList());
            korisnikRepo.Data[noviKorisnik.Id] = noviKorisnik;
            korisnikRepo.Save();

            return Ok(noviKorisnik);

        }

        [HttpPut("{id}")]
        public ActionResult<Korisnik> Update (int id, [FromBody] Korisnik uKorisnik)
        {
            if (string.IsNullOrWhiteSpace(uKorisnik.KorisnickoIme) || string.IsNullOrWhiteSpace(uKorisnik.Ime))
            {
                return BadRequest("Ime i korisnicko ime su obavezna polja.");
            }
            if (!korisnikRepo.Data.ContainsKey(id))
            {
                return NotFound();
            }
            Korisnik korisnik = korisnikRepo.Data[id];
            korisnik.KorisnickoIme = uKorisnik.KorisnickoIme;
            korisnik.Ime = uKorisnik.Ime;
            korisnik.Prezime = uKorisnik.Prezime;
            korisnik.DatumRodjenja = uKorisnik.DatumRodjenja;

            korisnikRepo.Save();
            return Ok(korisnik);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (int id)
        {
            if (!korisnikRepo.Data.ContainsKey(id))
            {
                return NotFound();
            }
            korisnikRepo.Data.Remove(id);
            korisnikRepo.Save();

            return NoContent();
        }

        private int SracunajNoviId(List<int> identifikatori)
        {
            int maxId = 0;
            foreach(int id in identifikatori)
            {
                if ( id > maxId)
                {
                    maxId = id;
                }
            }
            return maxId + 1;
        }
    }
}

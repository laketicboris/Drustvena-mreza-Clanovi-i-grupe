using Drustvena_mreza_Clanovi_i_grupe.Models;
using Drustvena_mreza_Clanovi_i_grupe.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drustvena_mreza_Clanovi_i_grupe.Controllers
{
    [Route("api/grupa/{grupaId}/korisnik")]
    [ApiController]
    public class GrupaKorisnikController : ControllerBase
    {
        private KorisnikRepository korisnikRepo = new KorisnikRepository();
        private GrupaRepository grupaRepo = new GrupaRepository();

        [HttpGet]
        public ActionResult<List<Korisnik>> GetClanovi (int grupaId)
        {
            if (!GrupaRepository.Data.ContainsKey(grupaId))
            {
                return NotFound("Grupa ne postoji.");
            }
            return Ok(GrupaRepository.Data[grupaId].Korisnici);
        }
    }
}

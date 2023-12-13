using Gestion_Livres.Models;
using Gestion_Livres.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Livres.Controllers
{
    [ApiController]
    [Route("api/livre/{livreId}/exemplaires")]
    public class ExemplaireController : ControllerBase
    {
        LivreService m_livreService;
        ExemplaireService m_exemplaireService;
        public ExemplaireController(LivreService P_livreService, ExemplaireService p_exemplaireService)
        {
            m_livreService = P_livreService;
            m_exemplaireService = p_exemplaireService;
        }

        // GET: api/livre/1/exemplaires
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<Exemplaire>> GetAllExemplaires(int livreId)
        {
            var livre = m_livreService.GetById(livreId);

            if(livre == null) 
            {
                return NotFound();
            }
            else
            {
               return Ok(m_exemplaireService.GetAllExemplaires(livreId));
            }
        }

        // GET: api/livre/1/exemplaires/5
        [HttpGet("{exemplaireId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Exemplaire> GetUnExemplaireById(int livreId, int exemplaireId)
        {
            var livre = m_livreService.GetById(livreId);
            var exemplaireRecherche = m_exemplaireService.GetUnExemplaireById(livreId, exemplaireId);

            if(livre == null || exemplaireRecherche == null) 
            {
                return NotFound();
            }
            else
            {
                return Ok(exemplaireRecherche);
            }
        }

        // POST: api/livre/1/exemplaires
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create(int livreId, Exemplaire nouvelExemplaire)
        {
            if(livreId !=  nouvelExemplaire.LivreId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var exemplaire = m_exemplaireService.Create(livreId, nouvelExemplaire);
            return CreatedAtAction(nameof(GetUnExemplaireById), new { id = exemplaire.ExemplaireId }, exemplaire);
        }

        // PUT: api/livre/1/exemplaires/5
        [HttpPut("{exemplaireId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int livreId, int exemplaireId, Exemplaire exemplaire)
        {
            if (!ModelState.IsValid || livreId != exemplaire.LivreId)
            {
                return BadRequest();
            }

            var exemplaireAModifier = m_exemplaireService.GetUnExemplaireById(livreId, exemplaireId);

            if (exemplaireAModifier == null)
            {
                return NotFound();
            }

            m_exemplaireService.Update(livreId, exemplaireId, exemplaire);
            return NoContent();
        }

        //DELETE: api/livre/1/exemplaires/5
        [HttpDelete("{exemplaireId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteById(int livreId, int exemplaireId)
        {
            var livre = m_livreService.GetById(livreId);
            var exemplaireASupprimer = m_exemplaireService.GetUnExemplaireById(livreId, exemplaireId);

            if(livre == null || exemplaireASupprimer == null)
            {
                return NotFound();
            }

            m_exemplaireService.DeleteById(livreId, exemplaireId);
            return NoContent();
        }
    }
}

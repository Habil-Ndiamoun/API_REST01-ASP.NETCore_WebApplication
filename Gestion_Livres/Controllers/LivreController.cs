using Gestion_Livres.Models;
using Gestion_Livres.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Livres.Controllers
{
    [ApiController]
    [Route("api/livre")]
    public class LivreController : ControllerBase
    {
        LivreService m_service;
        public LivreController(LivreService p_service)
        {
            m_service = p_service;
        }

        // GET: api/livre
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<Livre> GetAll()
        {
            return m_service.GetAll();
        }

        // GET: api/livre/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Livre> GetById(int id)
        {
            var livre = m_service.GetById(id);

            if(livre == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(livre);
            }
        }

        // POST: api/livre
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create(Livre nouveauLivre)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var livre = m_service.Create(nouveauLivre);
            return CreatedAtAction(nameof(GetById), new { id = livre!.LivreId }, livre);
        }


        // PUT: api/livre/4/addExemplaire?ExemplaireId=5
        [HttpPut("{id}/addExemplaire")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AddExemplaire(int id, int exemplaireId)
        {
            var livreAModifier = m_service.GetById(id);

            if(livreAModifier == null)
            {
                return NotFound();
            }
            else
            {
                m_service.AddUnExemplaireDansUnLivre(id, exemplaireId);
                return NoContent();
            }
        }

        // PUT: api/livre/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int id, Livre livre)
        {
            if(id != livre.LivreId || !ModelState.IsValid)
            { 
                return BadRequest(); 
            }

            var livreAModifier = m_service.GetById(id);
            
            if(livreAModifier == null)
            {
                return NotFound();
            }

            m_service.Update(livre);
            return NoContent();
        }

        //DELETE: api/livre/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var livreASupprimer = m_service.GetById(id);

            if(livreASupprimer == null)
            {
                return NotFound();
            }

            m_service.DeleteById(id);
            return NoContent();
        }
    }
}

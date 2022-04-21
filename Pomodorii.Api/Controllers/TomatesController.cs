using Pomodorii.Api.Models;
using Pomodorii.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomodorii.Api.Controllers
{
    /// <summary>
    /// On crée un controller pour gérer notre API
    /// La route sera "api/tomates"
    /// on implémente les principales types de requete http pour gérer nos tomates : GET, POST, PUT, DELETE
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TomatesController : ControllerBase
    {
        // le repository permettant d'accéder aux données
        private readonly ITomateRepository tomateRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tomateRepository"></param>
        public TomatesController(ITomateRepository tomateRepository)
        {
            this.tomateRepository = tomateRepository;
        }             


        [HttpGet]
        public async Task<ActionResult> GetTomates()
        {
            try
            {
                return Ok(await tomateRepository.GetTomates());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// la route sera "api/tomates/{id}"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tomate>> GetTomate(int id)
        {
            try
            {
                var result = await tomateRepository.GetTomate(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Tomate>> CreateTomate(Tomate Tomate)
        {
            try
            {
                if(Tomate == null) return BadRequest();

                var tomate = await tomateRepository.GetTomateByName(Tomate.Nom);
                
                if(tomate != null)
                {
                    ModelState.AddModelError("Nom", "Cette tomate existe déjà !");
                    return BadRequest(ModelState);
                }

                var createdTomate = await tomateRepository.AddTomate(Tomate);

                var res = CreatedAtAction(nameof(GetTomate), new { id = createdTomate.Id },
                    createdTomate);
                return res;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPut()]
        public async Task<ActionResult<Tomate>> UpdateTomate(Tomate Tomate)
        {
            try
            {
                var TomateToUpdate = await tomateRepository.GetTomate(Tomate.Id);

                if(TomateToUpdate == null)
                {
                    return NotFound($"Tomate with Id = {Tomate.Id} not found");
                }

                return await tomateRepository.UpdateTomate(Tomate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Tomate>> DeleteTomate(int id)
        {
            try
            {
                var TomateToDelete = await tomateRepository.GetTomate(id);

                if (TomateToDelete == null)
                {
                    return NotFound($"Tomate with Id = {id} not found");
                }

                return await tomateRepository.DeleteTomate(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
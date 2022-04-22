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
    public class SemisController : ControllerBase
    {
        // le repository permettant d'accéder aux données
        private readonly ISemiRepository semiRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tomateRepository"></param>
        public SemisController(ISemiRepository semiRepository)
        {
            this.semiRepository = semiRepository;
        }


        [HttpGet]
        public async Task<ActionResult> GetSemis()
        {
            try
            {
                return Ok(await semiRepository.GetSemis());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
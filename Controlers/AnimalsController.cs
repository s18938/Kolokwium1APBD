using Kolokwium1APBD.DTO.Requests;
using Kolokwium1APBD.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Kolokwium1APBD.Controlers
{
    [Route("api/animals")]
    [ApiController]    
    public class AnimalsController : Controller
    {
        
        private AnimalDbService _service;

        //dlaczego private?
        private AnimalsController(AnimalDbService service)
        {
            _service = service;
        }

        [HttpGet("{orderBy}")]
        public IActionResult GetAnimals(string orderBy)
        {
            try
            {
                return Ok(_service.FindAnimals(orderBy));
            }
            catch (SqlException)
            {
                return BadRequest();
            }

        }

        [HttpPost("add-animal")]
        public IActionResult AddAnimal(AnimalRequest request)
        {
            try
            {
                _service.InsertAnimal(request);
                return Ok();
            }
            catch (SqlException)
            {
                return NotFound();
            }

        }


    }
}
using mail_api.DTO;
using mail_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Net;

namespace mail_api.controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class Cepcontroller:ControllerBase
    {

        private readonly ICepService _cepService;
        public Cepcontroller(ICepService cepService)
        {
            _cepService = cepService;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> GetCep(string cep)
        {
            try
            {
                var result = await _cepService.GetByCep(cep);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("CEP not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex);
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] cepRequest cepRequest)
        {
            if (cepRequest == null || string.IsNullOrWhiteSpace(cepRequest.Cep))
            {
                return BadRequest("Invalid CEP.");
            }

            try
            {
                var result = await _cepService.PostAddressByCep(cepRequest);

                if (!result)
                {
                    return StatusCode(500, "Error saving data to the database.");
                }
                return Ok("Data successfully saved");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching data from the database", ex);
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }




    }
}

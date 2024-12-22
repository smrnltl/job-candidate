using JobCandidate.Core.Abstract;
using JobCandidate.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController(ICandidateService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDto candidateDto)
        {
            try
            {
                var candidate = await service.AddOrUpdateCandidateAsync(candidateDto);
                return Ok(candidate);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}

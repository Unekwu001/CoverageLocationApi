using Api.Models;
using Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CoverageController : ControllerBase
    {
        private readonly ICoverageService _coverageService;

        public CoverageController(ICoverageService coverageService)
        {
            _coverageService = coverageService;
        }



        [HttpGet("check-user-coverage")]
        public async Task<IActionResult> CheckNetworkCoverage(double userLatitude, double userLongitude)
        {
            try
            {                
                bool isInCoverageArea = await _coverageService.IsInCoverageAreaAsync(userLatitude, userLongitude);
                if (isInCoverageArea)
                {
                    return Ok(new { Message = "your location is within coverage area." });
                }
                else
                {
                    return Ok(new { Message = "your location is outside the coverage area." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("all-coverage-locations")]
        public async Task<IActionResult> GetAllCoverageLocations()
        {
            try
            {
                IEnumerable<CoverageLocation> coverageLocations = await _coverageService.GetAllCoverageLocationsAsync();
                return Ok(coverageLocations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }



}


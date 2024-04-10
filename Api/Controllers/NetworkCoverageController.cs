using Api.Models;
using ipNXSalesPortalApis.Dtos;
using ipNXSalesPortalApis.Services.GoogleServices;
using ipNXSalesPortalApis.Services.SalesPortalServices;
using Microsoft.AspNetCore.Mvc;
using SalesPortalApis.Dtos;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NetworkCoverageController : ControllerBase
    {
        private readonly ICoverageService _coverageService;
        private readonly IGoogleGeoCodingService _googleGeoCodingService;

        public NetworkCoverageController(ICoverageService coverageService, IGoogleGeoCodingService googleGeoCodingService)
        {
            _coverageService = coverageService;
            _googleGeoCodingService = googleGeoCodingService;
        }

        [HttpGet("check-network-coverage-availability")]
        public async Task<IActionResult> CheckNetworkCoverage([FromQuery] LocationDto locationdto)
        {
            try
            {
                var responseFromGoogle = _googleGeoCodingService.GetCoordinatesAsync(locationdto.Location);
                bool isInCoverageArea = await _coverageService.IsInCoverageAreaAsync(responseFromGoogle.Result.UserLatitude, responseFromGoogle.Result.UserLongitude);

                if (responseFromGoogle.Result.CloseCoverageLocations != null && responseFromGoogle.Result.CloseCoverageLocations.Any())
                {
                    var locationsMessage = string.Join(", ", responseFromGoogle.Result.CloseCoverageLocations.Select(location => location.CoverageName));
                    return Ok(new { Message = $"The locations closest to you are:{locationsMessage}" });
                }

                else if (isInCoverageArea)
                {
                    return Ok(new { Message = "your location is within ipNX Network Coverage Area." });
                }
                else
                {
                    return Ok(new { Message = "your location seems to be outside ipNX Network Coverage Area. Type in a more descriptive address." });
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


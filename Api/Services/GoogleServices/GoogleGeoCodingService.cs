using ipNXSalesPortalApis.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SalesPortalApis.Dtos;

namespace ipNXSalesPortalApis.Services.GoogleServices
{
    public class GoogleGeoCodingService : IGoogleGeoCodingService
    { 
        private readonly CoverageDbContext _coverageDbContext;

        public GoogleGeoCodingService(CoverageDbContext coverageDbContext)
        {            
            _coverageDbContext = coverageDbContext;
            
        }

        public async Task<LatitudeAndLongitudeDto> GetCoordinatesAsync(string location)
        {
            var record = await _coverageDbContext.GcpgeoCodingApiKeys.FirstOrDefaultAsync(k => k.Id == 1);
            var _GoogleGeocodingApiKey = record.Key;
            using (var client = new HttpClient())
            {
                var encodedLocation = Uri.EscapeDataString(location);
                var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={encodedLocation}&key={_GoogleGeocodingApiKey}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JObject.Parse(responseBody);
                    if (data.results.Count == 0)
                    {
                        //if google is not giving us the lat and long, then we check our ipNxDb to see if that location name is present with the lga or city
                        location = location.ToLower();
                        var locationWords = location.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        var LocationsFoundInipNXDb = await _coverageDbContext.CoverageLocations
                                                                .Where(coveragelocation =>
                                                                                        locationWords.Any(word =>
                                                                                            coveragelocation.Lga.ToLower().Contains(word.ToLower()) ||
                                                                                            coveragelocation.CoverageName.ToLower().Contains(word.ToLower()))).ToListAsync(); ;
                        if (LocationsFoundInipNXDb == null)
                        {
                            return new LatitudeAndLongitudeDto();
                        }
                        return new LatitudeAndLongitudeDto() { CloseCoverageLocations = LocationsFoundInipNXDb };

                    }
                    var latitude = (double)data.results[0].geometry.location.lat;
                    var longitude = (double)data.results[0].geometry.location.lng;

                    var networkCoverageDto = new LatitudeAndLongitudeDto
                    {
                        UserLatitude = latitude,
                        UserLongitude = longitude
                    };

                    return networkCoverageDto;
                }
                else
                {
                    throw new Exception("Failed to retrieve coordinates from Google Geocoding API.");

                }
            }
        }


    }


}

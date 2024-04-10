using SalesPortalApis.Dtos;

namespace ipNXSalesPortalApis.Services.GoogleServices
{
    public interface IGoogleGeoCodingService
    {
        Task<LatitudeAndLongitudeDto> GetCoordinatesAsync(string location);
    }
}

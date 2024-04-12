using ipNXSalesPortalApis.Models;

namespace ipNXSalesPortalApis.Services.SalesPortalServices
{
    public interface ICoverageService
    {
        Task<IEnumerable<CoverageLocation>> GetAllCoverageLocationsAsync();
        Task<bool> IsInCoverageAreaAsync(double latitude, double longitude);

    }
}

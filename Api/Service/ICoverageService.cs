using Api.Models;

namespace Api.Service
{
    public interface ICoverageService
    {
        Task<IEnumerable<CoverageLocation>> GetAllCoverageLocationsAsync();
        Task<bool> IsInCoverageAreaAsync(double latitude, double longitude);

    }
}

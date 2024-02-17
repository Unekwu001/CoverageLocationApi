using Api.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Api.Service
{
    public class CoverageService : ICoverageService
    {
        private readonly CoverageDbContext _coverageDbContext;

        public CoverageService(CoverageDbContext coverageDbContext)
        {
            _coverageDbContext = coverageDbContext;
        }

        public async Task<IEnumerable<CoverageLocation>> GetAllCoverageLocationsAsync()
        {
            return await _coverageDbContext.CoverageLocations.ToListAsync();
        }



        //public async Task<bool> IsInCoverageAreaAsync(double latitude, double longitude)
        //{
        //    var allCoverageLocations = await GetAllCoverageLocationsAsync();
        //    foreach (var coverageLocation in allCoverageLocations)
        //    {
        //        if ((latitude == coverageLocation.Latitude) && (longitude == coverageLocation.Longitude))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //This implementation of the IsInCoverageAreaAsync method compares the latitude and longitude of the specified
        //location with the latitude and longitude of each coverage location in your data.However, this implementation
        //may not be accurate because it checks for an exact match of latitude and longitude values, which is unlikely
        //due to floating-point precision issues.

        //    To improve the accuracy of the coverage area check, we should consider using a spatial library or algorithm
        //    to determine if the specified latitude and longitude fall within the boundaries of the coverage areas.One
        //    common approach is to use the Haversine formula for calculating distances between two points on the Earth's surface.
        //    Here's a revised implementation using the Haversine formula:
        //
        public async Task<bool> IsInCoverageAreaAsync(double latitude, double longitude)
        {
            var allCoverageLocations = await GetAllCoverageLocationsAsync();
            foreach (var coverageLocation in allCoverageLocations)
            {
                double coverageLatitude = coverageLocation.Latitude;
                double coverageLongitude = coverageLocation.Longitude;

                // Calculate distance between the specified location and the coverage location
                double distance = CalculateDistance(latitude, longitude, coverageLatitude, coverageLongitude);

                // If the distance is within a certain threshold (e.g., coverage radius), consider it within the coverage area
                if (distance <= MathUtils.CoverageRadiusInMeters)
                {
                    return true;
                }
                if (AreClose(latitude, coverageLatitude) && AreClose(longitude, coverageLongitude))
                {
                    return true;
                }
            }
            return false;
        }

        private double CalculateDistance(double userLatitude, double userLongitude, double databaseLatitude, double databaseLongitude)
        {
            // Implementation of the Haversine formula to calculate distance between two points on Earth's surface            
            double radius = 6371000; // Earth's radius in meters
            double userLat = MathUtils.ToRadians(userLatitude);
            double dbLat = MathUtils.ToRadians(databaseLatitude);
            double userLong = MathUtils.ToRadians(userLongitude);
            double dbLong = MathUtils.ToRadians(databaseLongitude);

            double diffInLatitude = dbLat - userLat;
            double diffInLongitude = dbLong - userLong;

            double a = Math.Sin(diffInLatitude / 2) * Math.Sin(diffInLatitude / 2) +
                       Math.Cos(userLat) * Math.Cos(dbLat) *
                       Math.Sin(diffInLongitude / 2) * Math.Sin(diffInLongitude / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = radius * c;

            return distance;
        }

        private bool AreClose(double value1, double value2, double tolerance = 50)
        {
            return Math.Abs(value1 - value2) < tolerance;
        }

    }
}

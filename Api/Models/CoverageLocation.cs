using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class CoverageLocation
{
    public int Id { get; set; }

    public string State { get; set; } = null!;

    public string CoverageName { get; set; } = null!;

    public string Lga { get; set; } = null!;

    public double Longitude { get; set; } 

    public double Latitude { get; set; } 
}

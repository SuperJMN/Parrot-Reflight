using System;
using System.Collections.Generic;
using System.Linq;
using Reflight.Core.Units;

namespace Reflight.Core
{
    public static class VectorMixin
    {
        public static double L2Norm(this Vector self)
        {
            return Math.Sqrt(self.Coordinates.Sum(arg => Math.Pow(arg, 2)));
        }
    }

    public static class UnitSource
    {
        static UnitSource()
        {
            UnitPacks = new List<UnitPack>
            {
                new UnitPack
                {
                    Id = "metrickms",
                    Name = "Metric (speed in Km/h)",
                    Speed = new KilometersPerHourUnit(),
                    Longitude = new KilometersUnit(),
                    Altitude = new MetersUnit(),
                },
                new UnitPack
                {
                    Id = "metricms",
                    Name = "Metric (speed in m/s)",
                    Speed = new MetersPerSecondUnit(),
                    Longitude = new MetersUnit(),
                    Altitude = new MetersUnit(),
                },
                new UnitPack
                {
                    Id = "imperial",
                    Name = "Imperial",
                    Speed = new MilesPerHourUnit(),
                    Longitude = new MilesUnit(),
                    Altitude = new FeetUnit()
                }
            };
        }

        public static ICollection<UnitPack> UnitPacks { get; }
    }
}
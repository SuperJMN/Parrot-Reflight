﻿namespace Reflight.Core.Units
{
    public class UnitPack
    {
        public string Name { get; set; }
        public IMeasurementUnit Speed { get; set; }
        public IMeasurementUnit Longitude { get; set; }
        public IMeasurementUnit Altitude { get; set; }
        public string Id { get; set; }
    }
}
using System;
using System.Collections.Generic;
using GeoCoordinatePortable;
using Reflight.Core;
using Reflight.Gui.ViewModels.Dashboards;

namespace Reflight.Gui.ViewModels
{
    public static class SettingsSampleData
    {
        public static ISimulationViewModel CreatePreviewViewModel(UnitPack up, VirtualDashboard vd)
        {
            var presentationOptions = new PresentationOptions {UnitPack = up, Dashboard = vd};
            return new DefaultSimulationViewModel(presentationOptions)
            {
                Altitude = new SamplePlottableViewModel(
                    new List<double> {0, 1, 2, 5, 8, 9, 19, 19, 29, 19, 12, 13, 23, 22, 21, 20, 19, 20, 21},
                    new Point(2, 2)),
                Speed = new SamplePlottableViewModel(
                    new List<double>
                        {0, 11, 12, 15, 18, 20, 22, 22, 22.3, 21, 2, 17.5, 5, 15.5, 18, 19, 19, 17, 18.75, 17, 16},
                    new Point(5, 16)),
                Status = new StatusViewModel(new Status
                {
                    TimeElapsed = new TimeSpan(0, 0, 11, 28),
                    Speed = new Vector(1, 2, 4),
                    AnglePhi = 0.3,
                    AnglePsi = 0.2,
                    AngleTheta = 0.4,
                    BatteryLevel = 0.75,
                    PitotSpeed = 9,
                    WifiStregth = -30,
                    TotalDistance = 1234,
                    ControllerPosition = new GeoCoordinate(0, 0, 0),
                    DronePosition = new GeoCoordinate(0, 0, 123)
                })
            };
        }
    }
}
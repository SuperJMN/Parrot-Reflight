using System;
using System.Collections.Generic;
using System.Linq;

namespace Reflight.Gui.ViewModels
{
    public class DroneModel
    {
        public int ProductId { get; }
        public string Name { get; }

        static readonly IEnumerable<DroneModel> drones = new List<DroneModel>()
        {
            new DroneModel(2324, "Anafi"),
            new DroneModel(2318, "Disco"),
        };

        private DroneModel(int productId, string name)
        {
            ProductId = productId;
            Name = name;
        }

        public static DroneModel FromProductId(int productId)
        {
            return drones.First(x => x.ProductId == productId);
        }
    }
}
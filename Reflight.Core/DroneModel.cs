using System.Collections.Generic;
using System.Linq;

namespace Reflight.Core
{
    public class DroneModel
    {
        public int ProductId { get; }
        public string Name { get; }
        public static DroneModel Disco => DroneModel.FromProductId(2318);
        public static DroneModel Anafi => DroneModel.FromProductId(2324);

        static readonly IEnumerable<DroneModel> Drones = new List<DroneModel>()
        {
            new DroneModel(2324, "Anafi"),
            new DroneModel(2318, "Disco"),
        };

        private DroneModel(int productId, string name)
        {
            ProductId = productId;
            Name = name;
        }

        public static DroneModel FromProductId(long productId)
        {
            return Drones.First(x => x.ProductId == productId);
        }
    }
}
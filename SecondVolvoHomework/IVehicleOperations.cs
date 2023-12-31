using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondVolvoHomework
{
    public interface IVehicleOperations
    {
        List<Vehicle> VehiclesByBrand(string brand);
        List<Vehicle> ListVehiclesByModelAndTenure(string chosenModel);
        decimal CalculateTotalFleetValue();
        List<Vehicle> GetVehiclesRequiringMaintenance();
    }
}

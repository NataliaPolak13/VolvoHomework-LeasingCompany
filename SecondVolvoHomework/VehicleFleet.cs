using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SecondVolvoHomework
{

    public class VehicleFleet : IVehicleOperations
    {
        private List<Vehicle> vehicles = new List<Vehicle>();

        public List<Vehicle> Vehicles
        {
            get { return vehicles; }
            set { vehicles = value; }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }

        public List<Vehicle> VehiclesByBrand(string brand)
        {
            return vehicles.Where(car => car.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Vehicle> ListVehiclesByModelAndTenure(string chosenModel)
        {
            return vehicles.Where(car =>
            {
                if (car.Model.Equals(chosenModel, StringComparison.OrdinalIgnoreCase))
                {
                    int yearsOfExploatation = DateTime.Now.Year - car.YearOfManufacture;
                    if (car is PassengerVehicle passengerVehicle)
                        return passengerVehicle.TravelDistance > 100000 || yearsOfExploatation > 5;
                    else if (car is CargoTransportVehicle cargoVehicle)
                        return cargoVehicle.TravelDistance > 1000000 || yearsOfExploatation > 15;
                }
                return false;
            }).ToList();

        }

        public decimal CalculateTotalFleetValue()
        {
            decimal totalValue = 0;

            foreach (var vehicle in vehicles)
            {
                totalValue += CalculateVehicleValue(vehicle);
            }

            return totalValue;
        }

        private decimal CalculateVehicleValue(Vehicle vehicle)
        {
            int yearsOfExploitation = DateTime.Now.Year - vehicle.YearOfManufacture;

            if (vehicle is PassengerVehicle passengerVehicle)
            {
                decimal lossRate = 0.10m; 
                return vehicle.Price * (decimal)Math.Pow((double)(1 - lossRate), yearsOfExploitation);
            }
            else 
            {
                decimal lossRate = 0.07m; 
                return vehicle.Price * (decimal)Math.Pow((double)(1 - lossRate), yearsOfExploitation);
            }

        }

    public List<Vehicle> GetVehiclesRequiringMaintenance()
        {
            return vehicles.Where(car =>
            {
                if (car is PassengerVehicle passengerVehicle)
                    return passengerVehicle.TravelDistance % 5000 <= 1000;
                else if (car is CargoTransportVehicle cargoVehicle)
                    return cargoVehicle.TravelDistance % 15000 <= 1000;

                return false;
            }).ToList();
        }
        public List<Vehicle> GetVehicles()
        {
            return vehicles;
        }
    }

}

using Newtonsoft.Json;
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
                totalValue = Math.Round(totalValue, 2);
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
                    return ((passengerVehicle.TravelDistance) % 5000) >= 4000;
                else if (car is CargoTransportVehicle cargoVehicle)
                    return ((cargoVehicle.TravelDistance) % 15000) >= 4000;

                return false;
            }).ToList();
        }
        public string CalculateComfortClass(Vehicle vehicle)
        {
            int yearsOfExploitation = DateTime.Now.Year - vehicle.YearOfManufacture;

            if (vehicle is PassengerVehicle passengerVehicle)
            {

                if (yearsOfExploitation <= 3 || passengerVehicle.TravelDistance < 10000)
                {
                    return "Premium";
                }
                else if (yearsOfExploitation <= 7 || passengerVehicle.TravelDistance < 50000)
                {
                    return "Standard";
                }
                else
                {
                    return "Economy";
                }

            }
            else if (vehicle is CargoTransportVehicle cargoTransportVehicle)
            {
                if (yearsOfExploitation <= 3 || cargoTransportVehicle.TravelDistance < 20000)
                {
                    return "Premium";
                }
                else if (yearsOfExploitation <= 7 || (cargoTransportVehicle.TravelDistance >= 20000 && cargoTransportVehicle.TravelDistance < 70000))
                {
                    return "Standard";
                }
                else
                {
                    return "Economy";
                }
            }
            else
                return "Unknown";


        }
        public List<Vehicle> VehiclesSortedByComfortClass(string chosenBrand, string chosenColor)
        {
            return vehicles
                .Where(vehicle => vehicle.Brand.Equals(chosenBrand, StringComparison.OrdinalIgnoreCase) &&
                 vehicle.Color.Equals(chosenColor, StringComparison.OrdinalIgnoreCase))
                .OrderBy(vehicle => CalculateComfortClass(vehicle))
                .ToList();
        }
        public List<Vehicle> GetAllVehicles()
        {
            return vehicles.ToList();
        }

        public int GetLastVehicleId()
        {
            if (vehicles.Any())
            {
                return vehicles.Max(vehicle => vehicle.Id);
            }
            else
            {
                Console.WriteLine("The fleet is empty. Returning default ID.");
                return 0;
            }
        }
    }

}

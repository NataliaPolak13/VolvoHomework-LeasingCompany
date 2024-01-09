using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondVolvoHomework
{
    public class Menu 
    {

        private IVehicleOperations vehicleOperations;
        private IJsonIO jsonIO;

        public Menu(IVehicleOperations vehicleOperations, IJsonIO jsonIO)
        {
            this.vehicleOperations = vehicleOperations;
            this.jsonIO = jsonIO;
        }
        bool firstLine = false;


        public void RunMenu()
        {
            while (true)
            {

                if (firstLine)
                {
                    Console.WriteLine();
                }

                Console.WriteLine("Welcome to the leasing company. ");
                Console.WriteLine("1. Display all vehicles");
                Console.WriteLine("2. Display vehicles of the selected brand");
                Console.WriteLine("3. Display vehicles exceeding operational tenure");
                Console.WriteLine("4. Calculate total fleet value");
                Console.WriteLine("5. Display vehicles requiring maintenance");
                Console.WriteLine("6. Display vehicles sorted by comfort class");
                Console.WriteLine("7. Add new vehicle");
                Console.WriteLine("8. Exit the program");

                firstLine = true;

                Console.Write("Choose an option: ");
                var option = GetNumber();



                switch(option)
                {
                    case 1:

                        DisplayAllVehicles();
                        break;

                    case 2:
                        DisplayAllVehiclesByBrand();
                        Console.Write("Enter the brand of the vehicle: ");
                        string brand = GetStringInput();
                        var vehicleByBrand = vehicleOperations.VehiclesByBrand(brand);
                        if (vehicleByBrand.Any())
                        { 
                            Console.WriteLine($"Our company has vehicles of {brand.ToLower()}: ");
                            foreach(var car in vehicleByBrand)
                            {
                                int i = 1;
                                Console.WriteLine($"{i}. {car.Brand} {car.Model} - {car.Color}");
                                i++;

                            }
                        }
                        else
                        {
                            Console.WriteLine($"Our company does not have vehicles of {brand.ToLower()}.");
                            Console.WriteLine();
                        }
                        break;

                    case 3:
                        DisplayAllVehiclesByModel();
                        Console.Write("Enter the model of the vehicle: ");
                        string model = GetStringInput();
                        var vehicleByTenure = vehicleOperations.ListVehiclesByModelAndTenure(model);
                        if (vehicleByTenure.Any()) 
                        {
                            Console.WriteLine("These cars of a chosen model exceeded a predetermined operational tenure: ");
                            foreach (var car in vehicleByTenure)
                            {
                                int i = 1;
                                Console.WriteLine($"{i}. {car.Brand} {car.Model}, car id: {car.Id}");
                                i++;

                            }
                        }
                        else
                        {
                            Console.WriteLine($"Each car of {model.ToLower()} model are able to use");
                            Console.WriteLine();
                        }


                        break;

                    case 4:
                        Console.WriteLine(vehicleOperations.CalculateTotalFleetValue());
                    break;

                    case 5:
                        var vehicleRequiringMaintenance = vehicleOperations.GetVehiclesRequiringMaintenance();
                        if (vehicleRequiringMaintenance.Any())
                        {
                            foreach (var car in vehicleRequiringMaintenance)
                            {
                                int i = 1;
                                Console.WriteLine($"car id: {car.Id}, {car.Brand} {car.Model} ");

                            }
                        }
                        else
                        {
                            Console.WriteLine($"All cars require no maintenance");
                            Console.WriteLine();
                        }
                        break;
                    case 6:
                        DisplayAllVehiclesByColorAndBrand();
                        Console.Write("Enter the brand of the vehicle: ");
                        string brandByComfort = GetStringInput();
                        Console.Write("Enter the color of the vehicle: ");
                        string colorByComfort = GetStringInput();
                        Console.WriteLine();
                        var vehicleByComfort = vehicleOperations.VehiclesSortedByComfortClass(brandByComfort, colorByComfort);
                        if (vehicleByComfort.Any())
                        {
                            Console.WriteLine($"Our company has vehicles of {brandByComfort.ToLower()} with color {colorByComfort.ToLower()}: ");
                            int i = 1;
                            foreach (var car in vehicleByComfort)
                            {
                                Console.WriteLine($"{i}. {car.Brand} {car.Color} and it's comfort class: {vehicleOperations.CalculateComfortClass(car)}");
                                i++;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Our company does not have these vehicles.");
                            Console.WriteLine();
                        }
                        break;
                    case 7:
                        AddVehicleFromConsole();
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Incorrect output. Try again.");
                        break;
                }
            }
        }
        public void SaveToJsonFile()
        {
            if (jsonIO == null)
            {
                Console.WriteLine("Cannot save to JSON file.");
                return;
            }

            jsonIO.SaveToJson(vehicleOperations as VehicleFleet, "FleetOfVehicleCompany.json");
        }

        public void LoadFromJsonFile(string filePath)
        {
            if (jsonIO == null)
            {
                Console.WriteLine("Cannot load from JSON file.");
                return;
            }

            var loadedFleet = jsonIO.LoadFromJson(filePath);
            vehicleOperations = loadedFleet;
        }
        static double GetNumber()
        {
            if (!double.TryParse(Console.ReadLine(), out double input))
                Console.WriteLine(new Exception("Your input is not a right number. Try again. "));
                Console.WriteLine();

            return input;
        }
        static string GetStringInput()
        {
            string input;
            bool isValid;

            do
            {
                input = Console.ReadLine().Replace(" ", ""); 
                isValid = !string.IsNullOrEmpty(input) && !input.All(char.IsDigit);

                if (!isValid)
                {
                    Console.WriteLine("Input must not be empty and cannot be a number. Try again.");
                    Console.WriteLine();
                }

            } while (!isValid);

            return input;
        }
        private void DisplayAllVehicles()
        {
            var allVehicles = vehicleOperations.GetAllVehicles();
            if (allVehicles.Any())
            {
                Console.WriteLine("All vehicles in our fleet:");
                foreach (var vehicle in allVehicles)
                {
                    Console.WriteLine($"Car's id: {vehicle.Id}, brand: {vehicle.Brand}, model: {vehicle.Model} and color: {vehicle.Color}");
                }
            }
            else
            {
                Console.WriteLine("The fleet is empty.");
            }
            Console.WriteLine();
        }
        private void DisplayAllVehiclesByBrand()
        {
            var allVehicles = vehicleOperations.GetAllVehicles();
            if (allVehicles.Any())
            {
                var uniqueModels = allVehicles.Select(vehicle => vehicle.Brand).Distinct();

                Console.WriteLine("Choose one of available brands: ");
                foreach (var brand in uniqueModels)
                {
                    Console.WriteLine(brand);
                }
            }
            else
            {
                Console.WriteLine("The fleet is empty.");
            }
            Console.WriteLine();
        }
        private void DisplayAllVehiclesByModel()
        {
            var allVehicles = vehicleOperations.GetAllVehicles();
            if (allVehicles.Any())
            {
                var uniqueModels = allVehicles.Select(vehicle => vehicle.Model).Distinct();

                Console.WriteLine("Choose one of available models: ");
                foreach (var model in uniqueModels)
                {
                    Console.WriteLine(model);
                }
            }
            else
            {
                Console.WriteLine("The fleet is empty.");
            }
            Console.WriteLine();
        }
        private void DisplayAllVehiclesByColorAndBrand()
        {
            var allVehicles = vehicleOperations.GetAllVehicles();

            if (allVehicles.Any())
            {
                Console.WriteLine("Available brands and colors:");

                foreach (var vehicle in allVehicles)
                {
                    Console.WriteLine($"{vehicle.Brand}, {vehicle.Color}");
                }
            }
            else
            {
                Console.WriteLine("The fleet is empty.");
            }

            Console.WriteLine();
        }


        private void AddVehicleFromConsole()
        {
            Console.Write("Is your car a passenger car? (yes/no): ");
            string isPassengerVehicle = GetStringInput().ToLower();

            while (!(isPassengerVehicle == "yes" || isPassengerVehicle == "no"))
            {
                Console.WriteLine("Incorrect input. Try again.");
                Console.Write("Is your car a passenger car? (yes/no): ");
                isPassengerVehicle = GetStringInput().ToLower();
            }

            Console.Write("Enter the brand of the vehicle: ");
            string brand = GetStringInput();

            Console.Write("Enter the model of the vehicle: ");
            string model = GetStringInput();

            Console.Write("Enter the color of the vehicle: ");
            string color = GetStringInput();

            Console.Write("Enter the year of manufacture of the vehicle: ");
            int yearOfManufacture = (int)GetNumber();

            Console.Write("Enter the registration number of the vehicle: ");
            string registrationNumber = Console.ReadLine();

            Console.Write("Enter the price of the vehicle: ");
            decimal price = (decimal)GetNumber();

            Vehicle newVehicle;

            if (isPassengerVehicle=="yes")
            {
                Console.Write("Enter the travel distance of the passenger vehicle: ");
                int travelDistance = (int)GetNumber();

                Console.Write("Enter the lessee rating of the passenger vehicle: ");
                decimal lesseeRating = (decimal)GetNumber();

                Console.Write("Enter the trip duration of the passenger vehicle: ");
                int tripDuration = (int)GetNumber();

                Console.Write("Enter the model coefficient of the passenger vehicle: ");
                decimal modelCoefficient = (decimal)GetNumber();

                newVehicle = new PassengerVehicle(color, model, brand, yearOfManufacture, vehicleOperations.GetLastVehicleId() + 1, registrationNumber, price, travelDistance, lesseeRating, tripDuration, modelCoefficient);
            }
            else if (isPassengerVehicle=="no") 
            {
                Console.Write("Enter the cargo weight of the cargo transport vehicle: ");
                decimal cargoWeight = (decimal)GetNumber();

                Console.Write("Enter the travel distance of the cargo transport vehicle: ");
                int travelDistance = (int)GetNumber();

                Console.Write("Enter the model coefficient of the cargo transport vehicle: ");
                decimal modelCoefficient = (decimal)GetNumber();

                Console.Write("Enter the trip duration of the cargo transport vehicle: ");
                int tripDuration = (int)GetNumber();

                newVehicle = new CargoTransportVehicle(color, model, brand, yearOfManufacture, vehicleOperations.GetLastVehicleId() + 1, registrationNumber, price, cargoWeight, travelDistance, modelCoefficient, tripDuration);
            }
            else
            {
                Console.WriteLine("Wrong answear");
                return;
            }
            vehicleOperations.AddVehicle(newVehicle);
            SaveToJsonFile();
            Console.WriteLine();

        }
    }

    
}

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
             var menuItems = new (string text, Action action)[]
             {
                 ("Display all vehicles", () => DisplayVehicles()),
                 ("Display vehicles of the selected brand", () => DisplaySelectedVehicles()),
                 ("Display vehicles exceeding operational tenure", () =>
                 {
                     DisplayAllVehiclesByModel();
                     Console.Write("Enter the model of the vehicle: ");
                     string model = GetStringInput();
                     var vehicleByTenure = vehicleOperations.ListVehiclesByModelAndTenure(model);
                     Console.WriteLine(vehicleByTenure.Any()
                         ? $"These cars of a chosen model exceeded a predetermined operational tenure: \n{string.Join(Environment.NewLine, vehicleByTenure.Select((car, i) => $"{i + 1}. {car.Brand} {car.Model}, car id: {car.Id}"))}"
                         : $"Each car of {model.ToLower()} model are able to use\n");
                 }),
                 ("Calculate total fleet value", () => Console.WriteLine(vehicleOperations.CalculateTotalFleetValue())),
                 ("Display vehicles requiring maintenance", () =>
                 {
                     var vehicleRequiringMaintenance = vehicleOperations.GetVehiclesRequiringMaintenance();
                     Console.WriteLine(vehicleRequiringMaintenance.Any()
                         ? string.Join(Environment.NewLine, vehicleRequiringMaintenance.Select((car, i) => 
                         $"car id: {car.Id}, {car.Brand} {car.Model}"))
                         : "All cars require no maintenance");
                     Console.WriteLine();
                 }),
                 ("Display vehicles sorted by comfort class", () =>
                 {
                     DisplayAllVehiclesByColorAndBrand();
                     Console.Write("Enter the brand of the vehicle: ");
                     string brandByComfort = GetStringInput();
                     Console.Write("Enter the color of the vehicle: ");
                     string colorByComfort = GetStringInput();
                     Console.WriteLine();
                     var vehicleByComfort = vehicleOperations.VehiclesSortedByComfortClass(brandByComfort, colorByComfort);
                     Console.WriteLine(vehicleByComfort.Any()
                         ? $"Our company has vehicles of {brandByComfort.ToLower()} with color {colorByComfort.ToLower()}: " +
                         $"\n{string.Join(Environment.NewLine, vehicleByComfort.Select((car, i) => 
                         $"{i + 1}. {car.Brand} {car.Color} and it's comfort class: {vehicleOperations.CalculateComfortClass(car)}"))}"
                         : $"Our company does not have these vehicles.\n");
                 }),
                 ("Add new vehicle", () => AddVehicleFromConsole()),
                 ("Exit the program", () => Environment.Exit(0)),

             };

             while (true)
             {

                 if (firstLine)
                 {
                     Console.WriteLine();
                 }

                 Console.WriteLine("Welcome to the leasing company. ");

                 foreach (var (text, _, index) in menuItems.Select((x, i) => (x.text, x.action, i)))
                 {
                     Console.WriteLine($"{index + 1}. {text}");
                 }

                 firstLine = true;

                Console.Write("Choose an option: ");
                var option = (int)GetNumber();
                if (option < 1 || option > menuItems.Length)
                 {
                     Console.WriteLine("Invalid option. Please try again.");
                     continue;
                 }

                 menuItems[option - 1].action();
             }
        }

        private void DisplaySelectedVehicles()
        {
            DisplayAllVehiclesByBrand();
            Console.Write("Enter the brand of the vehicle: ");
            string brand = GetStringInput();
            var vehicleByBrand = vehicleOperations.VehiclesByBrand(brand);

            Console.WriteLine(vehicleByBrand.Any()
                ? $"Our company has vehicles of {brand.ToLower()}: " +
                $"\n{string.Join("\n", vehicleByBrand.Select((car, i) => 
                $"{i + 1}. {car.Brand} {car.Model} - {car.Color}"))}"
                : $"Our company does not have vehicles of {brand.ToLower()}.\n");

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
        private void DisplayVehicles(
            Func<Vehicle, bool> filter = null,
            Func<Vehicle, string> displayTemplate = null)
        {
            filter ??= (_) => true;
            displayTemplate ??= (vehicle) => $"Car's id: {vehicle.Id}, brand: {vehicle.Brand}, model: {vehicle.Model} and color: {vehicle.Color}";

            var allVehicles = vehicleOperations.GetAllVehicles();
            if (allVehicles.Any())
            {
                Console.WriteLine("All vehicles in our fleet:");
                var vehiclesPassingFilter = allVehicles.Select(filter);
                foreach (var displayValue in allVehicles.Select(displayTemplate).Distinct())
                {
                    //Console.WriteLine($"Car's id: {vehicle.Id}, brand: {vehicle.Brand}, model: {vehicle.Model} and color: {vehicle.Color}");
                    Console.WriteLine(displayValue);
                }
            }
            else
            {
                Console.WriteLine("The fleet is empty.");
            }
            Console.WriteLine();
        }
        private void DisplayAllVehiclesByBrand() => DisplayVehicles(displayTemplate: (v) => v.Brand.ToString());
        private void DisplayAllVehiclesByModel()
        {
            var allVehicles = vehicleOperations.GetAllVehicles();

            Console.WriteLine(allVehicles.Any()
                ? "Choose one of available models: \n" + 
                string.Join("\n", allVehicles.Select(vehicle => vehicle.Model).Distinct())
                : "The fleet is empty.\n");

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

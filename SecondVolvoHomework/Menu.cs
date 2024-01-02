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

        public Menu(IVehicleOperations vehicleOperations)
        {
            this.vehicleOperations = vehicleOperations;
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

                Console.WriteLine("1. Display vehicles of the selected brand");
                Console.WriteLine("2. Display vehicles exceeding operational tenure");
                Console.WriteLine("3. Calculate total fleet value");
                Console.WriteLine("4. Display vehicles requiring maintenance");
                Console.WriteLine("5. Display vehicles sorted by comfort class");
                Console.WriteLine("6. Exit the program");

                firstLine = true;

                Console.Write("Choose an option: ");
                var option = GetNumber();



                switch(option)
                {
                    case 1:
                        Console.Write("Enter the brand of the vehicle: ");
                        string brand = Console.ReadLine();
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

                    case 2:
                        Console.Write("Enter the model of the vehicle: ");
                        string model = Console.ReadLine().Replace(" ", "");
                        var vehicleByTenure = vehicleOperations.ListVehiclesByModelAndTenure(model);
                        if (vehicleByTenure.Any()) 
                        { 
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

                    case 3:
                        Console.WriteLine(vehicleOperations.CalculateTotalFleetValue());
                    break;

                    case 4:
                        var vehicleRequiringMaintenance = vehicleOperations.GetVehiclesRequiringMaintenance();
                        if (vehicleRequiringMaintenance.Any())
                        {
                            foreach (var car in vehicleRequiringMaintenance)
                            {
                                int i = 1;
                                Console.WriteLine($"{i}. {car.Brand} {car.Model}, car id: {car.Id}");
                                i++;

                            }
                        }
                        else
                        {
                            Console.WriteLine($"All cars require no maintenance");
                            Console.WriteLine();
                        }
                        break;
                    case 5:
                        Console.Write("Enter the brand of the vehicle: ");
                        string brandByComfort = Console.ReadLine().Replace(" ", "");
                        Console.Write("Enter the color of the vehicle: ");
                        string colorByComfort = Console.ReadLine().Replace(" ", "");
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
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Incorrect output. Try again.");
                        break;
                }
            }
        }
        static double GetNumber()
        {
            if (!double.TryParse(Console.ReadLine(), out double input))
                Console.WriteLine(new Exception("Your input is not a number. Try again. "));
            Console.WriteLine();

            return input;
        }
    }
}

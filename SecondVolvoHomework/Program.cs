using Newtonsoft.Json;
using System;

namespace SecondVolvoHomework
{
    public class Program
    {
        static void Main(string[] args)
        {
            string jsonFilePath = "FleetOfVehicleCompany.json";
            VehicleFleet leasingCompany = LoadFromJsonFile(jsonFilePath);

            Menu menu = new Menu(leasingCompany);
            menu.RunMenu();

            SaveToJsonFile(leasingCompany, jsonFilePath);

            static void SaveToJsonFile(VehicleFleet fleet, string filePath)
            {
                string json = JsonConvert.SerializeObject(fleet, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }

            static VehicleFleet LoadFromJsonFile(string filePath)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        string json = File.ReadAllText(filePath);
                        var fleet = JsonConvert.DeserializeObject<VehicleFleet>(json);
                        return fleet;
                    }
                    else
                    {
                        Console.WriteLine("File does not exist. Creating a new fleet.");
                        return new VehicleFleet();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading data from JSON file: {ex.Message}");
                    return new VehicleFleet();
                }
            }
        }
    }
}

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
            SaveToJsonFile(leasingCompany, jsonFilePath);


            SaveToJsonFile(leasingCompany, jsonFilePath);

            Menu menu = new Menu(leasingCompany);
            menu.RunMenu();


            static void SaveToJsonFile(VehicleFleet fleet, string filePath)
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    Formatting = Formatting.Indented
                };
                string json = JsonConvert.SerializeObject(fleet, settings);
                File.WriteAllText(filePath, json);
            }

            static VehicleFleet LoadFromJsonFile(string filePath)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        string json = File.ReadAllText(filePath);
                        var settings = new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All // Adds information about the type of object during serialization
                        };

                        var fleet = JsonConvert.DeserializeObject<VehicleFleet>(json, settings);
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

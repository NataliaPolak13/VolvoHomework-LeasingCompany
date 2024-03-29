using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondVolvoHomework
{
    public class JsonIO : IJsonIO
    {
        private JsonSerializerSettings jsonSerializerSettings;

        public JsonIO()
        {
            jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
        }

        public void SaveToJson(VehicleFleet fleet, string filePath)
        {
            string json = JsonConvert.SerializeObject(fleet, jsonSerializerSettings);
            File.WriteAllText(filePath, json);
        }

        public VehicleFleet LoadFromJson(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File does not exist. Creating a new fleet.");
                    return new VehicleFleet();
                }

                string json = File.ReadAllText(filePath);
                var loadedFleet = JsonConvert.DeserializeObject<VehicleFleet>(json, jsonSerializerSettings);
                loadedFleet ??= new VehicleFleet();
                return loadedFleet;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from JSON file: {ex.Message}");
                return new VehicleFleet();
            }
        }
    }
}

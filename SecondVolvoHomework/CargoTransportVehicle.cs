using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondVolvoHomework
{
    public class CargoTransportVehicle : Vehicle
    {
        public int TravelDistance { get; set; }
        public int TripDuration { get; set; }
        public decimal ModelCoefficient { get; set; }
        public decimal CargoWeight { get; set; }

        public CargoTransportVehicle(string color, string model, string brand, int yearOfManufacture, int id, string registrationNumber, decimal price, decimal cargoWeight, int travelDistance, decimal modelCoefficient, int tripDuration)
        : base (color, model, brand, yearOfManufacture, id, registrationNumber, price)
        {
            CargoWeight = cargoWeight;
            TravelDistance = travelDistance;
            ModelCoefficient = modelCoefficient;
            TripDuration = tripDuration;
        }
    }
}

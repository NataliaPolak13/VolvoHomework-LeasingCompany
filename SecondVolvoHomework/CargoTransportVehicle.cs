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
        public decimal CargoWeight { get; private set; }
        public int TravelDistance { get; private set; }
        public decimal ModelCoefficient { get; private set; }
        public int TripDuration { get; private set; }

        public CargoTransportVehicle(
            string color,
            string model,
            string brand,
            int yearOfManufacture,
            int id,
            string registrationNumber,
            decimal price,
            decimal cargoWeight,
            int travelDistance,
            decimal modelCoefficient,
            int tripDuration)
            : base(color, model, brand, yearOfManufacture, id, registrationNumber, price)
        {
            CargoWeight = cargoWeight;
            TravelDistance = travelDistance;
            ModelCoefficient = modelCoefficient;
            TripDuration = tripDuration;
        }

        public override decimal GetVehicleMonetaryValue()
        {
            int yearsOfExploitation = DateTime.Now.Year - YearOfManufacture;
            decimal lossRate = 0.07m;
            return Price * (decimal)Math.Pow((double)(1 - lossRate), yearsOfExploitation);
        }
    }
}
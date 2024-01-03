using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondVolvoHomework
{
    public class PassengerVehicle : Vehicle
    {
        public int TravelDistance { get; set; }
        public decimal LesseeRating { get; set; }
        public int TripDuration { get; set; }
        public decimal ModelCoefficient { get; set; }



        public PassengerVehicle(string color, string model, string brand, int yearOfManufacture, int id, string registrationNumber, decimal price, int travelDistance, decimal lesseeRating, int tripDuration, decimal modelCoefficient)
        : base(color, model, brand, yearOfManufacture, id, registrationNumber, price)
        {
            TravelDistance = travelDistance;
            LesseeRating = lesseeRating;
            TripDuration = tripDuration;
            ModelCoefficient = modelCoefficient;
        }

    }
}

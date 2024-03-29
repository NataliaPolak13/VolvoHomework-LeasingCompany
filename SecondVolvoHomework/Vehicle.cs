using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SecondVolvoHomework
{
    public abstract class Vehicle
    {
        public string Color { get; private set; }
        public string Model { get; private set; }
        public string Brand { get; private set; }
        public int YearOfManufacture { get; private set; }
        public int Id { get; private set; }
        public string RegistrationNumber { get; private set; }
        public decimal Price { get; private set; }

        public Vehicle(string color, string model, string brand, int yearOfManufacture, int id, string registrationNumber, decimal price)
        {
            Color = color;
            Model = model;
            Brand = brand;
            YearOfManufacture = yearOfManufacture;
            Id = id;
            RegistrationNumber = registrationNumber;
            Price = price;
        }

        public abstract decimal GetVehicleMonetaryValue();

    }
}

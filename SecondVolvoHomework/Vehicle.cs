using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SecondVolvoHomework
{
    public class Vehicle
    {
        public string Color { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int YearOfManufacture { get; set; }
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal Price { get; set; }

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

    }
}

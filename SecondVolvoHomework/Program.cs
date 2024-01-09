using Newtonsoft.Json;
using System;

namespace SecondVolvoHomework
{
    public class Program
    {
        static void Main(string[] args)
        {
            var leasingCompany = VehicleFleet.LoadFromJsonFile("FleetOfVehicleCompany.json");

            Menu menu = new Menu(leasingCompany);
            menu.RunMenu();

            VehicleFleet.SaveToJsonFile(leasingCompany, "FleetOfVehicleCompany.json");

        }

    }
}

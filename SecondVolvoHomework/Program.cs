using Newtonsoft.Json;
using System;

namespace SecondVolvoHomework
{
    public class Program
    {
        static void Main(string[] args)
        {
            var jsonIO = new JsonIO();
            var leasingCompany = jsonIO.LoadFromJson("FleetOfVehicleCompany.json");

            Menu menu = new Menu(leasingCompany, jsonIO);
            menu.RunMenu();

            jsonIO.SaveToJson(leasingCompany, "FleetOfVehicleCompany.json");

        }

    }
}

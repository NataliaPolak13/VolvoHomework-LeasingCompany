using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondVolvoHomework
{
    public interface IJsonIO
    {
        void SaveToJson(VehicleFleet fleet, string filePath);
        VehicleFleet LoadFromJson(string filePath);
    }
}

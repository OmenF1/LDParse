using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDParse.Classes
{
    internal class VenueMeta
    {
        public VenueMeta()
        {
            Vehicle = new VehicleMeta();
        }

        public string? Name { get; set; }
        public UInt16 VehiclePointer { get; set; }
        public VehicleMeta? Vehicle { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDParse.Classes
{
    internal class VehicleMeta
    {
        public string? Id { get; set; }
        public UInt16 Weight { get; set; }
        public string? Type { get; set; }
        public string? Comment { get; set; }
    }
}

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

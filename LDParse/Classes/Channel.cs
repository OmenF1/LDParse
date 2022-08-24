namespace LDParse.Classes
{
    internal class Channel
    {
        public UInt16 PreviousMetaAddress { get; set; }
        public UInt16 NextMetaAddress { get; set; }
        public UInt16 DataAddress { get; set; }
        public UInt16 DataLength { get; set; }
        public ushort DataType { get; set; }
        public ushort DataType2 { get; set; }
        public ushort DataFrequency { get; set; }
        public ushort Shift { get; set; }
        public ushort Mul { get; set; }
        public ushort Scale { get; set; }
        public ushort DecPlaces { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Unit { get; set; }
    }
}

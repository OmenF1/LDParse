namespace LDParse.Classes
{
    internal class EventMeta
    {
        public EventMeta()
        {
            Venue = new VenueMeta();
        }

        public string Name { get; set; }
        public string Session { get; set; }
        public string Comment { get; set; }
        public UInt16 VenuePointer { get; set; }
        public VenueMeta? Venue { get; set; }
    }
}

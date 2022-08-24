using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDParse.Classes
{
    internal class Header
    {
        public Header()
        {
            EventMetaData = new EventMeta();
        }

        public ushort ChannelMetaPointer { get; set; }
        public ushort ChannelDataPointer { get; set; }
        public ushort EventPointer { get; set; }
        public char[]? Date { get; set; }
        public char[]? Time { get; set; }
        public char[]? Driver { get; set; }
        public char[]? VehicleID { get; set; }
        public char[]? Venue { get; set; }
        public char[]? ShortComment { get; set; }
        public char[]? Event { get; set; }
        public char[]? Session { get; set; }
        public EventMeta EventMetaData { get; set; }
    }
}

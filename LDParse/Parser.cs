using System.Buffers.Binary;
using System.Text;
using System.Threading.Channels;
using LDParse.Classes;

namespace LDParse
{
    public class Parser
    {
        private string _filePath;
        private Header header;

        public Parser(string filePath)
        {
            _filePath = filePath;
            header = new Header();
        }

        public bool Parse()
        {
            try
            {
                using var fs = File.OpenRead(_filePath);
                using var reader = new BinaryReader(fs, Encoding.ASCII);
                if (!ReadHeader(reader))
                {
                    return false;
                }

                List<LDParse.Classes.Channel> channels = new List<LDParse.Classes.Channel>();
                ushort channelAddress = header.ChannelMetaPointer;
                while (channelAddress != 0)
                {
                    var tmp = ReadChannel(reader, channelAddress);
                    channels.Add(tmp);
                    channelAddress = tmp.NextMetaAddress;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ReadHeader(BinaryReader reader)
        {
            try
            {

                reader.BaseStream.Seek(8, SeekOrigin.Begin);
                header.ChannelMetaPointer = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(4));
                header.ChannelDataPointer = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(4));
                reader.BaseStream.Seek(20, SeekOrigin.Current);
                header.EventPointer = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(4));

                //  Read in date and time values.
                reader.BaseStream.Seek(50, SeekOrigin.Current);
                header.Date = reader.ReadChars(16);
                reader.BaseStream.Seek(16, SeekOrigin.Current);
                header.Time = reader.ReadChars(16);

                //  Read in remaining meta.
                reader.BaseStream.Seek(16, SeekOrigin.Current);
                header.Driver = reader.ReadChars(64);
                header.VehicleID = reader.ReadChars(64);
                reader.BaseStream.Seek(64, SeekOrigin.Current);
                header.Venue = reader.ReadChars(64);
                reader.BaseStream.Seek(1158, SeekOrigin.Current);
                header.ShortComment = reader.ReadChars(64);
                reader.BaseStream.Seek(126, SeekOrigin.Current);
                header.Event = reader.ReadChars(64);
                header.Session = reader.ReadChars(64);

                //  Read additional header meta, this can be changed quite a bit.
                //  It will do for now while I'm working through the actual structure of the file but really does need to be redone.

                if (header.EventPointer > 0)
                {
                    reader.BaseStream.Seek(header.EventPointer, SeekOrigin.Begin);
                    header.EventMetaData.Name = GetStringFromChars(reader.ReadChars(64));
                    header.EventMetaData.Session = GetStringFromChars(reader.ReadChars(64));
                    header.EventMetaData.Comment = GetStringFromChars(reader.ReadChars(1024));
                    header.EventMetaData.VenuePointer= BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(2));
                    
                }

                if (header.EventMetaData.VenuePointer > 0)
                {
                    reader.BaseStream.Seek(header.EventMetaData.VenuePointer, SeekOrigin.Begin);
                    header.EventMetaData.Venue.Name = GetStringFromChars(reader.ReadChars(64));
                    reader.BaseStream.Seek(1034, SeekOrigin.Current);
                    header.EventMetaData.Venue.VehiclePointer = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(2));
                }

                if (header.EventMetaData.Venue.VehiclePointer > 0)
                {
                    reader.BaseStream.Seek(header.EventMetaData.Venue.VehiclePointer, SeekOrigin.Begin);
                    header.EventMetaData.Venue.Vehicle.Id = GetStringFromChars(reader.ReadChars(64));
                    reader.BaseStream.Seek(128, SeekOrigin.Current);
                    header.EventMetaData.Venue.Vehicle.Weight = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(4));
                    header.EventMetaData.Venue.Vehicle.Type = GetStringFromChars(reader.ReadChars(32));
                    header.EventMetaData.Venue.Vehicle.Comment = GetStringFromChars(reader.ReadChars(32));
                }
                


                return true;
            }
            catch
            {
                return false;
            }
        }

        private LDParse.Classes.Channel ReadChannel(BinaryReader reader, ushort channelAddress)
        {
            try
            {
                LDParse.Classes.Channel channel = new LDParse.Classes.Channel();
                reader.BaseStream.Seek(channelAddress, SeekOrigin.Begin);
                channel.PreviousMetaAddress = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(4));
                channel.NextMetaAddress = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(4));
                channel.DataAddress = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(4));
                channel.DataLength = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(4));
                reader.BaseStream.Seek(2, SeekOrigin.Current);
                channel.DataType = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(2));
                channel.DataType2 = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(2));
                channel.DataFrequency = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(2));
                channel.Shift = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(2));
                channel.Mul = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(2));
                channel.Scale = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(2));
                channel.DecPlaces = BinaryPrimitives.ReadUInt16LittleEndian(reader.ReadBytes(2));
                channel.Name = GetStringFromChars(reader.ReadChars(32));
                channel.ShortName = GetStringFromChars(reader.ReadChars(8));
                channel.Unit = GetStringFromChars(reader.ReadChars(12));


                return channel;
            }
            catch
            {
                return null;
            }
        }

        private string GetStringFromChars(char[] charArray)
        {
            return new string(charArray).Replace($"\0", "");
        }
    }
}

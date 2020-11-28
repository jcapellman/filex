using System.Text;

namespace filex.Parsers.PCAP.Objects
{
    public class PCAPFeatureExtractionRequestItem
    {
        public string PayloadContent { get; set; }

        public PCAPFeatureExtractionRequestItem(byte[] packet)
        {
            PayloadContent = Encoding.ASCII.GetString(packet);
        }
    }
}
using System.IO;
using System.Text;

namespace filex.Parsers.PCAP.Objects
{
    public class PCAPFeatureExtractionRequestItem
    {
        public string PayloadContent { get; set; }

        public float[] NgramFeatures { get; set; }

        public bool Label { get; set; }

        public PCAPFeatureExtractionRequestItem(byte[] packet, bool label = true)
        {
            PayloadContent = Encoding.ASCII.GetString(packet);
            Label = label;
        }

        public PCAPFeatureExtractionRequestItem(string fileName) : this(File.ReadAllBytes(fileName), fileName.Contains("benign")) { }
    }
}
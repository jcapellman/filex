using System.IO;
using System.Text;

using filex.Common;

namespace filex.Parsers.PCAP.Objects
{
    public class PCAPFeatureExtractionRequestItem
    {
        public string PayloadContent { get; set; }

        public float[] NgramFeatures { get; set; }

        public float Label { get; set; }

        public PCAPFeatureExtractionRequestItem(byte[] packet, bool label = true)
        {
            PayloadContent = Encoding.ASCII.GetString(packet);
            Label = label ? Constants.FLOAT_TRUE : Constants.FLOAT_FALSE;
        }

        public PCAPFeatureExtractionRequestItem(string fileName) : this(File.ReadAllBytes(fileName), fileName.Contains("benign")) { }
    }
}
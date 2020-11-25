using filex.Parsers.Base;

namespace filex.Parsers
{
    public class PCAPParser : BaseParser
    {
        public override string Name => "PCAP";

        public override bool IsParseable(byte[] data)
        {
            return false;
        }
    }
}
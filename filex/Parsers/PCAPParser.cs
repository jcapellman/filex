using System;

using filex.Parsers.Base;

using SharpPcap;
using SharpPcap.LibPcap;

namespace filex.Parsers
{
    public class PCAPParser : BaseParser
    {
        public override string Name => "PCAP";

        public override bool IsParseable(byte[] data, string fileName)
        {
            try
            {
                ICaptureDevice device = new CaptureFileReaderDevice(fileName);

                device.Open();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
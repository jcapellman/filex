using System;
using System.IO;

using filex.Objects;
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
            if (fileName is null)
            {
                throw new ArgumentNullException("FileName cannot be null");
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"Could not find {fileName}");
            }

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

        public override ModelPredictionResponse RunModel(byte[] data, string fileName)
        {
            return new ModelPredictionResponse();
        }
    }
}
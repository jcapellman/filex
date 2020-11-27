using System;
using System.Collections.Generic;
using System.IO;

using filex.Common;
using filex.Objects;
using filex.Parsers.Base;
using filex.Parsers.PCAP.Objects;

using Microsoft.ML;

using PacketDotNet;

using SharpPcap;
using SharpPcap.LibPcap;

namespace filex.Parsers.PCAP
{
    public class PCAPParser : BaseParser
    {
        private const string MODEL_NAME = "pcap.mdl";

        public override string Name => "PCAP";

        private PredictionEngine<PCAPFeatureExtractionRequestItem, ModelPredictionResponse> _mlEngine;

        private List<EthernetPacket> packets = new List<EthernetPacket>();

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

                device.Close();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private PCAPFeatureExtractionRequestItem ParsePackets()
        {
            var requestItem = new PCAPFeatureExtractionRequestItem();

            foreach (var packet in packets)
            {
                // TODO: Parse for Features
            }

            return requestItem;
        }

        public override ModelPredictionResponse RunModel(byte[] data, string fileName)
        {
            ICaptureDevice device = new CaptureFileReaderDevice(fileName);

            device.Open();

            device.OnPacketArrival += device_OnPacketArrival;

            device.Capture();

            device.Close();

            return _mlEngine.Predict(ParsePackets());
        }

        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            if (e.Packet.LinkLayerType != PacketDotNet.LinkLayers.Ethernet)
            {
                return;
            }

            var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            var ethernetPacket = (EthernetPacket) packet;

            packets.Add(ethernetPacket);
        }

        public override void LoadModel()
        {
            if (_mlEngine != null)
            {
                return;
            }

            if (!File.Exists(MODEL_NAME))
            {
                throw new FileNotFoundException($"Could not find PCAP Model {MODEL_NAME}");
            }

            var mlContext = new MLContext(Constants.ML_SEED);

            var model = mlContext.Model.Load(MODEL_NAME, out _);

            _mlEngine = mlContext.Model.CreatePredictionEngine<PCAPFeatureExtractionRequestItem, ModelPredictionResponse>(model);
        }
    }
}
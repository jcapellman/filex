using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        private List<ModelPredictionResponse> _packetPredictions = new List<ModelPredictionResponse>();

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

        public override ModelPredictionResponse RunModel(byte[] data, string fileName)
        {
            ICaptureDevice device = new CaptureFileReaderDevice(fileName);

            device.Open();

            device.OnPacketArrival += device_OnPacketArrival;

            device.Capture();

            device.Close();

            return _packetPredictions.FirstOrDefault();
        }

        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            if (e.Packet.LinkLayerType != PacketDotNet.LinkLayers.Ethernet)
            {
                return;
            }

            var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            var ethernetPacket = (EthernetPacket) packet;

            var requestItem = new PCAPFeatureExtractionRequestItem(ethernetPacket.PayloadPacket.Bytes);

            _packetPredictions.Add(_mlEngine.Predict(requestItem));
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

        public override ModelTrainingMetricsResponse TrainModel(string trainingPath)
        {
            if (string.IsNullOrEmpty(trainingPath))
            {
                throw new ArgumentNullException($"TrainingDataPath was not set");
            }

            var mlContext = new MLContext(Constants.ML_SEED);

            var files = GetFiles(trainingPath);

            var data = new List<PCAPFeatureExtractionRequestItem>();

            foreach (var file in files)
            {
                //    data.Add(RunModel(null, file));
            }

            var dataView = mlContext.Data.LoadFromEnumerable(data);

            var split = mlContext.Data.TrainTestSplit(dataView, testFraction: .5);

            var pipeline = mlContext.Transforms.Concatenate(
                    "Features", nameof(PCAPFeatureExtractionRequestItem.PayloadContent))
                .Append(mlContext.AnomalyDetection.Trainers.RandomizedPca(featureColumnName: "Features"));

            ITransformer trainedModel = pipeline.Fit(split.TrainSet);

            var predictions = trainedModel.Transform(split.TestSet);

            mlContext.Model.Save(trainedModel, predictions.Schema, MODEL_NAME);

            return new ModelTrainingMetricsResponse(mlContext.AnomalyDetection.Evaluate(
                data: predictions,
                labelColumnName: nameof(ModelPredictionRequest.Label),
                scoreColumnName: "Score",
                predictedLabelColumnName: nameof(ModelPredictionRequest.Label)));
        }
    }
}
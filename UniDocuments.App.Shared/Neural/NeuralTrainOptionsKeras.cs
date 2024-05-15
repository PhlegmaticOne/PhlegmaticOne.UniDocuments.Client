namespace UniDocuments.App.Shared.Neural;

public class NeuralTrainOptionsKeras : NeuralTrainOptionsBase
{
    public int WindowSize { get; set; }
    public int BatchSize { get; set; }
}
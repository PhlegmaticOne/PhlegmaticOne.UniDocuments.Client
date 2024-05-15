namespace UniDocuments.App.Shared.Neural;

public class NeuralTrainResultKeras : NeuralModelTrainResult
{
    public int WindowSize { get; set; }
    public int BatchSize { get; set; }
}
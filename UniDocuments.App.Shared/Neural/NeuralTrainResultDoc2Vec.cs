namespace UniDocuments.App.Shared.Neural;

public class NeuralTrainResultDoc2Vec : NeuralModelTrainResult
{
    public float MinAlpha { get; set; }
    public int Dm { get; set; }
    public int WorkersCount { get; set; }
    public int MinWordsCount { get; set; }
}
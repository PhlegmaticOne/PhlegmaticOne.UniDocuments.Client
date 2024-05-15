namespace UniDocuments.App.Shared.Neural;

public class NeuralTrainOptionsBase
{
    public int EmbeddingSize { get; set; }
    public int Epochs { get; set; }
    public float LearningRate { get; set; }
}
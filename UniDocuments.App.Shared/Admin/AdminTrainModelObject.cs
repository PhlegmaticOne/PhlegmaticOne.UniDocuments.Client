namespace UniDocuments.App.Shared.Admin;

public class AdminTrainModelObject
{
    public string ModelName { get; set; } = null!;
    public bool IsRebuildVocab { get; set; } = true;
}
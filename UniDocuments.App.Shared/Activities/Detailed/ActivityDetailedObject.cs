﻿using System.Text.Json.Serialization;

namespace UniDocuments.App.Shared.Activities.Detailed;

public class ActivityDetailedObject
{
    public Guid Id { get; set; }
    public string CreatorFirstName { get; set; } = null!;
    public string CreatorLastName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<ActivityDetailedStudentObject> Students { get; set; } = null!;
    public List<ActivityDetailedDocumentObject> Documents { get; set; } = null!;

    [JsonIgnore] public bool IsExpired => DateTime.UtcNow > EndDate;
}
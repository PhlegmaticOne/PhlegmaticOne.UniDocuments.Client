﻿using System.Globalization;

namespace UniDocuments.App.Shared.Activities.Shared;

public class ActivityDocumentObject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateLoaded { get; set; }

    public string GetDateLoadedView()
    {
        return DateLoaded.ToLocalTime().ToString("f", CultureInfo.CurrentUICulture);
    }
}
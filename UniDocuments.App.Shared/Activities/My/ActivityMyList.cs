﻿using UniDocuments.Results;

namespace UniDocuments.App.Shared.Activities.My;

public class ActivityMyList
{
    public PagedList<ActivityMyObject> Activities { get; set; } = null!;
}
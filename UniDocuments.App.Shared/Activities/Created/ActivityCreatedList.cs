﻿using PhlegmaticOne.PagedLists;

namespace UniDocuments.App.Shared.Activities.Created;

public class ActivityCreatedList
{
    public PagedList<ActivityCreatedObject> Activities { get; set; } = null!;
}
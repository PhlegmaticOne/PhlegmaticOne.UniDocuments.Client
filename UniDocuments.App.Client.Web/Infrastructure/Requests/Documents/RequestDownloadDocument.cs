﻿using PhlegmaticOne.ApiRequesting.Models;
using PhlegmaticOne.ApiRequesting.Models.Requests;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;

public class RequestDownloadDocument : ClientGetFileRequest<Guid>
{
    public RequestDownloadDocument(Guid requestData) : base(requestData) { }

    public override string BuildQueryString()
    {
        return WithOneQueryParameter(new GetRequestQueryParameter("documentId", RequestData));
    }
}
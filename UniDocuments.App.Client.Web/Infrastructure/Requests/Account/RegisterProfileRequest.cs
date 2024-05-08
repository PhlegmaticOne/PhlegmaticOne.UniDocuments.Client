﻿using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Account;

public class RegisterProfileRequest : ClientPostRequest<RegisterObject, ProfileObject>
{
    public RegisterProfileRequest(RegisterObject requestData) : base(requestData)
    {
    }
}
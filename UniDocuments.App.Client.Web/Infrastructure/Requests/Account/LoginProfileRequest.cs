﻿using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Account;

public class LoginProfileRequest : ClientPostRequest<LoginObject, ProfileObject>
{
    public LoginProfileRequest(LoginObject requestData) : base(requestData)
    {
    }
}
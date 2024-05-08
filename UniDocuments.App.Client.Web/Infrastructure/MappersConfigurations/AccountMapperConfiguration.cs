using AutoMapper;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Infrastructure.MappersConfigurations;

public class AccountMapperConfiguration : Profile
{
    public AccountMapperConfiguration()
    {
        CreateMap<RegisterViewModel, RegisterObject>();
        CreateMap<LoginViewModel, LoginObject>();
        CreateMap<UpdateAccountViewModel, UpdateProfileObject>()
            .ForMember(x => x.FirstName, o => o.MapFrom(x => x.FirstName ?? ""))
            .ForMember(x => x.LastName, o => o.MapFrom(x => x.LastName ?? ""))
            .ForMember(x => x.UserName, o => o.MapFrom(x => x.UserName ?? ""))
            .ForMember(x => x.NewPassword, o => o.MapFrom(x => x.NewPassword ?? ""))
            .ForMember(x => x.OldPassword, o => o.MapFrom(x => x.OldPassword ?? ""));
    }
}
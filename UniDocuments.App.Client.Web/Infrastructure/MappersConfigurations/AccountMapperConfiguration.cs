using AutoMapper;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;
using UniDocuments.App.Shared.Users;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Infrastructure.MappersConfigurations;

public class AccountMapperConfiguration : Profile
{
    public AccountMapperConfiguration()
    {
        CreateMap<RegisterViewModel, RegisterObject>();

        CreateMap<LoginViewModel, LoginObject>();
        
        CreateMap<UpdateAccountViewModel, UpdateProfileObject>()
            .ForMember(x => x.FirstName, o => o.MapFrom(x => x.FirstName ?? string.Empty))
            .ForMember(x => x.LastName, o => o.MapFrom(x => x.LastName ?? string.Empty))
            .ForMember(x => x.NewPassword, o => o.MapFrom(x => x.NewPassword ?? string.Empty))
            .ForMember(x => x.OldPassword, o => o.MapFrom(x => x.OldPassword ?? string.Empty));
    }
}
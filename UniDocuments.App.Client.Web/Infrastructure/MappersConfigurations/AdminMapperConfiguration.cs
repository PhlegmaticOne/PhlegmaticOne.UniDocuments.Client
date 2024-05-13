using AutoMapper;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Admin;
using UniDocuments.App.Shared.Admin;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Infrastructure.MappersConfigurations;

public class AdminMapperConfiguration : Profile
{
    public AdminMapperConfiguration()
    {
        CreateMap<AdminMakeAdminViewModel, AdminCreateObject>()
            .ForMember(x => x.StudyRole, o => o.MapFrom(x => Enum.Parse<StudyRole>(x.Role)));

        CreateMap<AdminTrainModelViewModel, AdminTrainModelObject>();
    }
}
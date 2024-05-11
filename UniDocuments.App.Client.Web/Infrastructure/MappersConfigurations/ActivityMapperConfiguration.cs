using AutoMapper;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Activities;
using UniDocuments.App.Shared.Activities.Create;

namespace UniDocuments.App.Client.Web.Infrastructure.MappersConfigurations;

public class ActivityMapperConfiguration : Profile
{
    public ActivityMapperConfiguration()
    {
        CreateMap<ActivityCreateViewModel, ActivityCreateObject>()
            .ForMember(x => x.Students, o => o.MapFrom(x => x.Students.Select(s => s.UserName.ToLower()).ToList()))
            .ForMember(x => x.StartDate, o => o.MapFrom(x => x.StartDate.ToUniversalTime()))
            .ForMember(x => x.EndDate, o => o.MapFrom(x => x.EndDate.ToUniversalTime()));
    }
}
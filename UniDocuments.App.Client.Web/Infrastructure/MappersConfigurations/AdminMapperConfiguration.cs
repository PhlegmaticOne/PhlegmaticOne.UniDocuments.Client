using System.Globalization;
using AutoMapper;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Admin;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;
using UniDocuments.App.Shared.Admin;
using UniDocuments.App.Shared.Neural;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Infrastructure.MappersConfigurations;

public class AdminMapperConfiguration : Profile
{
    public AdminMapperConfiguration()
    {
        CreateMap<AdminMakeAdminViewModel, AdminCreateObject>()
            .ForMember(x => x.StudyRole, o => o.MapFrom(x => Enum.Parse<StudyRole>(x.Role)));

        CreateMap<NeuralTrainKerasViewModel, NeuralTrainOptionsKeras>()
            .ForMember(x => x.LearningRate, o => o.MapFrom(x => float.Parse(x.LearningRate, NumberStyles.Float)));

        CreateMap<NeuralTrainDoc2VecViewModel, NeuralTrainOptionsDoc2Vec>()
            .ForMember(x => x.LearningRate, o => o.MapFrom(x => float.Parse(x.LearningRate, NumberStyles.Any)))
            .ForMember(x => x.MinAlpha, o => o.MapFrom(x => float.Parse(x.MinAlpha, NumberStyles.Any)))
            .ForMember(x => x.Dm, o => o.MapFrom(x => int.Parse(x.Dm)));
    }
}
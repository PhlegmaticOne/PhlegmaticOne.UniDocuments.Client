using AutoMapper;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Document;
using UniDocuments.App.Shared.Documents;

namespace UniDocuments.App.Client.Web.Infrastructure.MappersConfigurations;

public class DocumentMapperConfiguration : Profile
{
    public DocumentMapperConfiguration()
    {
        CreateMap<DocumentCheckExistingViewModel, DocumentDetailedCheckObject>();
        CreateMap<DocumentCheckNewViewModel, DocumentDetailedCheckDocumentObject>();
    }
}
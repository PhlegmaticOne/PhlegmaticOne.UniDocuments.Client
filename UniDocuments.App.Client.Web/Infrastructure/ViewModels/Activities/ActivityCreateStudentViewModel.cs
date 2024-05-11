using System.ComponentModel.DataAnnotations;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Activities;

public class ActivityCreateStudentViewModel
{
    [Display(Name = "Ник студента:")]
    public string UserName { get; set; } = null!;
}
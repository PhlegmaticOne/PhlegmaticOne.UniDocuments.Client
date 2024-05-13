using System.ComponentModel.DataAnnotations;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Activities;

public class ActivityCreateViewModel
{
    [Required]
    [StringLength(50)]
    [Display(Name = "Название:")]
    public string Name { get; set; } = null!;
    
    [Required]
    [StringLength(150)]
    [Display(Name = "Описание:")]
    public string Description { get; set; } = null!;

    [Required]
    [Display(Name = "Начало активности:")]
    public DateTime StartDate { get; set; } = DateTime.Now;
    [Required]
    [Display(Name = "Окончание активности:")]
    public DateTime EndDate { get; set; } = DateTime.Now;
    [Display(Name = "Студенты:")]
    public List<ActivityCreateStudentViewModel> Students { get; set; }

    public ActivityCreateViewModel()
    {
        Students = new List<ActivityCreateStudentViewModel>();
    }
}
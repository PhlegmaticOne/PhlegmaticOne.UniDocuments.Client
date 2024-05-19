using System.ComponentModel.DataAnnotations;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Activities;

public class ActivityCreateViewModel
{
    [Display(Name = "Название:")]
    [Required(ErrorMessage = "Название активности не может быть пустым")]
    [MinLength(2, ErrorMessage = "Название активности не может меньше 2 символов")]
    [MaxLength(35, ErrorMessage = "Название активности не может больше 35 символов")]
    public string Name { get; set; } = null!;
    
    [Display(Name = "Описание:")]
    [Required(ErrorMessage = "Описание активности не может быть пустым")]
    [MinLength(2, ErrorMessage = "Описание активности не может меньше 2 символов")]
    [MaxLength(150, ErrorMessage = "Описание активности не может больше 150 символов")]
    public string Description { get; set; } = null!;

    [Display(Name = "Начало активности:")]
    [Required(ErrorMessage = "Начало активности не может быть пустым")]
    public DateTime StartDate { get; set; } = DateTime.Now;
    
    [Display(Name = "Окончание активности:")]
    [Required(ErrorMessage = "Окончание активности не может быть пустым")]
    public DateTime EndDate { get; set; } = DateTime.Now;
    
    [Display(Name = "Студенты:")]
    public List<ActivityCreateStudentViewModel> Students { get; set; }

    public ActivityCreateViewModel()
    {
        Students = new List<ActivityCreateStudentViewModel>();
    }
}
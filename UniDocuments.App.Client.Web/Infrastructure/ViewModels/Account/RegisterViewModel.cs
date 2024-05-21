using System.ComponentModel.DataAnnotations;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account.Base;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;

public class RegisterViewModel : ErrorHaving
{
    [Required(ErrorMessage = "Имя не может быть пустым")]
    [MinLength(2, ErrorMessage = "Имя должно быть не менее 2 симловов")]
    [MaxLength(35, ErrorMessage = "Имя должно быть не более 35 симловов")]
    public string FirstName { get; set; } = null!;
    
    [Required(ErrorMessage = "Фамилия не может быть пустой")]
    [MinLength(2, ErrorMessage = "Фамилия должна быть не менее 2 симловов")]
    [MaxLength(35, ErrorMessage = "Фамилия должна быть не более 35 симловов")]
    public string LastName { get; set; } = null!;
    
    [Required(ErrorMessage = "Ник не может быть пустым")]
    [MinLength(2, ErrorMessage = "Ник должен быть не менее 2 симловов")]
    [MaxLength(35, ErrorMessage = "Ник должен быть не более 35 симловов")]
    public string UserName { get; set; } = null!;
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Пароль не может быть пустым")] 
    public string Password { get; set; } = null!;
    
    [DataType(DataType.Password)] 
    [Required(ErrorMessage = "Пароль не может быть пустым")]
    public string ConfirmPassword { get; set; } = null!;
}
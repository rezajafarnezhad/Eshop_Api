using System.ComponentModel.DataAnnotations;
using Common.Application.Validation.CustomValidation.IFormFile;
using Shop.Domain.UserAgg.Enums;

namespace Shop.Api.ViewModels.Auth;

public class EditCurrentUserViewModel 
{

    [FileImage(ErrorMessage = "آواتار معتبر نیست")]
    public IFormFile? Avatar { get;  set; }
    [Required(ErrorMessage = "نام وارد شود")]
    public string Name { get;  set; }
    [Required(ErrorMessage = "فامیلی وارد شود")]
    public string Family { get;  set; }

    [Required(ErrorMessage = "شماره تلفن وارد شود")]
    [MaxLength(11, ErrorMessage = "شماره موبایل مامعتبر می باشد")]
    [MinLength(11, ErrorMessage = "شماره موبایل مامعتبر می باشد")]
    public string PhoneNumber { get;  set; }

    [Required(ErrorMessage = "ایمیل وارد شود")]
    [DataType(DataType.EmailAddress,ErrorMessage = "ایمیل به دزستی وارد شود")]
    public string Email { get;  set; }
    public Gender Gender { get; set; } = Gender.None;
}
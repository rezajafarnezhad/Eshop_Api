using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Auth;

public class LoginViewModel
{
    [Required(ErrorMessage = "شماره تلفن وارد شود")]
    [MaxLength(11,ErrorMessage = "شماره موبایل مامعتبر می باشد")]
    [MinLength(11,ErrorMessage = "شماره موبایل مامعتبر می باشد")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "کلمه عبور وارد شود")]
    public string Password { get; set; }
}
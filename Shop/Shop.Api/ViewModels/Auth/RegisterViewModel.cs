using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "شماره تلفن وارد شود")]
    [MaxLength(11, ErrorMessage = "شماره موبایل مامعتبر می باشد")]
    [MinLength(11, ErrorMessage = "شماره موبایل مامعتبر می باشد")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "کلمه عبور وارد شود")]
    [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 6 کاراکتر باشد")]
    public string Password { get; set; }

    [Required(ErrorMessage = "تکرار کلمه عبور وارد شود")]
    [MinLength(6, ErrorMessage = "تکرار کلمه عبور باید بیشتر از 6 کاراکتر باشد")]
    [Compare(nameof(Password),ErrorMessage = "تکرار کلمه عبور صحیح نیست")]
    public string ConfirmPassword { get; set; }

}
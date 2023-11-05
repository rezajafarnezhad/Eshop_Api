using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Auth;

public class ChangePasswordViewModel
{

    [Required(ErrorMessage = "کلمه عبور فعلی وارد شود")]
    [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 6 کاراکتر باشد")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "کلمه عبور وارد شود")]
    [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 6 کاراکتر باشد")]
    public string Password { get; set; }

    [Required(ErrorMessage = "تکرار کلمه عبور وارد شود")]
    [MinLength(6, ErrorMessage = "تکرار کلمه عبور باید بیشتر از 6 کاراکتر باشد")]
    [Compare(nameof(Password), ErrorMessage = "تکرار کلمه عبور صحیح نیست")]
    public string ConfirmPassword { get; set; }

}


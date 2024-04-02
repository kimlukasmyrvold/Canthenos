using System.ComponentModel.DataAnnotations;

namespace Canthenos.Web.Models;

public class NewUserModelUsername
{
    [Required(ErrorMessage = "Select one of the options.")] public string? SelectedOption { get; set; }

    [CustomUsernameValidation(ErrorMessage = "Username is required.")]
    [MinLength(1, ErrorMessage = "Username is too short.")]
    public string? Username { get; set; }
}

public class CustomUsernameValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var model = (NewUserModelUsername)validationContext.ObjectInstance;

        if (model.SelectedOption == "custom" && string.IsNullOrEmpty(model.Username))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
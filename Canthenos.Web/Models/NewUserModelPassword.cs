using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Canthenos.Web.Models;

public class NewUserModelPassword
{
    [Required(ErrorMessage = "Password is required.")]
    [PasswordPropertyText]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Confirm is required.")]
    [PasswordPropertyText]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    [ComparePasswords(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string? ConfirmPassword { get; set; }
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class ComparePasswordsAttribute : ValidationAttribute
{
    private readonly string _otherPropertyName;

    public ComparePasswordsAttribute(string otherPropertyName)
    {
        _otherPropertyName = otherPropertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

        if (otherPropertyInfo == null)
        {
            return new ValidationResult($"Property {_otherPropertyName} not found.");
        }

        var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

        return !Equals(value, otherValue)
            ? new ValidationResult(ErrorMessage ??
                                   $"{validationContext.DisplayName} and {_otherPropertyName} do not match.")
            : ValidationResult.Success;
    }
}
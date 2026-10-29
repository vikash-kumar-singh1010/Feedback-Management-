using System.ComponentModel.DataAnnotations;

namespace FeedbackManagement.Models;

// Custom validation attribute
public class NoBadWordsAttribute : ValidationAttribute
{
    private static readonly string[] Banned = { "stupid", "idiot", "trash" };

    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        var text = (value as string)?.ToLowerInvariant() ?? "";
        return Banned.Any(text.Contains)
            ? new ValidationResult("Please keep your comments respectful.")
            : ValidationResult.Success;
    }
}

public class Feedback
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be 3-50 characters")]
    [Display(Name = "Full Name")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Please choose a category")]
    public string Category { get; set; } = "";

    [Range(1, 5, ErrorMessage = "Please select a rating from 1 to 5")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "Please write your comments")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Comments must be 10-500 characters")]
    [NoBadWords]
    public string Comments { get; set; } = "";

    [Display(Name = "Contact me back")]
    public bool ContactBack { get; set; }

    [Display(Name = "Submitted On")]
    public DateTime SubmittedOn { get; set; }
}

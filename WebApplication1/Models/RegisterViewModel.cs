using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RegisterViewModel
    {
    [Required(ErrorMessage = "School ID Number is required")]
    [Display(Name = "School ID Number")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "School ID Number must be exactly 8 digits")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "School ID Number must be exactly 8 digits")]
    public string? SchoolIdNumber { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [Display(Name = "First Name")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First name must contain only letters")]
    [MaxLength(25)]
    public string? Firstname { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [Display(Name = "Last Name")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last name must contain only letters")]
    [MaxLength(25)]
    public string? Lastname { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [MaxLength(50)]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be a valid Gmail address (example@gmail.com)")]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Course is required")]
    [Display(Name = "Course")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Course must contain only letters")]
    [MaxLength(50)]
    public string? Course { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    [Display(Name = "Confirm Password")]
    public string? ConfirmPassword { get; set; }
    }
}

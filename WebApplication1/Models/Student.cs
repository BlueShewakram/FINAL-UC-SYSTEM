using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    [Required(ErrorMessage = "Last name is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last name must contain only letters")]
    [MaxLength(25)]
    public string Lastname { get; set; } = "";

    [Required(ErrorMessage = "First name is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First name must contain only letters")]
    [MaxLength(25)]
    public string Firstname { get; set; } = "";

    [Display(Name = "Date Created")]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [Display(Name = "School ID Number")]
    [Required(ErrorMessage = "School ID Number is required")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "School ID Number must be exactly 8 digits")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "School ID Number must be exactly 8 digits")]
    public string? SchoolIdNumber { get; set; }

    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
    public int? Age { get; set; }

    [Required(ErrorMessage = "Course is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Course must contain only letters")]
    [MaxLength(50)]
    public string? Course { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be a valid Gmail address (example@gmail.com)")]
    [MaxLength(100)]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; } = "";

    // Stored hashed password
    [MaxLength(512)]
    public string? PasswordHash { get; set; }
       
    }
   
}

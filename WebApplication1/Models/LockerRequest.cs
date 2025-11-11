using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class LockerRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name must contain only letters and spaces")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Full Name must be between 2 and 100 characters")]
        [Display(Name = "Full Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Student ID Number is required")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Student ID must be exactly 8 digits")]
        [Display(Name = "Student ID Number")]
        public string? IdNumber { get; set; }

        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "Locker Number must contain only uppercase letters, numbers, and hyphens")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Locker Number must be between 1 and 20 characters")]
        [Display(Name = "Locker Number")]
        public string? LockerNumber { get; set; }

        [Required(ErrorMessage = "Semester is required")]
        [RegularExpression(@"^(1st|2nd)\s(Semester|sem)(\s\d{4}-\d{4})?$", ErrorMessage = "Semester must be in format: '1st Semester' or '2nd Semester'")]
        [StringLength(50, ErrorMessage = "Semester must not exceed 50 characters")]
        public string? Semester { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [RegularExpression(@"^(09|\+639)\d{9}$", ErrorMessage = "Contact Number must be a valid Philippine mobile number (e.g., 09171234567 or +639171234567)")]
        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        [Display(Name = "Registration File Path")]
        public string? RegistrationFilePath { get; set; }

        [Display(Name = "I accept the Terms of Service and Locker Usage Policy")]
        [Required]
        public bool TermsAccepted { get; set; }

        // Status tracking
        public bool? Approved { get; set; }
        public string? Approver { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModified { get; set; }
    }
}


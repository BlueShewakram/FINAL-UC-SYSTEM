using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class GatePassModel
    {
        [Key]
        public int Id { get; set; }

        // --- Student Information ---
        [Required(ErrorMessage = "Student ID is required")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Student ID must be exactly 8 digits")]
        [Display(Name = "Student ID No.")]
        public required string StudentIdNo { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Student Name must contain only letters and spaces")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Student Name must be between 2 and 100 characters")]
        [Display(Name = "Student Name")]
        public required string StudentName { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Course must contain only letters and spaces")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Course must be between 2 and 100 characters")]
        [Display(Name = "Course")]
        public required string StudentCourse { get; set; }

        [Required(ErrorMessage = "Year Level is required")]
        [RegularExpression(@"^[1-5]$", ErrorMessage = "Year Level must be between 1 and 5")]
        [Display(Name = "Year Level")]
        public required string StudentYear { get; set; }

        // --- Vehicle Information ---
        [Required(ErrorMessage = "Vehicle Plate Number is required")]
        [RegularExpression(@"^[A-Z0-9\s-]+$", ErrorMessage = "Plate Number must contain only uppercase letters, numbers, spaces, and hyphens")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Plate Number must be between 2 and 20 characters")]
        [Display(Name = "Vehicle Plate Number")]
        public required string VehiclePlateNo { get; set; }

        [Required(ErrorMessage = "Vehicle Model is required")]
        [RegularExpression(@"^[a-zA-Z0-9\s-]+$", ErrorMessage = "Vehicle Model must contain only letters, numbers, spaces, and hyphens")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Vehicle Model must be between 2 and 50 characters")]
        [Display(Name = "Vehicle Model")]
        public required string VehicleModel { get; set; }

        [Required(ErrorMessage = "Vehicle Make is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Vehicle Make must contain only letters and spaces")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Vehicle Make must be between 2 and 50 characters")]
        [Display(Name = "Vehicle Make")]
        public required string VehicleMake { get; set; }

        [Required(ErrorMessage = "Vehicle Type is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Vehicle Type must contain only letters and spaces")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Vehicle Type must be between 2 and 30 characters")]
        [Display(Name = "Vehicle Type")]
        public required string VehicleType { get; set; }

        // --- Uploaded Documents ---
        [Display(Name = "Official Receipt (OR) Document")]
        public string? OrDocumentPath { get; set; }

        [Display(Name = "Certificate of Registration (CR) Document")]
        public string? CrDocumentPath { get; set; }

        // --- Other Details ---
        [Display(Name = "Date Submitted")]
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending";
    }
}

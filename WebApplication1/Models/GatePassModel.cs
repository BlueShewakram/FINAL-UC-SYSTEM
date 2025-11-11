using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class GatePassModel
    {
        [Key]
        public int Id { get; set; }

        // --- Student Information ---
        [Required]
        [Display(Name = "Student ID No.")]
        public string StudentIdNo { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Required]
        [Display(Name = "Course")]
        public string StudentCourse { get; set; }

        [Required]
        [Display(Name = "Year Level")]
        public string StudentYear { get; set; }

        // --- Vehicle Information ---
        [Required]
        [Display(Name = "Vehicle Plate Number")]
        public string VehiclePlateNo { get; set; }

        [Required]
        [Display(Name = "Vehicle Model")]
        public string VehicleModel { get; set; }

        [Required]
        [Display(Name = "Vehicle Make")]
        public string VehicleMake { get; set; }

        [Required]
        [Display(Name = "Vehicle Type")]
        public string VehicleType { get; set; }

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

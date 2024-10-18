using System.ComponentModel.DataAnnotations;

public class ClaimViewModel
{
    [Required]
    [Key]
    public int ClaimID { get; set; }

    [Required(ErrorMessage = "Lecturer name is required.")]
    public string LecturerName { get; set; }

    [Required(ErrorMessage = "Hours worked is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Hours worked must be a positive number.")]
    public int HoursWorked { get; set; }

    [Required(ErrorMessage = "Hourly rate is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Hourly rate must be a positive number.")]
    public decimal HourlyRate { get; set; }

    public string Notes { get; set; }

    public string DocumentFile { get; set; } // Store uploaded document file name

    public IFormFile UploadFile { get; set; } // For file uploads

}

using System.ComponentModel.DataAnnotations;

namespace Contract_Monthly_ClaimSystem__CMCS_.Models
{
    public class Claim
    {
        public int ClaimID { get; set; } // Primary key

        [Required]
        public string LecturerName { get; set; } // Add this property if it doesn't exist

        [Required]
        public int HoursWorked { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        public string Notes { get; set; }

        public string DocumentFileName { get; set; }

        public string ClaimStatus { get; set; } // e.g., "Pending", "Approved", "Rejected"
    }
}

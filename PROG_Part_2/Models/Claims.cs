using System.ComponentModel.DataAnnotations;

namespace PROG_Part_2.Models
{
    public class Claims
    {
        [Key]
        public int ClaimId { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string? AdditionalNotes { get; set; }
        public string? DocumentName { get; set; }
        public long Size { get; set; }

        public string DisplaySize
        {
            get
            {
                if (Size >= 1024 * 1024)
                    return $"{Size / 1024 / 1024} MB";
                if (Size >= 1024)
                    return $"{Size / 1024} KB";
                return $"{Size} Bytes";
            }
        }
    }
}
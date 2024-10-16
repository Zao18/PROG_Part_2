using System.ComponentModel.DataAnnotations;

namespace PROG_Part_2.Models
{
    public class Claims
    {
        [Key]
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string AdditionalNotes { get; set; }
    }
}

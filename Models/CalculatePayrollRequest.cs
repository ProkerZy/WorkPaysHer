using System.ComponentModel.DataAnnotations;

namespace WorkPaysHer.Models
{
    public class CalculatePayrollRequest
    {
        [Required]
        public string PositionName { get; set; }
        [Required]
        public List<Shift> Shifts { get; set; } = new();
    }
}

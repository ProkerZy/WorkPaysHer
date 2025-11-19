using System.ComponentModel.DataAnnotations;

namespace WorkPaysHer.Models
{
    public class Position
    {
        [Required]
        public string PositionName { get; set; }
        
        public decimal? RatePerHour { get; set; }
        public decimal? Salary { get; set; }
    }
}

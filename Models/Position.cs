using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WorkPaysHer.Models
{
    public class Position
    {
        public int Id { get; set; }
        
        public string PositionName { get; set; } = "";
        
        public decimal? RatePerHour { get; set; }
        public decimal? Salary { get; set; }
        [JsonIgnore]
        public List<CalcShift> Shifts { get; set; } = new(); // навигационное свойство
    }
}

using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace WorkPaysHer.Models
{
    public class Shift
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required]
        public List<string> Colleagues { get; set; } = new();
        public void Validate()
        {
            foreach (var colleague in Colleagues)
            {
                if (string.IsNullOrWhiteSpace(colleague))
                    throw new ArgumentException("Имя коллеги не может быть пустым");
            }

            if (Colleagues.Count > 7)
                throw new ArgumentException("На смене не может быть больше 7 коллег");
        }
    }
}

namespace WorkPaysHer.Models
{
    public class PayrollCalculator : IPayrollService
    {
        private static readonly HashSet<(int Month, int Day)> _holidays = new()
        {
            (1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6),
            (2,23),
            (3, 8),
            (5, 1), (5,9),
            (6, 12),
            (11, 4),
        };
        
        private readonly List<Position> _positions = new()
        {
            new Position {PositionName = "Менеджер", RatePerHour = 280m},
            new Position {PositionName = "Директор", Salary = 80.000m},
            new Position {PositionName = "Лидер департамента", Salary = 65.000m},
            new Position {PositionName = "Ассистент директора", Salary = 75.000m},
            new Position {PositionName = "Работник", RatePerHour = 189},
            new Position {PositionName = "Инструктор", RatePerHour = 204m},
            new Position {PositionName = "Ночник", RatePerHour = 209m},
            new Position {PositionName = "Инструктор ночной смены", RatePerHour = 250m},
            new Position {PositionName = "Техник", Salary = 60.800m}
        };
        public PayrollCalculationResult CalculateSalary(string positionName, List<Shift> shifts)
        {
            List<Shift> _shifts = shifts;
            string gifUrl = positionName switch
            {
                "Ночник" => "/gifs/Savely.jpg",
                "Работник" => "/gifs/Isupova.jpg",
                "Менеджер" => "/gifs/NastyaBidon.jpg",
                "Директор" => "/gifs/Usatova.jpg",
                "Инструктор ночной смены" => "/gifs/Irina.jpg",
                "Техник" => "/gifs/Daniil.jpg",
                "Ассистент директора" => "/gifs/Ulyana.jpg",
                _ => "/gifs/default.jpg"
            };

            decimal salary = 0;
            var position = _positions.FirstOrDefault(p => p.PositionName == positionName);
            if (position == null)
            {
                throw new ArgumentException("Должность не найдена");
            }
            if (position.Salary.HasValue)
            {
                return new PayrollCalculationResult
                {
                    TotalSalary = position.Salary.Value
                };

            }
            if (position.RatePerHour.HasValue)
            {
                foreach (Shift shift in shifts)
                {
                    shift.Validate();
                    decimal hours = Math.Round((decimal)(shift.EndTime - shift.StartTime).TotalHours, 2);
                    var date = shift.StartTime.Date;
                    if (hours < 0)
                    {
                        throw new ArgumentException("Время начала позже времени окончания");
                    }
                    if (_holidays.Contains((date.Month, date.Day)))
                    {
                        salary += hours * (position.RatePerHour.Value) * 2;
                    }
                    else
                    {
                        salary += hours * position.RatePerHour.Value;
                    }
                }

                return new PayrollCalculationResult
                {
                    TotalSalary = salary,
                    GifUrl = gifUrl,
                    
                };
            }
            return new PayrollCalculationResult
            {
                TotalSalary = 0,
                GifUrl = "/gifs/default.jpg",
              
            };

        }
    }
}
// закончил смену раньше....

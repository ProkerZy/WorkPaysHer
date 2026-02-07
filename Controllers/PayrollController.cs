using Microsoft.AspNetCore.Mvc;
using WorkPaysHer.Models;

namespace WorkPaysHer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private static readonly List<string> _friends = new()
        {
            "Савелий", "Рома", "Андрей", "Даниил", "Настя", "Карина"
        };
        private readonly IPayrollService _service;
        public PayrollController(IPayrollService service)
        {
            _service = service;
        }
        [HttpPost("calculate")]
        public ActionResult<CalculatePayrollResponse> Calculate(CalculatePayrollRequest request)
        {
            PayrollCalculationResult salary;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                salary = _service.CalculateSalary(request.PositionName, request.Shifts);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            if (!request.Shifts.Any())
                return BadRequest("Список смен не может быть пустым");

            decimal totalHours = 0;
            int numberOfShits = 0;

            foreach (var shift in request.Shifts)
            {
                decimal hours = (decimal)(shift.EndTime - shift.StartTime).TotalHours;
                totalHours += hours;

                numberOfShits++;
            }
            var shiftMoodsDetail = new List<ShiftMoodDetail>();
            decimal totalMood = 0;

            for (int i = 0; i < request.Shifts.Count; i++)
            {
                var shift = request.Shifts[i];
                decimal mood = 50m;

                foreach (var colleague in shift.Colleagues)
                {
                    if (_friends.Contains(colleague))
                        mood += 10;
                    else
                        mood -= 5;
                }

                mood = Math.Max(0, Math.Min(100, mood));
                totalMood += mood;

                string moodMessage = mood switch
                {
                    >= 100 => "Это точно смена, а не праздник?! Все спортсмены на месте!",
                    >= 90 => "Лучше смены пожелать невозможно!",
                    >= 80 => "Идеальная синергия: друзья в сборе!",
                    >= 70 => "Смена точно пройдет на ура!",
                    >= 60 => "Хорошая смена в этот день обеспечена благодаря ...",
                    >= 50 => "Смена как смена. Че бубнеть? Работаем.",
                    >= 40 => "Похоже что сегодня будет спокойная смена в тишине... Восстанавливаемся!",
                    _ => "Сегодня сосредоточимся на работе!"
                };

                shiftMoodsDetail.Add(new ShiftMoodDetail
                {
                    ShiftIndex = i + 1,
                    StartTime = shift.StartTime,
                    EndTime = shift.EndTime,
                    MoodPercentage = mood,
                    MoodMessage = moodMessage,
                    Colleagues = shift.Colleagues
                });
            }

            decimal averageMood = request.Shifts.Any() ? totalMood / request.Shifts.Count : 50m;
            string averageMoodMessage = averageMood switch
            {
                >= 100 => "С такими коллегами и из мака уходить не хочется! Все смены — праздник!",
                >= 90 => "Отличный месяц! Почти все смены с кентозаврами!",
                >= 80 => "Хороший баланс: друзья есть, работа поперла!",
                >= 70 => "Стабильный месяц на вайбике!",
                >= 60 => "Месяц нормас под пивас, но можно лучше!",
                >= 50 => "Как обычно - все клубнично",
                >= 40 => "Месяц выдался спокойным, но одиноким...",
                _ => "Тяжёлый месяц... Нужно восстановление!"
            }; ;



            var response = new CalculatePayrollResponse
            {
                TotalSalary = salary.TotalSalary,
                TotalHours = totalHours,
                Message = $"Рассчитано для должности: {request.PositionName}",
                GifUrl = salary.GifUrl,
                AverageMoodPercentage = averageMood,
                AverageMoodMessage = averageMoodMessage,
                ShiftMoods = shiftMoodsDetail
            };

            return Ok(response);
        }
    }

}

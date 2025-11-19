using Microsoft.AspNetCore.Mvc;
using WorkPaysHer.Models;

namespace WorkPaysHer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase 
    {
        private readonly IPayrollService _service;
        public PayrollController(IPayrollService service)
        { 
            _service = service;
        }
        [HttpPost("calculate")]
        public ActionResult<CalculatePayrollResponse> Calculate (CalculatePayrollRequest request)
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
            foreach (var shift in request.Shifts)
            {
                decimal hours = (decimal)(shift.EndTime - shift.StartTime).TotalHours;
                totalHours += hours;
            }
            
            var response = new CalculatePayrollResponse
            {
                TotalSalary = salary.TotalSalary,
                TotalHours = totalHours,
                Message = $"Рассчитано для должности: {request.PositionName}",
                GifUrl = salary.GifUrl
            };

            return Ok(response);
        } 
    }

}

using WorkPaysHer.Models;

namespace WorkPaysHer.Services
{
    public interface IPayrollService
    {
        PayrollCalculationResult CalculateSalary(string positionName, List<Shift> shifts);
    }
}

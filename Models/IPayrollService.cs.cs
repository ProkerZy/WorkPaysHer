namespace WorkPaysHer.Models
{
    public interface IPayrollService
    {
        PayrollCalculationResult CalculateSalary(string positionName, List<Shift> shifts);
    }
}

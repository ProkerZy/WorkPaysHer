namespace WorkPaysHer.Models
{
    public class CalculatePayrollResponse
    {
        public decimal TotalSalary { get; set; }
        public decimal TotalHours { get; set; }
        public string Message { get; set; }
        public string GifUrl { get; set; }
    }
}

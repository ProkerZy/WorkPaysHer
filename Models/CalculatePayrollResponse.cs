namespace WorkPaysHer.Models
{
    public class CalculatePayrollResponse
    {
        public decimal TotalSalary { get; set; }
        public decimal TotalHours { get; set; }
        public string Message { get; set; }
        public string GifUrl { get; set; }
        public decimal AverageMoodPercentage { get; set; }
        public string AverageMoodMessage { get; set; }

        
        public List<ShiftMoodDetail> ShiftMoods { get; set; } = new();
    }

    public class ShiftMoodDetail
    {
        public int ShiftIndex { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal MoodPercentage { get; set; }
        public string MoodMessage { get; set; }
        public List<string> Colleagues { get; set; } = new();
    }
}

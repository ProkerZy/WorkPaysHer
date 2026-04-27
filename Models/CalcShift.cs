namespace WorkPaysHer.Models
{
    public class CalcShift
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public Position? Position { get; set; } // Навигационное свойство
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Hours { get; set; }
        public decimal TotalPay { get; set; }
        public decimal MoodPercentage { get; set; }
        public string Colleagues { get; set; } = ""; // тут пока что через строчки и метод Contain буду искать людей на смене

    }
}

using WorkPaysHer.Models;

namespace WorkPaysHer.Services
{
    public interface IShiftHistoryService
    {
        // Сохраняет смену, возвращает её Id после сохранения
        Task<int> SaveShiftAsync(CalcShift shift);

        // Возвращает список смен, отсортированный по дате (новые сверху)
        Task<List<CalcShift>> GetHistoryAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using WorkPaysHer.Models;

namespace WorkPaysHer.Services
{
    public class ShiftHistoryService : IShiftHistoryService
    {
        private readonly AppDbContext _context;

        public ShiftHistoryService (AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveShiftAsync(CalcShift shift)
        {
            await _context.CalcShifts.AddAsync(shift);
            await _context.SaveChangesAsync();
            return shift.Id;
        }
        public async Task<List<CalcShift>> GetHistoryAsync()
        {
            return await _context.CalcShifts
                .OrderByDescending(s=> s.StartTime)
                .ToListAsync();
        }
    }
}

using E_project2025.Data;
using Microsoft.EntityFrameworkCore; // <<-- Add this

namespace E_project2025
{
    public class UserAnalyticsService
    {
        private readonly E_project2025Context _context;

        public UserAnalyticsService(E_project2025Context context)
        {
            _context = context;
        }

        public async Task<int> TotalUsers()
            => await _context.Users.CountAsync();

        public async Task<int> NewUsersToday()
            => await _context.Users
                .CountAsync(u => u.CreatedAt.Date == DateTime.UtcNow.Date);

        public async Task<int> ActiveUsersLast7Days()
            => await _context.Users
                .CountAsync(u => u.LastLoginAt >= DateTime.UtcNow.AddDays(-7));

        public async Task<int> LockedUsers()
            => await _context.Users
                .CountAsync(u => u.LockoutEnd != null && u.LockoutEnd > DateTime.UtcNow);
    }
}

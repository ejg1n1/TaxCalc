using Athena.Infrastructure.Repositories;
using Domain.Entities.RequestModels;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace infra.data.Repositories.BookingRepositories
{
    public class BookingScheduleRepository : Repository<BookingSchedule>, IBookingScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingScheduleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BookingSchedule?>> QueryAllByAgentId(Guid agentId)
        {
            return await _context.BookingSchedules.Where(e => e.Booking.Agent.Id == agentId).ToListAsync();
        }

        public async Task<List<BookingSchedule?>> QueryAllByUser(Guid userId)
        {
            return await _context.BookingSchedules.Where(e => e.Booking.User.Id == userId).ToListAsync();

        }
    }
}

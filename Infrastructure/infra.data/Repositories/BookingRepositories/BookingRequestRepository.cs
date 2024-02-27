using Athena.Infrastructure.Repositories;
using Domain.Entities.RequestModels;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infra.data.Repositories.BookingRepositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Booking?>> QueryAllByAgentIdNoTracking(Guid agentId)
        {
            return await _context.Bookings.Where(e => e.Agent.Id == agentId).AsNoTracking().ToListAsync();
        }

        public async Task<List<Booking?>> QueryAllByStatus(Guid statusId)
        {
            return await _context.Bookings.Where(e => e.RequestStatus.Id == statusId).AsNoTracking().ToListAsync();
        }

        public async Task<List<Booking?>> QueryAllByType(Guid typeId)
        {
            return await _context.Bookings.Where(e => e.RequestType.Id == typeId).AsNoTracking().ToListAsync();
        }

        public async Task<List<Booking?>> QueryAllByUserIdNoTracking(Guid userId)
        {
            return await _context.Bookings.Where(e => e.User.Id == userId).AsNoTracking().ToListAsync();
        }

    }
}

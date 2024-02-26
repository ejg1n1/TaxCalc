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
    public class BookingRequestRepository : Repository<BookingRequest>, IBookingRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRequestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BookingRequest?>> QueryAllByAgentIdNoTracking(Guid agentId)
        {
            return await _context.BookingRequests.Where(e => e.Agent.Id == agentId).AsNoTracking().ToListAsync();
        }

        public async Task<List<BookingRequest?>> QueryAllByStatus(Guid statusId)
        {
            return await _context.BookingRequests.Where(e => e.RequestStatus.Id == statusId).AsNoTracking().ToListAsync();
        }

        public async Task<List<BookingRequest?>> QueryAllByType(Guid typeId)
        {
            return await _context.BookingRequests.Where(e => e.RequestType.Id == typeId).AsNoTracking().ToListAsync();
        }

        public async Task<List<BookingRequest?>> QueryAllByUserIdNoTracking(Guid userId)
        {
            return await _context.BookingRequests.Where(e => e.User.Id == userId).AsNoTracking().ToListAsync();
        }

    }
}

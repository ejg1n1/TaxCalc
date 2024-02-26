using Athena.Infrastructure.Repositories;
using Domain.Entities.RequestModels;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infra.data.Repositories.BookingRepositories
{
    public class BookingStatusRepository : Repository<BookingStatus>, IBookingStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingStatusRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BookingStatus>> QueryAllWithNoTracking()
        {
            return await _context.BookingStatuses.AsNoTracking().ToListAsync();
        }

        public async Task<BookingStatus?> QueryByName(string name)
        {
            return await _context.BookingStatuses.FirstOrDefaultAsync(d => d.Description == name);
        }
    }
}

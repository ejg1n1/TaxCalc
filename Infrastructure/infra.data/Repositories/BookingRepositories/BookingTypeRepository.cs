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
    public class BookingTypeRepository : Repository<BookingType>, IBookingTypesRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BookingType>> QueryAllWithNoTracking()
        {
            return await _context.BookingTypes.AsNoTracking().ToListAsync();
        }

        public async Task<BookingType?> QueryByName(string name)
        {
            return await _context.BookingTypes.FirstOrDefaultAsync(d => d.Description == name);
        }
    }
}

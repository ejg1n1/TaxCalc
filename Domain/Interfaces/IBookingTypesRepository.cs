using Athena.Core.Interfaces;
using Domain.Entities.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookingTypesRepository : IRepository<BookingType>
    {
        Task<BookingType?> QueryByName(string name);
        Task<List<BookingType>> QueryAllWithNoTracking();
    }
}

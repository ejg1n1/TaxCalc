using Athena.Core.Interfaces;
using Core.Entities;
using Domain.Entities.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookingStatusRepository : IRepository<BookingStatus>
    {
        Task<BookingStatus?> QueryByName(string name);
        Task<List<BookingStatus>> QueryAllWithNoTracking();
    }
}

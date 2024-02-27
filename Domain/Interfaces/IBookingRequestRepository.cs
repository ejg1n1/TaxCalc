using Athena.Core.Interfaces;
using Domain.Entities.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<List<Booking?>> QueryAllByUserIdNoTracking(Guid userId);
        Task<List<Booking?>> QueryAllByAgentIdNoTracking(Guid agentId);

        Task<List<Booking?>> QueryAllByStatus(Guid statusId);
        Task<List<Booking?>> QueryAllByType(Guid typeId);
    }
}

using Athena.Core.Interfaces;
using Domain.Entities.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookingRequestRepository : IRepository<BookingRequest>
    {
        Task<List<BookingRequest?>> QueryAllByUserIdNoTracking(Guid userId);
        Task<List<BookingRequest?>> QueryAllByAgentIdNoTracking(Guid agentId);

        Task<List<BookingRequest?>> QueryAllByStatus(Guid statusId);
        Task<List<BookingRequest?>> QueryAllByType(Guid typeId);
    }
}

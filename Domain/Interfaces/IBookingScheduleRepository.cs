using Athena.Core.Interfaces;
using Domain.Entities.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookingScheduleRepository : IRepository<BookingSchedule>
    {
        Task<List<BookingSchedule?>> QueryAllByUser(Guid userId);

        Task<List<BookingSchedule?>> QueryAllByAgentId(Guid agentId);
    }
}

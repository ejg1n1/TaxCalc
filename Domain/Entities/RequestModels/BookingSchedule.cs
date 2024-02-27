using Athena.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RequestModels
{
    public class BookingSchedule : BaseEntity
    {
        public string EventDetails { get; set; } = String.Empty;
        public DateTime DateOfEvent { get; set; }

        public Guid BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}

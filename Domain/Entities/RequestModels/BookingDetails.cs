using Athena.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RequestModels
{
    public class BookingDetails : BaseEntity
    {
        public DateTime DateOfRequest { get; set; }
        public string RequestDetail { get; set; } = String.Empty;

        public Guid BookingRequestId { get; set; }
        public virtual BookingRequest BookingRequest { get; set; }
    }
}

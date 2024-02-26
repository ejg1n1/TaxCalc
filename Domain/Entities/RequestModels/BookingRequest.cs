using Athena.Core.Entities;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RequestModels
{
    public class BookingRequest : BaseEntity
    {
        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationUser Agent { get; set; }

        public virtual ICollection<BookingDetails> RequestDetails { get; set; }
        public virtual ICollection<BookingSchedule> BookingSchedule { get; set; }

        public virtual BookingStatus RequestStatus { get; set; }

        public virtual BookingType RequestType { get; set; }
    }
}

using Athena.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RequestModels
{
    public class BookingStatus : BaseEntity
    {
        [MaxLength(50)]
        public string Description { get; set; } = String.Empty;

        public virtual ICollection<Booking> Booking { get; set; }
    }
}

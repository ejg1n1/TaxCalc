using Athena.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RequestModels
{
    public class History : BaseEntity
    {
        public Guid UserRequestId { get; set; } 
        public Guid UserId { get; set; }
        public DateTime DateOfRequest { get; set; } = DateTime.Now;
    }
}

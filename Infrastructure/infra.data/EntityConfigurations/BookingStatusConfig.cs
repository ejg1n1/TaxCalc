using Domain.Entities.RequestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infra.data.EntityConfigurations
{
    public class BookingStatusConfig : IEntityTypeConfiguration<BookingStatus>
    {
        public void Configure(EntityTypeBuilder<BookingStatus> builder)
        {
            //Primary Key
            builder.HasKey(u => u.Id);

            //Indexes

            //Maps to Table
            builder.ToTable("BookingStatus");

            //Table limitations / sizes

            //Entity relationships
            builder.HasMany(s => s.Booking)
                .WithOne(p => p.RequestStatus)
                .HasForeignKey(s => s.RequestStatus.Id);

        }
    }
}

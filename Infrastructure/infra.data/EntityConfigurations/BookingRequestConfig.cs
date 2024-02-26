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
    public class BookingRequestConfig : IEntityTypeConfiguration<BookingRequest>
    {
        public void Configure(EntityTypeBuilder<BookingRequest> builder)
        {
            //Primary Key
            builder.HasKey(t => t.Id);

            //Indexes
            builder.HasIndex(t => t.User.Id).HasDatabaseName("BookingUserId");
            builder.HasIndex(t => t.Agent.Id).HasDatabaseName("BookingUserId");

            //Map table name
            builder.ToTable("BookingRequest");

            //Limit table sizes
            builder.HasMany(p => p.RequestDetails)
                .WithOne(c => c.BookingRequest)
                .HasForeignKey(c => c.BookingRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.BookingSchedule)
                .WithOne(c => c.BookingRequest)
                .HasForeignKey(c => c.BookingRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.RequestStatus)
                .WithMany(s => s.BookingRequest)
                .HasForeignKey(p => p.RequestStatus.Id);

            builder.HasOne(p => p.RequestStatus)
                .WithMany(s => s.BookingRequest)
                .HasForeignKey(p => p.RequestStatus.Id);

            builder.HasOne(p => p.RequestType)
                .WithMany(s => s.BookingRequest)
                .HasForeignKey(p => p.RequestType.Id);
        }
    }
}

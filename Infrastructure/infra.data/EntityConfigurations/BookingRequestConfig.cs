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
    public class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            //Primary Key
            builder.HasKey(t => t.Id);

            //Indexes
            builder.HasIndex(t => t.User.Id).HasDatabaseName("BookingUserId");
            builder.HasIndex(t => t.Agent.Id).HasDatabaseName("BookingUserId");

            //Map table name
            builder.ToTable("Booking");

            //Limit table sizes
            builder.HasMany(p => p.RequestDetails)
                .WithOne(c => c.Booking)
                .HasForeignKey(c => c.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.BookingSchedule)
                .WithOne(c => c.Booking)
                .HasForeignKey(c => c.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.RequestStatus)
                .WithMany(s => s.Booking)
                .HasForeignKey(p => p.RequestStatus.Id);

            builder.HasOne(p => p.RequestStatus)
                .WithMany(s => s.Booking)
                .HasForeignKey(p => p.RequestStatus.Id);

            builder.HasOne(p => p.RequestType)
                .WithMany(s => s.Booking)
                .HasForeignKey(p => p.RequestType.Id);
        }
    }
}

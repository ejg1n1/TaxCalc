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
    public class BookingDetailsConfig : IEntityTypeConfiguration<BookingDetails>
    {
        public void Configure(EntityTypeBuilder<BookingDetails> builder)
        {
            //Primary Key
            builder.HasKey(u => u.Id);

            //Indexes

            //Maps to Table
            builder.ToTable("BookingDetails");

            //Table limitations / sizes

            //Entity relationships

        }
    }
}

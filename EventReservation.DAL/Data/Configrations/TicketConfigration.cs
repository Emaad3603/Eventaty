using EventReservation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.DAL.Data.Configrations
{
    public class TicketConfigration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(T => T.TicketPrice).HasColumnType("decimal(18,2)");
            builder.HasIndex(T => T.TicketCode)
                   .IsUnique();
                   

        }
    }
}

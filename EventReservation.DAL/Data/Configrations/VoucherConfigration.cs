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
    public class VoucherConfigration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.Property(V => V.VoucherDiscount).HasColumnType("decimal(18,2)");
            builder.HasIndex(V => V.VoucherCode)
                   .IsUnique();
        }
    }
}

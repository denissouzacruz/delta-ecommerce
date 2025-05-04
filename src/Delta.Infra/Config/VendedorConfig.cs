using Delta.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Infra.Config
{
    public class VendedorConfig : IEntityTypeConfiguration<Vendedor>
    {
        public void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            builder.ToTable("Vendedores")
                .HasKey("Id");

            builder.Property(p => p.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Email).HasColumnType("varchar(100)").IsRequired();

            builder
                .HasMany(v => v.Produtos)
                .WithOne(p => p.Vendedor);
        }
    }
}

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

            builder
                .HasMany(v => v.Produtos)
                .WithOne(p => p.Vendedor);
        }
    }
}

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
    public class ProdutoConfig : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos")
                .HasKey("Id");

            builder.Property(p => p.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Descricao).HasColumnType("varchar(200)").IsRequired();
            builder.Property(p => p.Valor).HasColumnType("decimal(17,2)").IsRequired();
            builder.Property(p => p.QuantidadeEstoque).IsRequired();
            builder.Property(p => p.Imagem).HasColumnType("varchar(100)").IsRequired();

            builder
                .HasOne(p => p.Categoria)
                .WithMany(p => p.Produtos)
                .HasForeignKey(p => p.CategoriaId);

            builder
                .HasOne(p => p.Vendedor)
                .WithMany(p=> p.Produtos)
                .HasForeignKey(p => p.VendedorId);

        }    
    }
}

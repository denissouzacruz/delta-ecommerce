using Delta.Business.Models;
using Delta.Infra.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Infra.Context
{
    public class DeltaDbContext: DbContext
    {
        public DeltaDbContext(DbContextOptions<DeltaDbContext> options)
            : base(options)
        {
                
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoConfig());
            modelBuilder.ApplyConfiguration(new CategoriaConfig());
            modelBuilder.ApplyConfiguration(new VendedorConfig());
            base.OnModelCreating(modelBuilder);
        }

    }
}

using Delta.AppMvc.Data;
using Delta.AppMvc.ViewModel;
using Delta.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Delta.AppMvc.Configurations
{
    public static class DbMigrationsHelpers
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        { 
            DbMigrationsHelpers.EnsureSeedData(app).Wait();
        }

        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        { 
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var contextID = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var contextDelta = scope.ServiceProvider.GetRequiredService<DeltaDbContext>();
           
            if (env.IsDevelopment())
            {
                await contextID.Database.MigrateAsync();
                await contextDelta.Database.MigrateAsync();

                await EnsureSeedProducts(contextID, contextDelta);
            }
        }

        private static async Task EnsureSeedProducts(ApplicationDbContext identityContext, DeltaDbContext deltaDb)
        {
            //Realiza a carga inicial dos dados

            if (deltaDb.Categorias.Any())
                return;

            var idUsuario = Guid.NewGuid();
            await identityContext.Users.AddAsync(new Microsoft.AspNetCore.Identity.IdentityUser
            { 
                Id = idUsuario.ToString(),
                UserName = "teste@teste.com",
                NormalizedUserName = "TESTE@TESTE.COM",
                Email = "teste@teste.com",
                NormalizedEmail = "TESTE@TESTE.COM",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAEA8BzmHCVEcOD+VNHR7Z91SjCRm9Zc4yodRPaowNC98ttq1IuwawRlqBzwUPidXCnw==",
                TwoFactorEnabled = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            });

            await identityContext.SaveChangesAsync();

            deltaDb.Vendedores.Add(new Business.Models.Vendedor() { Id = idUsuario, Nome = "Teste", Email= "teste@teste.com" });
            await deltaDb.SaveChangesAsync();

            var categoriaPapelaria = Guid.NewGuid();
            var categoriaInformatica = Guid.NewGuid();

            deltaDb.Categorias.AddRange(
                new Business.Models.Categoria()
                { 
                    Id = categoriaPapelaria,
                    Nome = "Papelaria",
                    Descricao = "Materiais Acadêmicos"
                },
                new Business.Models.Categoria()
                {
                    Id = categoriaInformatica,
                    Nome = "Informática",
                    Descricao = "Artigos eletrônicos e informática em geral"
                }
            );
            await deltaDb.SaveChangesAsync();

            if (deltaDb.Produtos.Any())
                return;

            deltaDb.Produtos.AddRange(
                new Business.Models.Produto()
                { 
                    Id = Guid.NewGuid(),
                    Nome = "Livro A Cabana",
                    Descricao = "Livro A Cabana / Literatura Estrangeira",
                    CategoriaId = categoriaPapelaria,
                    VendedorId = idUsuario,
                    Imagem = "5be38508-8799-4625-8c68-77100b36c7fc-Acabana.jpg",
                    Valor = 30,
                    QuantidadeEstoque = 245
                },
                new Business.Models.Produto()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Teclado Logi",
                    Descricao = "Teclado Logi, sem bluetooth",
                    CategoriaId = categoriaInformatica,
                    VendedorId = idUsuario,
                    Imagem = "6afb183a-744b-4f99-b394-6180f9d3b1dd-teclado_logi.png",
                    Valor = 122,
                    QuantidadeEstoque = 198
                }
            );

            await deltaDb.SaveChangesAsync();
        }
    }
}

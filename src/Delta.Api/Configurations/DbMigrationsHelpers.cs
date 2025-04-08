﻿using Microsoft.EntityFrameworkCore;
using Delta.Infra.Context;
using Delta.Api.Context;

namespace Delta.Api.Configurations
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

            var contextID = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
            var contextDelta = scope.ServiceProvider.GetRequiredService<DeltaDbContext>();

            if (env.IsDevelopment())
            {
                await contextID.Database.MigrateAsync();
                await contextDelta.Database.MigrateAsync();

                await EnsureSeedProducts(contextID, contextDelta);
            }
        }

        private static async Task EnsureSeedProducts(ApiDbContext identityContext, DeltaDbContext deltaDb)
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
                PasswordHash = "AQAAAAIAAYagAAAAEDTNe5lU6PWDRTXDd0G0kgK/vIrumY/QTO4s2dMRrtYolGMmeiilKpZ02aJ0zYAcHQ==",
                TwoFactorEnabled = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            });

            await identityContext.SaveChangesAsync();

            deltaDb.Vendedores.Add(new Business.Models.Vendedor() { Id = idUsuario });
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
                    Imagem = "JHGFGHGFDDFGH",
                    Valor = 30.55m,
                    QuantidadeEstoque = 245
                },
                new Business.Models.Produto()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Teclado Logi",
                    Descricao = "Teclado Logi, sem bluetooth",
                    CategoriaId = categoriaInformatica,
                    VendedorId = idUsuario,
                    Imagem = "JHGFGHGFDDFGH",
                    Valor = 122.70m,
                    QuantidadeEstoque = 198
                }
            );

            await deltaDb.SaveChangesAsync();
        }
    }
}

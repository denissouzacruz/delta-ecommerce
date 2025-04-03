using Delta.AppMvc.Data;
using Delta.Infra.Context;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Delta.AppMvc.Configurations
{
    public static class DatabaseSelectExtension
    {
        public static void AddDatabaseSelector(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionLite") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

                builder.Services.AddDbContext<DeltaDbContext>(options =>
                    options.UseSqlite(connectionString));
            }
            else
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

                builder.Services.AddDbContext<DeltaDbContext>(options =>
                    options.UseSqlServer(connectionString));
            }

        }
    }
}

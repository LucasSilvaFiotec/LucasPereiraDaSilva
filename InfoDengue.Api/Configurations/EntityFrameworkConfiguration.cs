using InfoDengue.Infra.Data.Contexts;
using InfoDengue.Infra.Data.Interfaces;
using InfoDengue.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InfoDengue.Configurations
{
    public static class EntityFrameworkConfiguration
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            //capturar string de conexão
            var connectionString = configuration.GetConnectionString("DBApiConsultorio");

            //injetar a connectionString na classe SqlServerContext do EntityFramework
            services.AddDbContext<SqlServerContext>(options =>
            options.UseSqlServer(connectionString));

            //injeção de dependência para o UnitOfWork
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}

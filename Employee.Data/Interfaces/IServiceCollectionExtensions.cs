using Employee.Business.Interfaces;
using Employee.Data.Context;
using Employee.Data.RepoUnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.Data.Interfaces;

public static class IServiceCollectionExtensions
{

  public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext(configuration);
    services.AddRepositories();
  }

  public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(connectionString,
           builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
  }

  private static void AddRepositories(this IServiceCollection services)
  {
    services
        .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
        .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
  }
}

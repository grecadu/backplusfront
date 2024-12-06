using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Employee.Business.Interfaces
{
  public static class IServiceCollectionExtensions
  {

    public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddAutoMapper();
      services.AddMediator();
      services.AddCorsServiceConfiguration();
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    private static void AddMediator(this IServiceCollection services)
    {
      services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    private static void AddCorsServiceConfiguration(this IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy("AllowSpecificOrigin",
          builder =>
          {
            builder.WithOrigins("http://127.0.0.1:4200")
                 .AllowAnyMethod()
                 .AllowAnyHeader();
          });
      });
    }
  }
}

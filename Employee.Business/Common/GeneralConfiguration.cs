using Microsoft.Extensions.Configuration;

namespace Employee.Business.Common;

public class GeneralConfiguration
{
  public IConfiguration Configuration;

  public GeneralConfiguration()
  {
    Configuration = GetConfiguration();
  }

  private IConfiguration GetConfiguration()
  {
    var environment = "Development";
    var jsonFileName = "appsettings.json";
    if (environment == "Development")
    {
      jsonFileName = "appsettings.Development.json";
    }


    IConfiguration configuration = new ConfigurationBuilder()
      .SetBasePath($@"{Directory.GetCurrentDirectory()}\..\Employee")
      .AddJsonFile(jsonFileName)
      .Build();

    return configuration;
  }
}

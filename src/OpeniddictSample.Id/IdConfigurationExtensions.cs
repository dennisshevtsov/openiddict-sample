using OpeniddictSample.Id;

namespace Microsoft.Extensions.Configuration;

public static class IdConfigurationExtensions
{
  public static DbSettings GetDbSettings(this IConfiguration configuration, string section = "ConnectionStrings") =>
    configuration.GetSection(section).Get<DbSettings>() ??
    throw new Exception("There are no DB settings.");
}

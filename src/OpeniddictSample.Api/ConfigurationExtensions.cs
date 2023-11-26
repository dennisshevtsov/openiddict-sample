using OpeniddictSample.Api;

namespace Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
  public static AuthenticationSettings GetAuthenticationSettings(this IConfiguration configuration) =>
    configuration.GetSection("API_SERVICE").Get<AuthenticationSettings>() ??
    throw new Exception("There are no authenctication settings.");
}

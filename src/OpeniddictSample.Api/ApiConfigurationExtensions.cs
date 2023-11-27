using OpeniddictSample.Api;

namespace Microsoft.Extensions.Configuration;

public static class ApiConfigurationExtensions
{
  public static AuthenticationSettings GetAuthenticationSettings(this IConfiguration configuration, string section = "AUTHENTICATION") =>
    configuration.GetSection(section).Get<AuthenticationSettings>() ??
    throw new Exception("There are no authenctication settings.");
}

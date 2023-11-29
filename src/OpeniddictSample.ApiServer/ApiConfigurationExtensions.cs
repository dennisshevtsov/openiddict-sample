// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using OpeniddictSample.ApiServer;

namespace Microsoft.Extensions.Configuration;

public static class ApiConfigurationExtensions
{
  public static AuthenticationSettings GetAuthenticationSettings(this IConfiguration configuration, string section = "AUTHENTICATION") =>
    configuration.GetSection(section).Get<AuthenticationSettings>() ??
    throw new Exception("There are no authenctication settings.");
}

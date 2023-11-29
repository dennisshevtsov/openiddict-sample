// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using OpeniddictSample.OpeniddictServer;

namespace Microsoft.Extensions.Configuration;

public static class OpeniddictServerConfigurationExtensions
{
  public static OpeniddictServerDbSettings GetDbSettings(this IConfiguration configuration, string section = "ConnectionStrings")
  {
    ArgumentNullException.ThrowIfNull(configuration);

    return configuration.GetSection(section).Get<OpeniddictServerDbSettings>() ??
           throw new Exception("There are no DB settings.");
  }
}

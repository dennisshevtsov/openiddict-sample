// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using OpeniddictSample.Id;

namespace Microsoft.Extensions.Configuration;

public static class IdConfigurationExtensions
{
  public static DbSettings GetDbSettings(this IConfiguration configuration, string section = "ConnectionStrings") =>
    configuration.GetSection(section).Get<DbSettings>() ??
    throw new Exception("There are no DB settings.");
}

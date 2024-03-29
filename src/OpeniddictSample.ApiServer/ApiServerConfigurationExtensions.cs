﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using OpeniddictSample.ApiServer;

namespace Microsoft.Extensions.Configuration;

public static class ApiServerConfigurationExtensions
{
  public static ApiServerAuthenticationSettings GetAuthenticationSettings(this IConfiguration configuration) =>
    configuration.Get<ApiServerAuthenticationSettings>() ??
    throw new Exception("There are no authenctication settings.");
}

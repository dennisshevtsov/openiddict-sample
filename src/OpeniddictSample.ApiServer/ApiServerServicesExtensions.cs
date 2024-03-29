﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using OpeniddictSample.ApiServer;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApiServerServicesExtensions
{
  public static IServiceCollection SetUpApi(this IServiceCollection services)
  {
    ArgumentNullException.ThrowIfNull(services);

    services.AddControllers(options =>
    {
      AuthorizationPolicy policy =
        new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                        .Build();
      AuthorizeFilter filter = new(policy);

      options.Filters.Add(filter);
    });

    return services;
  }

  public static IServiceCollection SetUpAuthentication(
    this IServiceCollection services, ApiServerAuthenticationSettings settings)
  {
    ArgumentNullException.ThrowIfNull(services);
    ArgumentNullException.ThrowIfNull(settings?.IdentityProviderUrl);

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.Authority = settings.IdentityProviderUrl);

    return services;
  }
}

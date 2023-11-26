// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApiServicesExtensions
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

  public static IServiceCollection SetUpAuthentication(this IServiceCollection services)
  {
    ArgumentNullException.ThrowIfNull(services);

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
              options.Authority = "http://localhost:5004"; // id proj url
              options.Audience = "openiddict-sample-api";
              options.RequireHttpsMetadata = false;
            });

    return services;
  }
}

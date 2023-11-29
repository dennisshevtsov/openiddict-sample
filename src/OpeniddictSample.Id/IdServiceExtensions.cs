// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.EntityFrameworkCore;
using OpeniddictSample.Id;

namespace Microsoft.Extensions.DependencyInjection;

public static class IdServiceExtensions
{
  public static IServiceCollection SetUpOpenIddict(this IServiceCollection services, DbSettings dbSettings)
  {
    ArgumentNullException.ThrowIfNull(services);
    ArgumentNullException.ThrowIfNull(dbSettings?.ConnectionString);

    services.AddDbContext<DbContext>(options =>
    {
      options.UseNpgsql(dbSettings.ConnectionString);
      options.UseOpenIddict();
    });

    services.AddOpenIddict()
            .AddCore(builder => builder.UseEntityFrameworkCore()
                                       .UseDbContext<DbContext>())
            .AddServer(builder => builder.SetTokenEndpointUris("connect/token")
                                         .AllowClientCredentialsFlow()
                                         .AddDevelopmentEncryptionCertificate()
                                         .AddDevelopmentSigningCertificate()
                                         .UseAspNetCore()
                                         .EnableTokenEndpointPassthrough())
            .AddValidation(builder =>
            {
              builder.UseLocalServer();
              //builder.AddAudiences("openiddict-sample-api");
              builder.UseAspNetCore();
            });

    return services;
  }
}

// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpeniddictSample.OpeniddictServer;

namespace Microsoft.Extensions.DependencyInjection;

public static class OpeniddictServerServiceExtensions
{
  public static IServiceCollection SetUpOpenIddict(this IServiceCollection services)
  {
    ArgumentNullException.ThrowIfNull(services);

    services.AddDbContext<DbContext>((provider, builder) =>
    {
      OpeniddictServerDbSettings settings = provider.GetRequiredService<IOptions<OpeniddictServerDbSettings>>().Value;
      builder.UseNpgsql(settings.OpeniddictServerDb);
      builder.UseOpenIddict();
    });

    services.AddOpenIddict()
            .AddCore(builder => builder.UseEntityFrameworkCore()
                                       .UseDbContext<DbContext>())
            .AddServer(builder => builder.SetTokenEndpointUris("connect/token")
                                         .SetAuthorizationEndpointUris("http://localhost:5005/signin")
                                         .AllowClientCredentialsFlow()
                                         .AllowAuthorizationCodeFlow()
                                         .AddDevelopmentEncryptionCertificate()
                                         .AddDevelopmentSigningCertificate()
                                         .UseAspNetCore()
                                         .EnableTokenEndpointPassthrough())
            .AddValidation(builder =>
            {
              builder.UseLocalServer();
              builder.UseAspNetCore();
            });

    return services;
  }
}

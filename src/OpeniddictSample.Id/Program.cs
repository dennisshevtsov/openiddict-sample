// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContext>(options =>
                {
                  string? connectionString = builder.Configuration.GetConnectionString("openiddict-id-db");
                  ArgumentNullException.ThrowIfNullOrWhiteSpace(connectionString);
                  options.UseNpgsql(connectionString);
                  options.UseOpenIddict();
                });
builder.Services.AddOpenIddict()
                .AddCore(builder => builder.UseEntityFrameworkCore())
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

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
  DbContext dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
  dbContext.Database.EnsureCreated();

  IOpenIddictApplicationManager manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

  if (manager.FindByClientIdAsync("openiddict-sample-api").GetAwaiter().GetResult() == null)
  {
    manager.CreateAsync(new OpenIddictApplicationDescriptor
    {
      ClientId = "openiddict-sample-api",
      ClientSecret = "test",
      DisplayName = "Openiddict Sample API",
      Permissions =
      {
        Permissions.Endpoints.Token,
        Permissions.GrantTypes.ClientCredentials,
      },
    }).GetAwaiter().GetResult();
  }
}

app.Run();

// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Microsoft.AspNetCore.Builder;

public static class IdWebApplicationExtensions
{
  public static WebApplication SetUpDb(this WebApplication app)
  {
    ArgumentNullException.ThrowIfNull(app);

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

    return app;
  }
}

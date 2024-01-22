// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace OpeniddictSample.OpeniddictServer;

public sealed class IdDbInitializer(DbContext dbContext, IOpenIddictApplicationManager manager)
{
  private readonly DbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
  private readonly IOpenIddictApplicationManager _manager = manager ?? throw new ArgumentNullException(nameof(manager));

  public async Task InitializeAsync(CancellationToken cancellationToken)
  {
    await _dbContext.Database.EnsureCreatedAsync(cancellationToken);

    if (await _manager.FindByClientIdAsync("openiddict-sample-api", cancellationToken) == null)
    {
      await _manager.CreateAsync(IdDbInitializer.GetDefaultClient(), cancellationToken);
    }
  }

  private static OpenIddictApplicationDescriptor GetDefaultClient() => new()
  {
    ClientId = "openiddict-sample-api",
    ClientSecret = "test",
    DisplayName = "Openiddict Sample API",
    Permissions =
    {
      Permissions.Endpoints.Token,
      Permissions.GrantTypes.ClientCredentials,
    },
  };
}

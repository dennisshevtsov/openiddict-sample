// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using System.Xml.Linq;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace OpeniddictSample.OpeniddictServer;

public sealed class OpeniddictServerDbInitializer(ApplicationDbContext dbContext, IOpenIddictApplicationManager manager, UserManager<IdentityUser> userManager)
{
  private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
  private readonly IOpenIddictApplicationManager _manager = manager ?? throw new ArgumentNullException(nameof(manager));
  private readonly UserManager<IdentityUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

  public async Task InitializeAsync(CancellationToken cancellationToken)
  {
    await _dbContext.Database.EnsureCreatedAsync(cancellationToken);

    if (await _manager.FindByClientIdAsync("openiddict-sample-api", cancellationToken) == null)
    {
      await _manager.CreateAsync(OpeniddictServerDbInitializer.GetDefaultClient(), cancellationToken);
    }

    IdentityUser? testUserEntity = null;

    if ((testUserEntity = await _userManager.FindByNameAsync("test")) == null)
    {
      testUserEntity = new IdentityUser
      {
        Email = "test",
        UserName = "test",
      };

      var result = await _userManager.CreateAsync(testUserEntity, "test");
    }
  }

  private static OpenIddictApplicationDescriptor GetDefaultClient() => new()
  {
    ClientId = "openiddict-sample-api",
    DisplayName = "Openiddict Sample API",
    Type = ClientTypes.Public,
    Permissions =
    {
      Permissions.Endpoints.Authorization,
      Permissions.Endpoints.Token,
      Permissions.GrantTypes.AuthorizationCode,
      Permissions.GrantTypes.RefreshToken,
      Permissions.ResponseTypes.Code,
      Permissions.Scopes.Email,
      Permissions.Scopes.Profile,
      Permissions.Scopes.Roles,
    },
    RedirectUris =
    {
      new Uri("http://localhost:5005"),
      new Uri("http://localhost:5005/signin-callback"),
      new Uri("http://localhost:5005/silent-callback"),
    },
    Requirements =
    {
        Requirements.Features.ProofKeyForCodeExchange,
    },
  };
}

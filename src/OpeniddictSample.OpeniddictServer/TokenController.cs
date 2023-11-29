// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace OpeniddictSample.OpeniddictServer;

[ApiController]
public sealed class TokenController : ControllerBase
{
  [HttpPost("connect/token")]
  public IActionResult Get()
  {
    ClaimsIdentity identity = new
    (
      authenticationType: TokenValidationParameters.DefaultAuthenticationType,
      nameType: Claims.Name,
      roleType: Claims.Role
    );

    identity.SetClaim(Claims.Subject, Guid.NewGuid().ToString());
    identity.SetClaim(Claims.Name, "test");
    identity.SetScopes("test");
    identity.SetResources("test");
    identity.SetDestinations((Claim claim) =>
      claim.Type == Claims.Name && claim.Subject?.HasScope(Scopes.Profile) == true ?
      [Destinations.AccessToken, Destinations.IdentityToken] :
      [Destinations.AccessToken]);

    return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
  }
}

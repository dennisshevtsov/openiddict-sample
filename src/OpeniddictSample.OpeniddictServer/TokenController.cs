// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Net;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;
using static System.Net.Mime.MediaTypeNames;

namespace OpeniddictSample.OpeniddictServer;

[ApiController]
public sealed class TokenController : ControllerBase
{
  private readonly IOpenIddictApplicationManager _applicationManager;
  private readonly IOpenIddictAuthorizationManager _authorizationManager;
  private readonly SignInManager<IdentityUser> _signInManager;

  public TokenController(IOpenIddictApplicationManager applicationManager, IOpenIddictAuthorizationManager authorizationManager, SignInManager<IdentityUser> signInManager)
  {
    _applicationManager = applicationManager ?? throw new ArgumentNullException(nameof(applicationManager));
    _authorizationManager = authorizationManager ?? throw new ArgumentNullException(nameof(authorizationManager));
    _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
  }

  [HttpPost("connect/authorize")]
  [HttpPost("connect/token")]
  public async Task<IActionResult> Get()
  {
    var request = HttpContext.GetOpenIddictServerRequest();
    var result = await HttpContext.AuthenticateAsync();

    var signInResult =
          await _signInManager.PasswordSignInAsync(
            request!.Username!, request.Password!, false, false);

    var user = await _signInManager.UserManager.FindByNameAsync(request.Username!);

    //if (signInResult != null && signInResult.Succeeded)
    //{
    //  return NoContent();
    //}

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
    //AuthenticationService


    //var user = await _userManager.GetUserAsync(result.Principal) ??
    //        throw new InvalidOperationException("The user details cannot be retrieved.");

    var application = await _applicationManager.FindByClientIdAsync(request!.ClientId!) ??
            throw new InvalidOperationException("Details concerning the calling client application cannot be found.");

    var authorizations = await _authorizationManager.FindAsync(
           subject: user!.Id,
           client: await _applicationManager.GetIdAsync(application),
           status: Statuses.Valid,
           type: AuthorizationTypes.Permanent,
           scopes: request.GetScopes()).ToListAsync();

    var authorization = authorizations.LastOrDefault();
    await _authorizationManager.CreateAsync(
        identity: identity,
        subject: user.Id,
        client: await _applicationManager.GetIdAsync(application),
        type: AuthorizationTypes.Permanent,
        scopes: identity.GetScopes());

    identity.SetAuthorizationId(await _authorizationManager.GetIdAsync(authorization));
    //identity.SetDestinations(GetDestinations);

    return NoContent();
    //return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
  }
}

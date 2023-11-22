// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Client;
using static OpenIddict.Client.OpenIddictClientModels;

namespace OpeniddictSample.Id.Test;

[TestClass]
public sealed class IdServiceTest
{
  [TestMethod]
  public async Task GetToken_CorrectCreadentials_TokenReturned()
  {
    // Arrange
    ServiceCollection services = new();
    services.AddOpenIddict()
            .AddClient(options =>
            {
              options.AddRegistration(new OpenIddictClientRegistration
              {
                Issuer = new Uri("http://localhost:5002"),
                ClientId = "openiddict-api",
                ClientSecret = "test",
              });
            });

    IServiceProvider serviceProvider = services.BuildServiceProvider();

    OpenIddictClientService openIddictClientService =
      serviceProvider.GetRequiredService<OpenIddictClientService>();

    // Act
    ClientCredentialsAuthenticationResult result =
      await openIddictClientService.AuthenticateWithClientCredentialsAsync(
        new ClientCredentialsAuthenticationRequest());

    // Assert
    Assert.IsNotNull(result.AccessToken);
  }
}

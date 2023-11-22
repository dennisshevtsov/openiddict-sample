// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Client;
using System.Net;

namespace OpeniddictSample.Api.Test;

[TestClass]
public sealed class SurveyTest
{
#pragma warning disable CS8618
  private HttpClient _httpClient;
  private IServiceScope _serviceScope;
  private OpenIddictClientService _openIddictClientService;
#pragma warning restore CS8618

  [TestInitialize]
  public void Initialize()
  {
    _httpClient = new HttpClient
    {
      BaseAddress = new Uri("http://localhost:5001/api/")
    };

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
  }

  [TestCleanup] public void Cleanup()
  {
    _httpClient?.Dispose();
    _serviceScope.Dispose();
  }

  [TestMethod]
  public async Task GetSurvey_NoToken_401Returned()
  {
    // Act
    HttpResponseMessage responseMessage = await _httpClient.GetAsync("survey");

    // Assert
    Assert.AreEqual(HttpStatusCode.Unauthorized, responseMessage.StatusCode);
  }
}

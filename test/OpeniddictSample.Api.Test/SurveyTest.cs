// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Net;

namespace OpeniddictSample.Api.Test;

[TestClass]
public sealed class SurveyTest
{
#pragma warning disable CS8618
  private HttpClient _httpClient;
#pragma warning restore CS8618

  [TestInitialize]
  public void Initialize()
  {
    _httpClient = new HttpClient
    {
      BaseAddress = new Uri("http://localhost:5001/api/")
    };
  }

  [TestCleanup] public void Cleanup()
  {
    _httpClient?.Dispose();
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

// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace OpeniddictSample.ApiServer;

[Route("api/echo")]
[ApiController]
public sealed class EchoController : ControllerBase
{
  [HttpGet("{message}", Name = nameof(EchoController.Echo))]
  [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
  public EchoResponseDto Echo([FromRoute] string message) => new(message);
}

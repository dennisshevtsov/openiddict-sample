// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace OpeniddictSample.Api;

[Route("api/echo")]
[ApiController]
public sealed class EchoController : ControllerBase
{
  [HttpGet]
  [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
  public IActionResult GetSurveys(EchoDto echoDto) => Ok(echoDto);
}

// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace OpeniddictSample.Api;

[Route("api/survey")]
[ApiController]
public sealed class SurveyController : ControllerBase
{
  [HttpGet]
  public IActionResult GetSurveys()
  {
    return Ok(new SurveyResponseDto[]
    {
      new(Guid.NewGuid(), "Survey #1", "Test survey.", new DateTime(2023, 10, 17)),
      new(Guid.NewGuid(), "Survey #2", "Test survey.", new DateTime(2023, 10, 17)),
      new(Guid.NewGuid(), "Survey #3", "Test survey.", new DateTime(2023, 10, 17)),
    });
  }
}

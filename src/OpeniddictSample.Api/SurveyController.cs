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
      new(new Guid("f7b167ad-740d-4f6d-8986-8408b3cf9698"), "Survey #1", "Test survey.", new DateTime(2023, 10, 17)),
      new(new Guid("8a491ae3-d105-4d42-a8a5-cd1ed793347a"), "Survey #2", "Test survey.", new DateTime(2023, 10, 17)),
      new(new Guid("0a8f3dad-f7e5-4e14-8609-616a5493cf41"), "Survey #3", "Test survey.", new DateTime(2023, 10, 17)),
    });
  }
}

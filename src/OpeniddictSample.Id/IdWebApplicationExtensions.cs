﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using OpeniddictSample.Id;

namespace Microsoft.AspNetCore.Builder;

public static class IdWebApplicationExtensions
{
  public static WebApplication SetUpDb(this WebApplication app)
  {
    ArgumentNullException.ThrowIfNull(app);

    using (IServiceScope scope = app.Services.CreateScope())
    {
      scope.ServiceProvider.GetRequiredService<IdDbInitializer>()
                           .InitializeAsync(CancellationToken.None)
                           .GetAwaiter()
                           .GetResult();
    }

    return app;
  }
}
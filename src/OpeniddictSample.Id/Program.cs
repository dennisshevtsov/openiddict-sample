// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.SetUpOpenIddict(builder.Configuration.GetDbSettings());
builder.Services.AddControllers();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
  DbContext dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
  dbContext.Database.EnsureCreated();

  IOpenIddictApplicationManager manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

  if (manager.FindByClientIdAsync("openiddict-sample-api").GetAwaiter().GetResult() == null)
  {
    manager.CreateAsync(new OpenIddictApplicationDescriptor
    {
      ClientId = "openiddict-sample-api",
      ClientSecret = "test",
      DisplayName = "Openiddict Sample API",
      Permissions =
      {
        Permissions.Endpoints.Token,
        Permissions.GrantTypes.ClientCredentials,
      },
    }).GetAwaiter().GetResult();
  }
}

app.UseRouting();
app.MapControllers();
app.Run();

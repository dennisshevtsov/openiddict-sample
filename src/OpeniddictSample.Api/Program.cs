// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.SetUpApi();
builder.Services.SetUpAuthentication();

WebApplication app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

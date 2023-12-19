// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using OpeniddictSample.OpeniddictServer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<OpeniddictServerDbSettings>(builder.Configuration);
builder.Services.SetUpOpenIddict();
builder.Services.AddControllers();
builder.Services.AddAuthentication("Bearer");
builder.Services.AddAuthorization();
builder.Services.AddCors();

WebApplication app = builder.Build();
app.SetUpDb();
app.UseRouting();
app.UseCors(builder => builder.AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

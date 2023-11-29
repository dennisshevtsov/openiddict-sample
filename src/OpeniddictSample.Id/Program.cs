// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.SetUpOpenIddict(builder.Configuration.GetDbSettings());
builder.Services.AddControllers();

WebApplication app = builder.Build();
app.SetUpDb();
app.UseRouting();
app.MapControllers();
app.Run();

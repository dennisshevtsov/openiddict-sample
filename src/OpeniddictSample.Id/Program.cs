// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContext>(builder =>
                {
                  builder.UseNpgsql("");
                  builder.UseOpenIddict();
                });
builder.Services.AddOpenIddict()
                .AddCore(builder => builder.UseEntityFrameworkCore())
                .AddServer(builder => builder.SetTokenEndpointUris("connect/token")
                                             .AllowClientCredentialsFlow()
                                             .AddDevelopmentEncryptionCertificate()
                                             .AddDevelopmentSigningCertificate()
                                             .UseAspNetCore()
                                             .EnableTokenEndpointPassthrough())
                .AddValidation(builder =>
                {
                  builder.UseLocalServer();
                  builder.UseAspNetCore();
                });

WebApplication app = builder.Build();
app.Run();
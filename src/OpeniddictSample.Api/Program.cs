// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
                {
                  AuthorizationPolicy policy =
                    new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                    .Build();
                  AuthorizeFilter filter = new(policy);

                  options.Filters.Add(filter);
                });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                  options.Authority = "http://localhost:5002"; // id proj url
                  options.Audience = "openiddict-sample-api";
                  options.RequireHttpsMetadata = false;
                });

WebApplication app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

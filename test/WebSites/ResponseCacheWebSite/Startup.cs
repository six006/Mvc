// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;

namespace ResponseCacheWebSite
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var configuration = app.GetTestConfiguration();
            app.UseServices(services =>
            {
                services.AddMvc(configuration);
                services.Configure<MvcOptions>(options =>
                {
                    options.CacheProfiles.Add(
                        "PublicCache30Sec", new CacheProfile
                        {
                            Duration = 30,
                            Location = ResponseCacheLocation.Any
                        });

                    options.CacheProfiles.Add(
                        "PrivateCache30Sec", new CacheProfile
                        {
                            Duration = 30,
                            Location = ResponseCacheLocation.Client
                        });

                    options.CacheProfiles.Add(
                        "NoCache", new CacheProfile
                        {
                            NoStore = true,
                            Duration = 0,
                            Location = ResponseCacheLocation.None
                        });
                });
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
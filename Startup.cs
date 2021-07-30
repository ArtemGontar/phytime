using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Phytime.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Phytime.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Phytime
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PhytimeContext>(options => options.UseSqlServer(connection));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<EmailService>(x =>
                new EmailService(x.GetRequiredService<IServiceScopeFactory>(),
                x.GetRequiredService<IConfiguration>()));

            services.AddHostedService<EmailService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }

    //public class Startup
    //{
    //    public Startup(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //    }

    //    public IConfiguration Configuration { get; }

    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        string connection = Configuration.GetConnectionString("DefaultConnection");
    //        services.AddDbContext<PhytimeContext>(options => options.UseSqlServer(connection));

    //        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    //            .AddCookie(options => //CookieAuthenticationOptions
    //            {
    //                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    //            });

    //        services.AddMvc();
    //        services.AddSession();

    //        services.AddSingleton<EmailService>(x =>
    //            new EmailService(x.GetRequiredService<IServiceScopeFactory>(),
    //            x.GetRequiredService<IConfiguration>()));

    //        services.AddHostedService<EmailService>();

    //        //services.AddSpaStaticFiles(configuration =>
    //        //{
    //        //    configuration.RootPath = "ClientApp/dist";
    //        //});
    //    }

    //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    //    {
    //        if (env.IsDevelopment())
    //        {
    //            app.UseDeveloperExceptionPage();
    //        }
    //        app.UseStaticFiles();
    //        if (!env.IsDevelopment())
    //        {
    //            app.UseSpaStaticFiles();
    //        }

    //        app.UseRouting();

    //        app.UseAuthentication();
    //        app.UseAuthorization();

    //        app.UseEndpoints(endpoints =>
    //        {
    //            endpoints.MapControllerRoute(
    //                name: "default",
    //                pattern: "{controller=Home}/{action=Index}/{id?}");
    //        });

    //        app.UseSpa(spa =>
    //        {
    //            spa.Options.SourcePath = "ClientApp";

    //            if (env.IsDevelopment())
    //            {
    //                spa.UseAngularCliServer(npmScript: "start");
    //            }
    //        });
    //    }
    //}
}
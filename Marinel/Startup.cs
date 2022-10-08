using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolDraftWebsite.Data;
using SchoolDraftWebsite.Services;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System;
using Marinel.Services;

namespace SchoolDraftWebsite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SchoolContext>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddTransient<ISecretProvider, SecretProvider>();
            services.AddSingleton<IMSGraphService, MSGraphService>();

            var instance = Environment.GetEnvironmentVariable("Instance") ?? Configuration["Instance"];
            var domain = Environment.GetEnvironmentVariable("Domain") ?? Configuration["Domain"];
            var clientId = Environment.GetEnvironmentVariable("ClientId") ?? Configuration["ClientId"];
            var tenantId = Environment.GetEnvironmentVariable("TenantId") ?? Configuration["TenantId"];
            var signedOutCallbackPath = Environment.GetEnvironmentVariable("SignedOutCallbackPath") ?? Configuration["SignedOutCallbackPath"];
            var signUpSignInPolicyId = Environment.GetEnvironmentVariable("SignUpSignInPolicyId") ?? Configuration["SignUpSignInPolicyId"];

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(options =>
            {
                options.Instance = instance;
                options.Domain = domain;
                options.ClientId = clientId;
                options.TenantId = tenantId;
                options.SignedOutCallbackPath = signedOutCallbackPath;
                options.SignUpSignInPolicyId = signUpSignInPolicyId;
            });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = options.DefaultPolicy;
            });

            services.AddRazorPages(options => {
                options.Conventions.AllowAnonymousToPage("/Index");
            })
            .AddMvcOptions(options => { })
            .AddMicrosoftIdentityUI();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

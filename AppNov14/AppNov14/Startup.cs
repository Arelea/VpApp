using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppNov14.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.AspNetCore.HttpOverrides;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace AppNov14
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
            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection2")));

            services.AddIdentity<Users, IdentityRole>(options=>
            {
                options.Password.RequiredLength = 8;  // ����������� �����
                options.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
                options.Password.RequireDigit = false; // ��������� �� �����
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
            }

                )
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".MyApp.Session";
                options.IdleTimeout = TimeSpan.FromHours(5);
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();
            services.AddMvc();
            services.AddLocalization();
        }
     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = new[]{new CultureInfo("ru")};

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru"),
                SupportedCultures = supportedCultures,
                FallBackToParentCultures = false
            });

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("ru");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
           ForwardedHeaders.XForwardedProto
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");            
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Gate}/{id?}");
            });
        }
    }
}

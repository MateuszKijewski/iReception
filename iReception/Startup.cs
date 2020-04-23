using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iReception.DataAccess;
using iReception.Models.Converters;
using iReception.Models.Converters.Interfaces;
using iReception.Repository;
using iReception.Repository.Interfaces;
using iReception.Services;
using iReception.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iReception
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
            services.AddDbContext<iReceptionDbContext>(options =>
                options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<iReceptionDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // Converters
            services.AddSingleton<IClientConverter, ClientConverter>();
            services.AddSingleton<IBuildingConverter, BuildingConverter>();
            services.AddSingleton<IRoomConverter, RoomConverter>();
            services.AddSingleton<IMinuteServiceConverter, MinuteServiceConverter>();
            services.AddSingleton<IServiceConverter, ServiceConverter>();
            // Repositories
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IMinuteServiceRepository, MinuteServiceRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IRoomToServiceRepository, RoomToServiceRepository>();
            services.AddScoped<IRoomToMinuteServiceRepository, RoomToMinuteServiceRepository>();
            // Services
            services.AddScoped<IClientService, ClientService>();            
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IMinuteServiceService, MinuteServiceService>();
            services.AddScoped<IServiceService, ServiceService>();            

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITimeProvider, TimeProvider>();

            // Identity settings
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
            });
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
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseExceptionHandler("/Home/Error");
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

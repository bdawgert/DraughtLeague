using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DraughtLeague.DAL;
using DraughtLeague.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;


namespace DraughtLeague.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("secrets.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string dbServer = Configuration["FantasyDraughtDatabase:Server"];
            string database = Configuration["FantasyDraughtDatabase:Database"];
            string userId = Configuration["FantasyDraughtDatabase:UserId"];
            string password = Configuration["FantasyDraughtDatabase:Password"];

            string connectionString = string.Format(Configuration.GetConnectionString("FantasyDraftProduction"), dbServer, database, userId, password);
            //services.Add(new ServiceDescriptor(typeof(EFDbContext), new EFDbContext(connectionString)));
            services.AddDbContext<LeagueDbContext>(options => options.UseSqlServer(connectionString));
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddScoped<SessionService>();
            //services.AddTransient(_ => UserManager.Create(connectionString));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x => {
                    x.LoginPath = new PathString("/Login");
                    x.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
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

            app.UseAuthentication();
            app.UseRouting();
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

using Elite.BiznessLayer.Abstract;
using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.Abstract;
using Elite.DataAccsessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Elite.EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteStoreCore
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
			services.AddDbContext<Context>();
			services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider).AddEntityFrameworkStores<Context>();
			//services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();
			services.AddMvc(config =>
			{
				var policy = new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				.Build();
				config.Filters.Add(new AuthorizeFilter(policy));
            });
			services.AddControllersWithViews();
            services.AddScoped<IAboytServices, AboutManager>();
            services.AddScoped<IAboutDal, EFAboutRepository>();
            services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Home/Index/";
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
			app.UseStatusCodePagesWithReExecute("/Home/Error404", "?code={0}");

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

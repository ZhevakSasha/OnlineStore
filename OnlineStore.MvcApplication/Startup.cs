using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineStore.BusinessLogic;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using OnlineStore.DataAccess.RepositoryPatterns;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http;
using System;

namespace OnlineStore.MvcApplication
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
            services.AddTransient<ApiTokenMessageHandler>();
            services.AddHttpClient("serviceApi")
                .ConfigureHttpClient((provider, c) => c.BaseAddress = new Uri(Configuration.GetSection("Urls:ServiceUrl").Value))
            .AddHttpMessageHandler<ApiTokenMessageHandler>();
            services.AddHttpClient("authApi")
                .ConfigureHttpClient((provider, c) => c.BaseAddress = new Uri(Configuration.GetSection("Urls:AuthUrl").Value));

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews()
               .AddDataAnnotationsLocalization()
               .AddViewLocalization();

            services.AddDbContext<DataBaseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            ////services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            ////  .AddCookie(x => x.LoginPath = "/login/loginForm");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                          .AddCookie(options =>
                          {
                              options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login/LoginForm");
                              options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Login/LoginForm");
                          });

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ICustomerRepository,EntityFrameworkCustomerRepository>();
            services.AddScoped<IProductRepository, EntityFrameworkProductRepository>();
            services.AddScoped<ISaleRepository, EntityFrameworkSaleRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleService, SaleService>();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                 {
                new CultureInfo("en"),
                new CultureInfo("ru")
                 };
                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRequestLocalization();

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Sale}/{action=SaleTable}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}

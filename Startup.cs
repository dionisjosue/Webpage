using AutoMapper;
using BuyWebPage.Data;
using BuyWebPage.Data.Entities;
using BuyWebPage.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BuyWebPage
{
    public class Startup
    {
        public IConfiguration Config;
        private readonly IHostingEnvironment _Env;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config, IHostingEnvironment _env)
        {
            Config = config;
            _Env = _env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<Users, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequiredLength = 6;
                config.Password.RequireUppercase = false;
            })
           .AddEntityFrameworkStores<BuyDbContext>();

            services.AddDbContext<BuyDbContext>(config =>
            {
                config.UseSqlServer(Config.GetConnectionString("BuyConnectionString"));

            });
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        //los campos del JSON DEBEN EMPEZAR EN MAYUSCULA EJEMPLO "Tokens: Issuer" 
                        ValidIssuer = Config["Tokens:Issuer"],
                        ValidAudience = Config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Tokens:Key"]))

                    };
                });
           

            services.AddAutoMapper();
            services.AddTransient<BuyDbSeeder>();
            services.AddScoped<IBuyRepository, BuyRepository>();

            services.AddTransient<IEmailService, EmailService>();
            services.AddMvc(config=>{
                if (_Env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }

            }).AddJsonOptions(opt=> opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            /* This is a form to configure AUTOMAPPER
             * Mapper.Initialize(config =>
            {
                config.CreateMap<OrderViewModel, Order>().ReverseMap();//permit that can use automapper to create objets trips a partir de un tripsviewmodel y viceversa
                config.CreateMap<ProductViewModels, Product>().ReverseMap();

                if you configure(of this form you have to USE Mapper.map<> in the api or wherever place)

            });*/

            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "index" });

            });
            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder2=
                        scope.ServiceProvider.GetService<BuyDbSeeder>();
                    seeder2.Seed().Wait();
                
                }
            }

        }
    }
}

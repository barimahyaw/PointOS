using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PointOS.BusinessLogic;
using PointOS.BusinessLogic.Interfaces;
using PointOS.DataAccess;
using PointOS.DataAccess.Entities;
using System;
using System.IO;
using System.Reflection;

namespace PointOS.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Swagger documentation
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("PointOS", new OpenApiInfo
                {
                    Title = "NickSoft Solutions POS API",
                    Version = "1",
                    Description = "NickSoft Solutions POS API",
                    //TermsOfService = new Uri(""),
                    Contact = new OpenApiContact
                    {
                        Email = "nanabarimah22@gmail.com",
                        Name = "NickSoft Solutions Support",
                        //Url = new Uri("")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "NickSoft Solutions Open License",
                        //Url = new Uri("")
                    }
                });

                //Get xml comments to add the comments to the documentation
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppConnectionString")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            // DI of interfaces
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IProductCategoryBusiness, ProductCategoryBusiness>();
            services.AddTransient<IProductBusiness, ProductBusiness>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                //setupAction.InjectStylesheet("/Assets/custom-ui.css");
                setupAction.SwaggerEndpoint("/swagger/PointOS/swagger.json", "Nicksoft Solutions POS API");
                setupAction.RoutePrefix = string.Empty; //so documentation will show on root url
                setupAction.DefaultModelExpandDepth(2);
                setupAction.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                setupAction.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                setupAction.EnableDeepLinking();
                setupAction.DisplayOperationId();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

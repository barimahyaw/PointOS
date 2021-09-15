using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PointOS.BusinessLogic;
using PointOS.BusinessLogic.Interfaces;
using PointOS.BusinessLogic.Security;
using PointOS.BusinessLogic.Validators;
using PointOS.BusinessLogic.Validators.IValidators;
using PointOS.Common.Helpers;
using PointOS.Common.Helpers.IHelpers;
using PointOS.Common.Settings;
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
            services.AddControllers(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                o.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    //builder.WithMethods("POST");
                    builder.AllowAnyHeader();
                });

                //services.AddCors(options =>
                //    options.AddPolicy("AllowSpecific", p => p.WithOrigins("http://localhost:1233")
                //        .WithMethods("GET")
                //        .WithHeaders("name")));

            });

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            //general configuration for token expiry time span
            //services.Configure<DataProtectionTokenProviderOptions>(options =>
            //    options.TokenLifespan = TimeSpan.FromHours(24));

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

                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                //Get xml comments to add the comments to the documentation
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });


            var appConnectionString = Configuration.GetSection("ConnectionStrings");
            services.Configure<ConnectionStrings>(appConnectionString);

            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppConnectionString")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //general configuration for token expiry time span
            services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromHours(48));


            var emailSettings = Configuration.GetSection("EmailSettings");
            services.Configure<EmailSettings>(emailSettings);

            var antiVirusPath = Configuration.GetSection("StaticFilesPath");
            services.Configure<StaticFilesPath>(antiVirusPath);

            var documentSettings = Configuration.GetSection("UploadDocumentSettings");
            services.Configure<UploadDocumentSettings>(documentSettings);

            var apiBaseUrlSettings = Configuration.GetSection("ApiBaseUrlSettings");
            services.Configure<ApiBaseUrlSettings>(apiBaseUrlSettings);

            // DI of interfaces
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IProductCategoryBusiness, ProductCategoryBusiness>();
            services.AddTransient<IProductBusiness, ProductBusiness>();
            services.AddTransient<ICompanyBusiness, CompanyBusiness>();
            services.AddTransient<IBranchBusiness, BranchBusiness>();
            services.AddTransient<IProductPricingBusiness, ProductPricingBusiness>();
            services.AddTransient<ITransactionBusiness, TransactionBusiness>();
            services.AddTransient<IUserAccountBusiness, UserAccountBusiness>();
            services.AddTransient<ICurrencyBusiness, CurrencyBusiness>();
            services.AddTransient<ICustomerBusiness, CustomerBusiness>();
            services.AddTransient<ISalesBusiness, SalesBusiness>();
            services.AddTransient<IDashboardBusiness, DashboardBusiness>();
            services.AddTransient<IProductStockBusiness, ProductStockBusiness>();

            services.AddScoped<IProductValidator, ProductValidator>();

            services.AddScoped<IUtils, Utils>();


            #region Custom Bearer or Basic Authenticaation with a JWT Token provider
            services.AddAuthentication("CustomAuthentication")
                .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("CustomAuthentication", options => { })
                .AddCookie();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomAuthentication", new AuthorizationPolicyBuilder("CustomAuthentication")
                    .RequireAuthenticatedUser().Build());
            });
            #endregion

            #region JWT Bearer Token Authentication Config
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ClockSkew = TimeSpan.FromMinutes(5),
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = Configuration["JwtIssuer"],
            //            ValidAudience = Configuration["JwtAudience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
            //        };
            //    });
            #endregion

            //services.AddAuthorization();
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

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

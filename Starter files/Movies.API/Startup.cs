using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.API.Contexts;
using Movies.API.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Movies.API
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
            services.AddMvc(options =>
            {
                // Return a 406 when an unsupported media type was requested
                options.ReturnHttpNotAcceptable = true;

                // Add XML formatters
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                options.InputFormatters.Add(new XmlSerializerInputFormatter(options));

                // Set XML as default format instead of JSON - the first formatter in the 
                // list is the default, so we insert the input/output formatters at 
                // position 0
                //options.OutputFormatters.Insert(0,new XmlSerializerOutputFormatter());
                //options.InputFormatters.Insert(0, new XmlSerializerInputFormatter(options));
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            // add support for compressing responses (eg gzip)
            services.AddResponseCompression();

            // suppress automatic model state validation when using the 
            // ApiController attribute (as it will return a 400 Bad Request
            // instead of the more correct 422 Unprocessable Entity when
            // validation errors are encountered)
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            // register the DbContext on the container, getting the connection string from
            // appSettings (note: use this during development; in a production environment,
            // it's better to store the connection string in an environment variable)
            var connectionString = Configuration["ConnectionStrings:MoviesDBConnectionString"];
            services.AddDbContext<MoviesContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IPostersRepository, PostersRepository>();
            services.AddScoped<ITrailersRepository, TrailersRepository>();

            services.AddAutoMapper();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Movies API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // use response compression (client should pass through 
            // Accept-Encoding)
            app.UseResponseCompression();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Movies API (v1)");
                // serve UI at root
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 

            app.UseMvc();
        }
    }
}

using System;
using Catalog.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Catalog.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;

namespace Catalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container. 
        public void ConfigureServices(IServiceCollection services) // Servicios
        {
            BsonSerializer.RegisterSerializer(          //Serialization for GUID
                new GuidSerializer(BsonType.String)); 
            BsonSerializer.RegisterSerializer(          //Serialization for DateTimeOffset
                new DateTimeOffsetSerializer(BsonType.String));
            services.AddSingleton<IMongoClient>(provider =>{
                var settings = Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
                return new MongoClient(settings.ConnectionString);
            } );// MongoDB inyection
            
            services.AddSingleton<IItemsRepo,MongoDBItemsRepository>(); // Items Service (InMemItemsRepo for local)
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc( "v1", new OpenApiInfo
                {
                    Title = "Online Items Catalog",
                    Description = "An API to learn the basics of .NET WebApis",                
                    Contact = new OpenApiContact
                    {
                        Name = "Jaciel Israel Reséndiz Ochoa",
                        Email = "jresendizochoa@deloitte.com",
                        Url = new Uri("https://github.com/reoj")
                    },
                    Version = "v1"
                });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

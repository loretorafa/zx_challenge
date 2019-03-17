using FluentValidation.AspNetCore;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ZX_Challenge.Application.GraphQL;
using ZX_Challenge.Application.Services;
using ZX_Challenge.Application.Validators;
using ZX_Challenge.Domain.Interfaces.Repositories;
using ZX_Challenge.Infrastructure;

namespace ZX_Challenge.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PdvRequestValidator>());

            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ZX Challange",
                    Description = "Sample PDV API",
                    Contact = new Contact
                    {
                        Name = "Rafael di Loreto",
                        Email = "loretorafa@gmail.com"
                    },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            services.AddGraphQl(schema =>
            {
                schema.SetQueryType<PdvQuery>();
                schema.SetMutationType<PdvMutation>();
            });

            services.AddDbContext<ZxContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:ZX_Challenge"], x => x.UseNetTopologySuite()));

            services.AddScoped<IPdvRepository, PdvRepository>();
            services.AddScoped<IPdvService, PdvService>();

            ConfigureGraphQlServices(services);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZX Challange - Sample PDV API");
                c.RoutePrefix = string.Empty;
            });

            app.UseGraphiql("/graphiql", options =>
            {
                options.GraphQlEndpoint = "/graphql";
            });

            app.UseGraphQl("/graphql");

            app.UseMvc();
        }

        private static void ConfigureGraphQlServices(IServiceCollection services)
        {
            var graphQlProject = Assembly.GetAssembly(typeof(PdvQuery));
            var projectNamespace = graphQlProject.GetName().Name;
            var graphQlTypes = graphQlProject
                               .GetTypes()
                               .Where(t => t.IsClass
                                        && t.IsPublic
                                        && t.IsSubclassOf(typeof(GraphType))
                                        && t.Namespace.StartsWith(projectNamespace, StringComparison.InvariantCultureIgnoreCase))
                               .Select(x => x.GetTypeInfo())
                               .ToList();

            foreach (var type in graphQlTypes)
            {
                services.AddTransient(type.AsType());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChuckApplicationService;
using ChuckSwapCAssessment.Controllers;
using ChuckSwapiCAssessment;
using ChuckSwapiCAssessment.API;
using ChuckSwapiCAssessment.API.GraphQL;
using ChuckSwapiCAssessment.Domain.Model;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SearchApplicationService;
using SearchApplicationService.GraphQL;
using SwapiApplicationService;

namespace ChuckSwapCAssessment.API
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
            services.AddControllers();
            services.AddHttpClient<IChuckNorrisService, ChuckNorrisService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["ChuckApiUrl"]);
            });
            services.AddHttpClient<ISwapiService, SwapiService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["SwapiApiUrl"]);
            });
            services.AddSingleton<Joke>();
            services.AddSingleton<People>();
            services.AddSingleton<JokeType>();
            services.AddSingleton<PeopleType>();
            services.AddSingleton<SearchQuery>();
            services.AddSingleton<SwapiQuery>();
            services.AddSingleton<JokeSchema>();
            services.AddSingleton<JokeQuery>();
            services.AddSingleton<PeopleQueryResultType>();
            services.AddSingleton<ISearchService,SearchService>();
            services.AddSingleton<IPeopleRepository, PeopleRepository>();
            services.AddSingleton<SearchSchema>();
            services.AddSingleton<SwapiSchema>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ChuckSwapi Assessment API",
                    Description = "An assesment for Sovtech - done by Owen",
                    //TermsOfService = new Uri("https://example.com/terms"),
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Shayne Boyer",
                    //    Email = string.Empty,
                    //    Url = new Uri("https://twitter.com/spboyer"),
                    //},
                    //License = new OpenApiLicense
                    //{
                    //    Name = "Use under LICX",
                    //    Url = new Uri("https://example.com/license"),
                    //}
                });
            });
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddLogging(builder => builder.AddConsole());
            services.AddHttpContextAccessor();

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
            })
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
            .AddSystemTextJson(
            options => { options.PropertyNamingPolicy =  SnakeCaseNamingPolicy.Instance;
                options.MaxDepth = 0;
                options.IgnoreNullValues = true;
                options.IgnoreReadOnlyProperties = true;
            }
            )
            .AddUserContextBuilder(httpContext => new GraphQLUserContext { User = httpContext.User })
            .AddDataLoader();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            //app.UseGraphiQl("/graphql");
            //app.UseGraphQL<Controllers.JockeSchema>();
            app.UseGraphQL<SearchSchema>("/api/search");
            app.UseGraphQL<SwapiSchema>("/api/swapi");
            app.UseGraphQL<JokeSchema>("/api/chuck");
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/api/search", Path = "/ui/search" });
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/api/swapi", Path = "/ui/swapi" });
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/api/chuck", Path = "/ui/chuck" });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

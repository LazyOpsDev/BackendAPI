using System;
using System.Threading;
using Autofac;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Minitwit.DataAccessLayer;
using MySql.Data.MySqlClient;
using Prometheus;
using Repository;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Minitwit.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            var elasticUri = Configuration["ElasticConfiguration:Uri"];
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true,
                })
            .CreateLogger();
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connString = Configuration["ConnectionString"];
            //connString = "Server=localhost;Database=minitwit;Uid=root;Pwd=hej123;";
            // var loggerFactory = LoggerFactory.Create(builder =>
            // {
            //     builder
            //         .AddFilter("Microsoft", LogLevel.Warning)
            //         .AddFilter("System", LogLevel.Warning)
            //         .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
            //         .AddConsole();
            // });

            // ILogger logger = loggerFactory.CreateLogger<Program>();
            // logger.LogError(connString);
            WaitForDBInit(connString);
            services.AddControllers();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITimelineRepository, TimelineRepository>();
            services.AddScoped<ILastNumberRepository, LastNumberRepository>();
            //services.AddScoped<CustomDbContext>();
            //logger.Log(LogLevel.Error, connString);
            services.AddDbContext<CustomDbContext>(o => o.UseMySql(connString));

            services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CustomDbContext context, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            // app.UseHttpsRedirection();

            context.Database.Migrate();

            app.UseRouting();
            app.UseHttpMetrics();

            //app.UseAuthorization();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(MyAllowSpecificOrigins);
                endpoints.MapMetrics();
            });

            loggerFactory.AddSerilog();
        }

        private void WaitForDBInit(string connectionString, Microsoft.Extensions.Hosting.IHostApplicationLifetime lifetime = null)
        {
            var connection = new MySqlConnection(connectionString);
            int retries = 1;
            while (retries < 7)
            {
                try
                {
                    Console.WriteLine("Connecting to db. Trial: {0}", retries);
                    connection.Open();
                    connection.Close();
                    break;
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.StackTrace);
                    Thread.Sleep((int)Math.Pow(2, retries) * 1000);
                    Console.WriteLine("retrying");
                    retries++;
                }
            }
            if (retries >= 7 && lifetime != null)
                lifetime.StopApplication();
        }
    }
}

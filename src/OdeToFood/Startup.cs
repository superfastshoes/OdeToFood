using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace OdeToFood
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(env.ContentRootPath)
                          .AddJsonFile("appsettings.json")
                          .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration);
            // Custom service
            services.AddSingleton<IGreeter, Greeter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IGreeter greeter)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                // Middleware, Letting request to flow through to following middleware, and waiting for response. Helps with debugging. Only want to show this page for developers.
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // This middleware is for the users, an error page for them
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Opps!")
                });
            }
            
            // Middleware: Look at the incoming request and see if theres a default file that matches that request.
            //app.UseDefaultFiles();

            // Middleware: Ability to use files in root folder
            //app.UseStaticFiles();

            // Combines both UseDefaultFiles(); and UseStaticFiles(); 
            app.UseFileServer();

            // **** Middleware example, most middleware are configurable by adding the options object. **** //
            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //Path = "/welcome"
            //});

            // Middleware for request the rest of the middleware didnt recognize
            //app.Run(async (context) =>
            //{
            //var message = greeter.GetGreeting();
            //await context.Response.WriteAsync(message);
            // });

            /* **** Goal of this middleware is going to look at an incoming HTTP request and try to map that request to a method on a C# class. The MVC framwork then invokes a method,
                    and that method will tell the MVC framework what to do next. **** */
            app.UseMvcWithDefaultRoute();
        }
    }
}

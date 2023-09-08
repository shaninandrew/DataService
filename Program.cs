
using System.Diagnostics;
using Microsoft.Extensions.Options;
using Swashbuckle;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;



namespace MyService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddControllers();

            builder.Services.AddControllers().ConfigureApiBehaviorOptions( options =>
            {
                // To preserve the default behavior, capture the original delegate to call later.
                var builtInFactory = options.InvalidModelStateResponseFactory;

                options.InvalidModelStateResponseFactory = context =>
                {
                    var logger = context.HttpContext.RequestServices
                                        .GetRequiredService<ILogger<Program>>();

                    // Perform logging here.
                    // ...
                    Debug.WriteLine(context.RouteData.Values.ToString());

                    // Invoke the default behavior, which produces a ValidationProblemDetails
                    // response.
                    // To produce a custom response, return a different implementation of 
                    // IActionResult instead.
                    return builtInFactory(context);

                };
            }
            );


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                /*
                 * app.UseSwagger();
                app.UseSwaggerUI();*/
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DefaultModelExpandDepth(3);
                    options.EnableDeepLinking();
                    //options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                   // options.RoutePrefix = string.Empty;
                   // options.SerializeAsV2 = true;
                   // options.SerializeAsV2 = true;
                });

                

            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            
            /*
             * if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            */

            app.Run();
        }
    }
}
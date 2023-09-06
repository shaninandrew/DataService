
using System.Diagnostics;

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
                app.UseSwagger();
                app.UseSwaggerUI();
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
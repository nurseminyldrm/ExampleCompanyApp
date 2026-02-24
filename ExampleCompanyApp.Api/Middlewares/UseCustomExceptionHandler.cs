using ExampleCompanyApp.Core.Dtos;
using ExampleCompanyApp.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ExampleCompanyApp.Api.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        //api da hata verirse yakalasın diye middleware yazdım.



        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };


                    context.Response.StatusCode = statusCode;
                    var response = CustomReponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);
                    await context.Response.WriteAsJsonAsync(JsonSerializer.Serialize(response));
                });
            });
        } 
    }
}

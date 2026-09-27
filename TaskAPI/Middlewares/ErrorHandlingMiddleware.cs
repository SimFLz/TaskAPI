using System.Net;
using System.Text.Json;

namespace TaskAPI.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var respostaDeErro = new
        {
            Status = context.Response.StatusCode,
            Mensagem = "Ocorreu um erro interno no servidor.",
            DetalheTecnico = exception.Message
        };
        var jsonResult = JsonSerializer.Serialize(respostaDeErro);
        return context.Response.WriteAsync(jsonResult);

    }
}


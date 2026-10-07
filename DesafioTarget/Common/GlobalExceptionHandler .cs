using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTarget.Common
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext context, Exception ex, CancellationToken ct)
        {
            var (status, title) = ex switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Não encontrado"),
                DomainException => (StatusCodes.Status400BadRequest, "Regra de negócio violada"),
                _ => (0, null)
            };

            if (status == 0) return false;

            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(
                new ProblemDetails { Status = status, Title = title, Detail = ex.Message }, ct);

            return true;
        }
    }
}

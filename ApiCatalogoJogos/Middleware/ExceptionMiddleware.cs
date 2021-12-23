using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Middleware
{
    public class ExceptionMiddleware  // -> Usar o middleare para pegar erros nao tratados (pode se usar algum log para rastrear erros na aplicação.
    {

        private readonly RequestDelegate next;  //-> middleware (tem que implementar o next para dar continuidade ate chegar a controller)

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context) //-> na requisição Http 
        {
            try
            {
                await next(context);                // -> tentar passar para o proximo no caso a Controller
            }
            catch
            {
                await HandleExceptionAsync(context);   //-> se houver algum problema lançar essa excessão
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context)    // -> personalizando Excessão
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new { Message = "Ocorreu um erro durante sua solicitação, por favor, tente novamente mais tarde" });
        }


    }
}

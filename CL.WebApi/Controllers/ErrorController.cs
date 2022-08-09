using CL.Core.Shared.ModelViews;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CL.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            //Recebe o Error dentro do contexto da api
            var contexto = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = contexto?.Error;


            #region [Trato o error e devolvo um erro tratado.]
            Response.StatusCode = 500;
            var idErro = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            return new ErrorResponse(idErro, HttpContext?.TraceIdentifier); 
            #endregion

        }
    }
}

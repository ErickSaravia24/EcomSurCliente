using Microsoft.AspNetCore.Mvc;

namespace WebApiUsuarios.Controller
{

    [ApiController]
    [Route("/")]

    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Ecom Sur Usuarios";
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using WebApiUsuarios.Models;
using WebApiUsuarios.Repository;

namespace WebApiUsuarios.Controller
{
    [ApiController]
    [Route("/users")]
    public class UsersController
    {
        private readonly string _connectionString;

        private readonly IUsuarioRepository _userRepository;

        public UsersController(IUsuarioRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserById(int idUsuario, int idUsuarioAdmin)
        {
            try
            {
                var user = await _userRepository.GetUsersById(idUsuario, idUsuarioAdmin);

                if (user == null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAll(int id)
        {
            try
            {
                List<Usuario> user = await _userRepository.GetUsers(id);

                if (user == null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateUsers(Usuario usuarios)
        {
            try
            {
                bool result = await _userRepository.InsertUsers(usuarios);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }
        [HttpDelete("{user}")]
        public async Task<ActionResult<bool>> DeleteUser(int id, int idAdmin)
        {
            try
            {
                bool result = await _userRepository.DeleteUsers(id, idAdmin);
                if (result)
                {
                    return new OkObjectResult(result);
                }
                else
                {

                    return new NotFoundResult();
                }   
            }
            catch (Exception ex)
            {
             return   new BadRequestObjectResult(new { error = ex.Message });
            }
        }
    }

}


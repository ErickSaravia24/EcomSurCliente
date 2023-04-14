using WebApiUsuarios.Models;

namespace WebApiUsuarios.Repository
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetUsers(int id);
        Task<Usuario> GetUsersById(int id, int idrol);
        Task<bool> InsertUsers(Usuario Usuario);
        Task<bool> UpdateUsers(Usuario Usuario);
        Task<bool> DeleteUsers(int id);
    }
}

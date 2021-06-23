using PostApi.Models.Models;
using System.Threading.Tasks;

namespace PostApi.Data.Interface
{
    public interface IUsuarioReposotorio : IRepositorioGenerico<Usuario>
    {
        Task<(bool Resultado, Usuario Usuario)> ValidarLogin(Usuario usuario);
        Task<bool> ExistUser(Usuario usuario);

    }
}

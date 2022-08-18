using E_comerceAPI.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Repositorios
{
    public interface IUsuario
    {
        Task<List<Usuario>> PegarTodosUsuariosAsync();
        Task<Usuario> PegarUsuarioPeloIdAsync(int id);
        Task NovoUsuarioAsync(Usuario usuarios);
        Task AtualizarUsuarioAsync(Usuario usuarios);
        Task DeletarUsuarioAsync(int id);
        Task PegarUsuarioPeloEmailAsync(string email);
    }
}

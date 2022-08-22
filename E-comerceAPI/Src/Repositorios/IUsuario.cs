using E_comerceAPI.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Repositorios
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de usuario </para>
    /// <para>Criado por: Grupo 4</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 22/08/2022</para>
    /// </summary>

    public interface IUsuario
    {
        Task<List<Usuario>> PegarTodosUsuariosAsync();
        Task<Usuario> PegarUsuarioPeloIdAsync(int id);
        Task NovoUsuarioAsync(Usuario usuarios);
        Task AtualizarUsuarioAsync(Usuario usuarios);
        Task DeletarUsuarioAsync(int id);
        Task<Usuario> PegarUsuarioPeloEmailAsync(string email);
    }
}

using E_comerceAPI.Src.Modelos;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Repositorios
{
    /// <summary>
    /// <para>Resumo: Interface Responsavel por representar ações de autenticação</para>
    /// <para>Criado por: Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(Usuario usuario);
        string GerarToken(Usuario usuario);
    }
}

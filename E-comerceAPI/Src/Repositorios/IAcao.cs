using E_comerceAPI.Src.Modelos;
using E_comerceAPI.Src.Utilidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Repositorios
{
    public interface IAcao
    {
        Task<List<Acao>> CarregarMinhasAcoesAsync(int idUsuario);
        Task NovaAcaoAsync(Acao acao);
        Task AtualizarStatusAcaoAsync(int idAcao, StatusAcao novoStatus);
    }
}

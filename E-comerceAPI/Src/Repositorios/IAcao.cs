using E_comerceAPI.Src.Modelos;
using E_comerceAPI.Src.Utilidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Repositorios
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de Ações </para>
    /// <para>Criado por: Grupo 4</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 22/08/2022</para>
    /// </summary>
    
    public interface IAcao
    {
        Task<List<Acao>> CarregarMinhasAcoesAsync(int idUsuario);
        Task NovaAcaoAsync(Acao acao);
        Task AtualizarStatusAcaoAsync(int idAcao, StatusAcao novoStatus);
    }
}

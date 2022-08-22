using E_comerceAPI.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Repositorios
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de Produtos </para>
    /// <para>Criado por: Grupo 4</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 22/08/2022</para>
    /// </summary>
    
    public interface IProduto
    {
        Task<List<Produto>> PegarTodosProdutosAsync();
        Task<Produto> PegarProdutoPeloIdAsync(int id);
        Task<List<Produto>> CarregarMeusProdutosEmpresaAsync(int idUsuario);
        Task NovoProdutoAsync(Produto produtos);
        Task AtualizarProdutoAsync(Produto produtos);
        Task DeletarProdutoAsync(int id);
    }
}

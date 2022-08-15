using E_comerceAPI.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Repositorios
{
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

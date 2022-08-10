using E_comerceAPI.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Repositorios
{
    public interface IProduto
    {
        Task<List<Produtos>> PegarTodosProdutosAsync();
        Task<Produtos> PegarProdutoPeloIdAsync(int id);
        Task NovoProdutoAsync(Produtos produtos);
        Task AtualizarProdutoAsync(Produtos produtos);
        Task DeletarProdutoAsync(int id);
    }
}

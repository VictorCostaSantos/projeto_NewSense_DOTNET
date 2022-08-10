using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_comerceAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;

namespace E_comerceAPI.Src.Repositorios.Implementacoes
{
    public class ProdutoRepositorio : IProduto
    {
        #region Atributos

        private readonly EcomerceContexto _contextos;
        #endregion Atributos
        #region Construtores
        public ProdutoRepositorio(EcomerceContexto contextos)
        {
            _contextos = contextos;
        }
           
        #endregion Construtores
        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos produtos</para>
        /// </summary>
        /// <return>Lista ProdutoModelo></return>
        public async Task<List<Produtos>> PegarTodosProdutosAsync()
        {
             return await _contextos.Produtos.ToListAsync();
        }
        public async Task<Produtos> PegarProdutoPeloIdAsync(int id)
        {
            if(!ExisteId(id)) throw new Exception("Id do produto não encontrado");
            return await _contextos.Produtos.FirstOrDefaultAsync(p => p.Id == id);

            // funções auxiliares
            bool ExisteId(int id)
            {
                var auxiliar = _contextos.Produtos.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }

        }
        public async Task NovoProdutoAsync(Produtos produtos)
        {
            await _contextos.Produtos.AddAsync(
            new Produtos
            {
                Descricao = produtos.Descricao
            });
            await _contextos.SaveChangesAsync();
        }

        public async Task AtualizarProdutoAsync(Produtos produtos)
        {
            var produtosExistente = await PegarProdutoPeloIdAsync(produtos.Id);
            produtosExistente.Descricao = produtos.Descricao;
            _contextos.Produtos.Update(produtosExistente);
            await _contextos.SaveChangesAsync();
        }

        public async Task DeletarProdutoAsync(int id)
        {
            _contextos.Produtos.Remove(await PegarProdutoPeloIdAsync(id));
            await _contextos.SaveChangesAsync();

        }

        #endregion Métodos
    }
}


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
                var auxiliar = _contextos.Produtos.FirstOrDefault(p => p.Id == id);
                return auxiliar != null;
            }

        }

        public async Task NovoProdutoAsync(Produtos produtos)
        {
            if (await ExisteProduto(produtos.Produto)) throw new Exception("Produto já existente no sistema!");
            if (await ExisteDescricao(produtos.Descricao)) throw new Exception("Descrição já existente no sistema!");

            await _contextos.Produtos.AddAsync(new Produtos
            {
                Produto = produtos.Produto,
                Descricao = produtos.Descricao,
                QtdProduto = produtos.QtdProduto,
                QtdLimite = produtos.QtdLimite,
                URL_Imagem =produtos.URL_Imagem
            });
            await _contextos.SaveChangesAsync();
        }

        public async Task AtualizarProdutoAsync(Produtos produtos)
        {
            if (await ExisteProduto(produtos.Produto)) throw new Exception("Produto já existente no sistema!");
            if (await ExisteDescricao(produtos.Descricao)) throw new Exception("Descrição já existente no sistema!");

            var auxiliar = await PegarProdutoPeloIdAsync(produtos.Id);
            auxiliar.Produto = produtos.Produto;
            auxiliar.Descricao = produtos.Descricao;
            auxiliar.QtdProduto = produtos.QtdProduto;
            auxiliar.QtdLimite = produtos.QtdLimite;
            auxiliar.URL_Imagem = produtos.URL_Imagem;
            _contextos.Produtos.Update(auxiliar);
            await _contextos.SaveChangesAsync();

        }

        public async Task DeletarProdutoAsync(int id)
        {
            _contextos.Produtos.Remove(await PegarProdutoPeloIdAsync(id));
            await _contextos.SaveChangesAsync();

        }

        private async Task<bool> ExisteProduto(string produto)
        {
            var auxiliar = await _contextos.Produtos.FirstOrDefaultAsync(p => p.Produto == produto);

            return auxiliar != null;
        }

        private async Task<bool> ExisteDescricao(string descricao)
        {
            var auxiliar = await _contextos.Produtos.FirstOrDefaultAsync(p => p.Descricao == descricao);

            return auxiliar != null;
        }


        #endregion Métodos
    }
}


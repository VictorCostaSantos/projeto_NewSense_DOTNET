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
        public async Task<List<Produto>> PegarTodosProdutosAsync()
        {
             return await _contextos.Produtos.ToListAsync();
        }

        public async Task<Produto> PegarProdutoPeloIdAsync(int id)
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

        public async Task NovoProdutoAsync(Produto produtos)
        {
            if (await ExisteProduto(produtos.Titulo)) throw new Exception("Produto já existente no sistema!");
            if (await ExisteDescricao(produtos.Descricao)) throw new Exception("Descrição já existente no sistema!");

            await _contextos.Produtos.AddAsync(new Produto
            {
                Titulo = produtos.Titulo,
                Descricao = produtos.Descricao,
                QtdProduto = produtos.QtdProduto,
                QtdLimite = produtos.QtdLimite,
                URL_Imagem =produtos.URL_Imagem
            });
            await _contextos.SaveChangesAsync();
        }

        public async Task AtualizarProdutoAsync(Produto produto)
        {
            if (await ExisteProduto(produto.Titulo)) throw new Exception("Produto já existente no sistema!");
            if (await ExisteDescricao(produto.Descricao)) throw new Exception("Descrição já existente no sistema!");

            var auxiliar = await PegarProdutoPeloIdAsync(produto.Id);
            auxiliar.Titulo = produto.Titulo;
            auxiliar.Descricao = produto.Descricao;
            auxiliar.QtdProduto = produto.QtdProduto;
            auxiliar.QtdLimite = produto.QtdLimite;
            auxiliar.URL_Imagem = produto.URL_Imagem;
            _contextos.Produtos.Update(auxiliar);
            await _contextos.SaveChangesAsync();

        }

        public async Task DeletarProdutoAsync(int id)
        {
            _contextos.Produtos.Remove(await PegarProdutoPeloIdAsync(id));
            await _contextos.SaveChangesAsync();

        }

        private async Task<bool> ExisteProduto(string titulo)
        {
            var auxiliar = await _contextos.Produtos.FirstOrDefaultAsync(p => p.Titulo == titulo);

            return auxiliar != null;
        }

        private async Task<bool> ExisteDescricao(string descricao)
        {
            var auxiliar = await _contextos.Produtos.FirstOrDefaultAsync(p => p.Descricao == descricao);

            return auxiliar != null;
        }

        public async Task<List<Produto>> CarregarMeusProdutosEmpresaAsync(int idUsuario)
        {
            if (!ExisteUsuario(idUsuario)) throw new Exception("Id de usuario não existe!");

            return await _contextos.Produtos
                .Include(p => p.Criador)
                .Where(p => p.Criador.Id == idUsuario)
                .ToListAsync();

            // Funções auxiliares
            bool ExisteUsuario(int idUsuario)
            {
                var aux = _contextos.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
                return aux != null;
            }
        }

        #endregion Métodos

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_comerceAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;

namespace E_comerceAPI.Src.Repositorios.Implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IProduto</para>
    /// <para>Criado por: Grupo 4</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 22/08/2022</para>
    /// </summary>
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
        /// <return>Lista Produto></return>
        public async Task<List<Produto>> PegarTodosProdutosAsync()
        {
             return await _contextos
                .Produtos
                .Include(p => p.Criador)
                .Include(p => p.Acoes)
                .ToListAsync();
 
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar uma produto pelo Id</para>
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <return>Produto</return>
        /// <exception cref="Exception">Id do produto não pode ser nulo</exception>
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

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar novo produto</para>
        /// </summary>
        /// <param> 'name="produtos">Construtor para cadastrar produto</param>
        /// <exception cref="Exception">Produto já existe</exception>
        /// <exception cref="Exception">Descrição já existe</exception>
        public async Task NovoProdutoAsync(Produto produtos)
        {
            await _contextos.Produtos.AddAsync(new Produto
            {
                Titulo = produtos.Titulo,
                Descricao = produtos.Descricao,
                QtdProduto = produtos.QtdProduto,
                QtdLimite = produtos.QtdLimite,
                URL_Imagem =produtos.URL_Imagem,
                Criador = _contextos.Usuarios.FirstOrDefault(u => u.Id == produtos.Criador.Id)
            });
            await _contextos.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar produto</para>
        /// </summary>
        /// <param> 'name="produtos">Construtor para atualizar produto</param>
        /// <exception cref="Exception">Produto já existe</exception>
        /// <exception cref="Exception">Descrição já existe</exception>
        public async Task AtualizarProdutoAsync(Produto produto)
        {
            var auxiliar = await PegarProdutoPeloIdAsync(produto.Id);
            auxiliar.Titulo = produto.Titulo;
            auxiliar.Descricao = produto.Descricao;
            auxiliar.QtdProduto = produto.QtdProduto;
            auxiliar.QtdLimite = produto.QtdLimite;
            auxiliar.URL_Imagem = produto.URL_Imagem;
            _contextos.Produtos.Update(auxiliar);
            await _contextos.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um produto</para>
        /// </summary>
        /// <param name="id">Id do produto</param>
        public async Task DeletarProdutoAsync(int id)
        {
            _contextos.Produtos.Remove(await PegarProdutoPeloIdAsync(id));
            await _contextos.SaveChangesAsync();

        }

        // Funções auxiliares
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

        /// <summary>
        /// <para>Resumo: Método assíncrono carregar produtos</para>
        /// </summary>
        /// <param> 'name="idUsuario">Id do usuário</param>
        /// <return>Lista de produtos pelo Id do usuario</return>
        /// <exception cref="Exception">Id do usuario não pode ser nulo</exception>
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


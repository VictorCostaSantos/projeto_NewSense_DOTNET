using E_comerceAPI.Src.Modelos;
using E_comerceAPI.Src.Utilidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Repositorios.Implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IAcao</para>
    /// <para>Criado por: Grupo 4</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 22/08/2022</para>
    /// </summary>
    public class AcaoRepositorio : IAcao
    {
        #region Atributos

        private readonly EcomerceContexto _contextos;

        #endregion Atributos

        #region Construtores

        public AcaoRepositorio(EcomerceContexto contextos)
        {
            _contextos = contextos;
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar atualizar status da ação</para>
        /// </summary>
        /// <param> 'name="novoStatus">novo status</param>
        /// <return>Status da Ação</return>
        /// <exception cref="Exception">Id da ação pode ser nulo</exception>
        public async Task AtualizarStatusAcaoAsync(int idAcao, StatusAcao novoStatus)
        {
            var acao = _contextos.Acoes.FirstOrDefault(a => a.Id == idAcao);

            if (acao != null)
            {
                acao.Status = novoStatus;
                _contextos.Acoes.Update(acao);
                await _contextos.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Id da ação não existe!");
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para carregar minhas açõeso</para>
        /// </summary>
        /// <param> 'name="idUsuario">Id do usuário</param>
        /// <return>Lista Ações</return>
        /// <exception cref="Exception">Id de usuario não pode ser nulo</exception>
        public async Task<List<Acao>> CarregarMinhasAcoesAsync(int idUsuario)
        {
            var usuario = _contextos.Usuarios.FirstOrDefault(u => u.Id == idUsuario);

            if (usuario != null)
            {
                switch (usuario.Condicao)
                {
                    case CondicaoUsuario.DOADOR:
                        return await _contextos.Acoes
                            .Include(a => a.Produto)
                            .Include(a => a.Ong)
                            .Where(a => a.Produto.Criador.Id == idUsuario)
                            .ToListAsync();
                    default:
                        return await _contextos.Acoes
                            .Include(a => a.Produto)
                            .Include(a => a.Produto.Criador)
                            .Where(a => a.Ong.Id == idUsuario)
                            .ToListAsync();
                }
            }
            else
            {
                throw new Exception("Id de Usuario não existe!");
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova ação</para>
        /// </summary>
        /// <param> 'name="acao">Construtor para salvar ação</param>
        /// <exception cref="Exception">Id de ONG não pode ser nulo</exception>
        /// <exception cref="Exception">Id do produto não pode ser nulo</exception>
        public async Task NovaAcaoAsync(Acao acao)
        {
            if (!ExisteUsuario(acao.Ong.Id)) throw new Exception("Id de ONG não existe!");

            if (!ExisteProduto(acao.Produto.Id)) throw new Exception("Id do produto não existe!");

            await _contextos.Acoes.AddAsync(new Acao
            {
                DataAcao = DateTime.Now,
                QtdAcao = acao.QtdAcao,
                Status = StatusAcao.PENDENTE,
                Ong = _contextos.Usuarios.FirstOrDefault(u => u.Id == acao.Ong.Id),
                Produto = _contextos.Produtos.FirstOrDefault(p => p.Id == acao.Produto.Id)
            });
            await _contextos.SaveChangesAsync();
        }

        // Funções auxiliares
        bool ExisteUsuario(int idUsuario)
        {
            var aux = _contextos.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            return aux != null;
        }

        bool ExisteProduto(int idProduto)
        {
            var aux = _contextos.Produtos.FirstOrDefault(u => u.Id == idProduto);
            return aux != null;
        }

        #endregion Métodos

    }
}
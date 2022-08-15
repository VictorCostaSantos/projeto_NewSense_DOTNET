using E_comerceAPI.Src.Modelos;
using E_comerceAPI.Src.Utilidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Repositorios.Implementacoes
{
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
                            .Where(a => a.Produto.Criador.Id == idUsuario)
                            .ToListAsync();
                    default:
                        return await _contextos.Acoes
                            .Include(a => a.Produto)
                            .Where(a => a.Ong.Id == idUsuario)
                            .ToListAsync();
                }
            }
            else
            {
                throw new Exception("Id de Usuario não existe!");
            }

        }

        public async Task NovaAcaoAsync(Acao acao)
        {
            if (!ExisteUsuario(acao.Ong.Id)) throw new Exception("Id de ONG não existe!");

            if (!ExisteProduto(acao.Produto.Id)) throw new Exception("Id do produto não existe!");

            await _contextos.Acoes.AddAsync(new Acao
            {
                DataAcao = DateTime.Now,
                QtdAcao = acao.QtdAcao,
                Status = StatusAcao.Pendente,
                Ong = _contextos.Usuarios.FirstOrDefault(u => u.Id == acao.Ong.Id),
                Produto = _contextos.Produtos.FirstOrDefault(p => p.Id == acao.Produto.Id)
            });
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
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

        public async Task NovaAcaoAsync(Acao acao)
        {
            await _contextos.Acao.AddAsync(new Acao
            {
                DataAcao = DateTime.Now,
                QtdAcao = acao.QtdAcao,
                Status = StatusAcao.Pendente,
                Ong = _contextos.Usuarios.FirstOrDefault(u => u.Id == acao.Ong.Id),
                Produto = _contextos.Produtos.FirstOrDefault(p => p.Id == acao.Produto.Id)

            });

        }

        #endregion Métodos

    }
}
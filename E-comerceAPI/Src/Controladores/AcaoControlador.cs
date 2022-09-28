using E_comerceAPI.Src.Modelos;
using E_comerceAPI.Src.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using E_comerceAPI.Src.Utilidades;

namespace E_comerceAPI.Src.Controladores
{
    [ApiController]
    [Route("api/Acao")]
    [Produces("application/json")]
    public class AcaoControlador : ControllerBase
    {
        #region Atributos

        private readonly IAcao _repositorio;

        #endregion

        #region Construtores
        public AcaoControlador(IAcao repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Carregar ações do usuário 
        /// </summary>
        /// <param name="idUsuario">Id do usuario</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna as ações</response>
        /// <response code="204">Ações não existentes</response>
        [HttpGet]
        public async Task<ActionResult> CarregarMinhasAcoesAsync(int idUsuario)
        {
            var lista = await _repositorio.CarregarMinhasAcoesAsync(idUsuario);
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Criar nova Ação(doação, solicitação)
        /// </summary>
        /// <param name="acao">Construtor para criar ação</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Acao
        /// {
        /// "Dataacao": "Data em que ação foi criada",
        /// "QtdAcao": "1",
        /// "StatusAcao": "PENDENTE",
        /// "Ong": { 
        ///     "Id": N
        /// },
        /// "Produto": {
        ///     "Id": N
        /// }
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna ação criada</response>
        /// <response code="400">Ação não foi criada</response>
        [HttpPost]
        public async Task<ActionResult> NovaAcaoAsync([FromBody] Acao acao)
        {
            try
            {
                await _repositorio.NovaAcaoAsync(acao);
                return Created($"api/Acao", acao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }


        /// <summary>
        /// Atualizar Status da Ação
        /// </summary>
        /// <param name="idAcao">Busca Id da Ação</param>
        /// <param name="novoStatus">Busca o novo Status da Ação no Enum</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna atualização efetuada com sucesso</response>
        /// <response code="400">Retorna atualização não foi efetuada</response>
        [HttpPut]
        public async Task<ActionResult> AtualizarStatusAcaoAsync([FromBody] int idAcao, StatusAcao novoStatus)
        {
            try
            {
                await _repositorio.AtualizarStatusAcaoAsync(idAcao, novoStatus);
                return Ok((idAcao, novoStatus));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        #endregion

    }
}
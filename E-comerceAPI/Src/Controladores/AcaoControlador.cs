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

        [HttpGet]
        public async Task<ActionResult> CarregarMinhasAcoesAsync(int idUsuario)
        {
            var lista = await _repositorio.CarregarMinhasAcoesAsync(idUsuario);
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

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
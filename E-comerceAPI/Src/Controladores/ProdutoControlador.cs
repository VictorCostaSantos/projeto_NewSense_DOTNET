using E_comerceAPI.Src.Modelos;
using E_comerceAPI.Src.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Controladores
{
    [ApiController]
    [Route("api/Produtos")]
    [Produces("application/json")]

    public class ProdutosControlador : ControllerBase
    {
        #region Atributos
        private readonly IProduto _repositorio;
        #endregion

        #region Construtores
        public ProdutosControlador(IProduto repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion

        #region Métodos

        [HttpGet]
        public async Task<ActionResult> PegarTodosProdutosAsync()
        {
            var lista = await _repositorio.PegarTodosProdutosAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        [HttpGet("id/{idProduto}")]
        public async Task<ActionResult> PegarProdutoPeloIdAsync([FromRoute] int idProduto)
        {
            try
            {
                return Ok(await _repositorio.PegarProdutoPeloIdAsync(idProduto));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> NovoProdutoAsync([FromBody] Produtos produtos)
        {
            try
            {
                await _repositorio.NovoProdutoAsync(produtos);
                return Created($"api/Produtos", produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarProduto([FromBody] Produtos produtos)
        {
            try
            {
                await _repositorio.AtualizarProdutoAsync(produtos);
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpDelete("id/{idProduto}")]
        public async Task<ActionResult> DeletarProduto([FromRoute] int idProduto)
        {
            try
            {
                await _repositorio.DeletarProdutoAsync(idProduto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        #endregion
    }

}

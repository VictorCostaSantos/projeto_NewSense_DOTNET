using E_comerceAPI.Src.Modelos;
using E_comerceAPI.Src.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Controladores
{
    public class UsuarioControlador
    {
        [ApiController]
        [Route("api/Usuarios")]
        [Produces("application/json")]

        public class UsuariosControlador : ControllerBase
        {
            #region Atributos

            private readonly IUsuario _repositorio;

            #endregion

            #region Construtores

            public UsuariosControlador(IUsuario repositorio)
            {
                _repositorio = repositorio;
            }
            #endregion

            #region Métodos

            [HttpGet]
            public async Task<ActionResult> PegarTodosUsuariosAsync()
            {
                var lista = await _repositorio.PegarTodosUsuariosAsync();
                if (lista.Count < 1) return NoContent();
                return Ok(lista);
            }

            [HttpGet("id/{idUsuario}")]
            public async Task<ActionResult> PegarUsuarioPeloIdAsync([FromRoute] int idUsuario)
            {
                try
                {
                    return Ok(await _repositorio.PegarUsuarioPeloIdAsync(idUsuario));
                }
                catch (Exception ex)
                {
                    return NotFound(new { Mensagem = ex.Message });
                }
            }
            [HttpPost]
            public async Task<ActionResult> NovoUsuarioAsync([FromBody] Usuario usuarios)
            {
                try
                {
                    await _repositorio.NovoUsuarioAsync(usuarios);
                    return Created($"api/Usuarios", usuarios);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Mensagem = ex.Message });
                }
            }
            [HttpPut]
            public async Task<ActionResult> AtualizarUsuario([FromBody] Usuario usuarios)
            {
                try
                {
                    await _repositorio.AtualizarUsuarioAsync(usuarios);
                    return Ok(usuarios);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Mensagem = ex.Message });
                }
            }
            [HttpDelete("id/{idUsuarios}")]
            public async Task<ActionResult> DeletarUsuario([FromRoute] int idUsuario)
            {
                try
                {
                    await _repositorio.DeletarUsuarioAsync(idUsuario);
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
}
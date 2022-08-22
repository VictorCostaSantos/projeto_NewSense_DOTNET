using E_comerceAPI.Src.Modelos;
using E_comerceAPI.Src.Repositorios;
using E_comerceAPI.Src.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Controladores
{
    [ApiController]
    [Route("api/Usuarios")]
    [Produces("application/json")]

        public class UsuariosControlador : ControllerBase
        {
            #region Atributos

            private readonly IUsuario _repositorio;
            private readonly IAutenticacao _servicos;

            #endregion

            #region Construtores

            public UsuariosControlador(IUsuario repositorio, IAutenticacao servicos)
            {
                _repositorio = repositorio;
                _servicos = servicos;
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
            [HttpPost("cadastrar")]
            [AllowAnonymous]
            public async Task<ActionResult> NovoUsuarioAsync([FromBody] Usuario usuario)
            {
                try
                {
                    await _servicos.CriarUsuarioSemDuplicarAsync(usuario);
                    return Created($"api/Usuarios/email/{usuario.Email}", usuario);
                }
                catch (Exception ex)
                {
                    return Unauthorized(ex.Message);
                }
            }

            
            [HttpPost("logar")]
            [AllowAnonymous]
            public async Task<ActionResult> LogarAsync([FromBody] Usuario usuario)
            {
                var auxiliar = await _repositorio.PegarUsuarioPeloEmailAsync(usuario.Email);
                if (auxiliar == null) return Unauthorized(new
                {
                    Mensagem = "E-mail invalido"
                });
                if (auxiliar.Senha != _servicos.CodificarSenha(usuario.Senha))
                    return Unauthorized(new { Mensagem = "Senha invalida" });
                var token = "Bearer " + _servicos.GerarToken(auxiliar);
                return Ok(new { Usuario = auxiliar, Token = token });
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
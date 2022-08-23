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

        /// <summary>
        /// Pegar todos usuarios
        /// </summary>
        /// <param name="usuarios">Busca todos usuarios</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna usuarios</response>
        /// <response code="404">Usuarios não existem</response>
        [HttpGet]
        public async Task<ActionResult> PegarTodosUsuariosAsync()
        {
            var lista = await _repositorio.PegarTodosUsuariosAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar usuario pelo Id
        /// </summary>
        /// <param name="idUsuario">Busca de usuario pelo Id</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Email não existente</response>
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

        /// <summary>
        /// Criar novo Usuario
        /// </summary>
        /// <param name="usuario">Construtor para criar usuario</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Usuarios
        /// {
        /// "nome": "Nome Sobrenome",
        /// "email": "usuario@domain.com",
        /// "senha": "134652",
        /// "endereco": "Rua do usuario, 100",
        /// "documento": "44444444444",
        /// "tipo": "NORMAL",
        /// "condicao": "DOADOR"
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="401">E-mail ja cadastrado</response>
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

        /// <summary>
        /// Pegar Autorização
        /// </summary>
        /// <param name="usuario">Construtor para logar usuario</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Usuarios/logar
        /// {
        /// "email": "usuario@domain.com",
        /// "senha": "134652"
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="401">E-mail ou senha invalido</response>
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


        /// <summary>
        /// Atualizar usuario pelo Id
        /// </summary>
        /// <param name="usuarios">Construtor para criar usuario</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// PUT /api/Usuarios
        /// {
        /// "nome": "Nome Sobrenome",
        /// "email": "usuario@domain.com",
        /// "senha": "134652",
        /// "endereco": "Rua do usuario, 100",
        /// "documento": "44444444444",
        /// "tipo": "NORMAL",
        /// "condicao": "DOADOR"
        /// }
        ///
        /// </remarks>
        /// <response code="200">Retorna usuario atualizado</response>
        /// <response code="400">Usuario nao localizado</response>
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

        /// <summary>
        /// Deleta usuario pelo Id
        /// </summary>
        /// <param name="usuario">Deleta o usuario pelo Id</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Retorna o usuario</response>
        /// <response code="404">Email não existente</response>
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
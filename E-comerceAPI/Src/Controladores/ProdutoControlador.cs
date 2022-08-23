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

        /// <summary>
        /// Pegar Todos os Produtos
        /// </summary>
        /// <param name="produtos">Todos os Produtos</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Todos produtos</response>
        /// <response code="404">Produtos não encotrados</response>
        [HttpGet]
        public async Task<ActionResult> PegarTodosProdutosAsync()
        {
            var lista = await _repositorio.PegarTodosProdutosAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar Produto Pelo Id
        /// </summary>
        /// <param name="idProduto">Id do Produto</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna produto pelo Id</response>
        /// <response code="404">Id do produto não pode ser nulo</response>
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

        /// <summary>
        /// Criar novo Produto
        /// </summary>
        /// <param name="produtos">Construtor para criar produto</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Produtos
        /// {
        /// "titulo": "Titulo produto",
        /// "descricao": "Descricao do produto",
        /// "qtdproduto": 100,
        /// "qtdlimite": 10,
        /// "url_imagem": "URLIMAGEM",
        /// "criador": { 
        ///     "Id": n°
        /// }
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna produto criado</response>
        /// <response code="400">Produto nao cadastrado</response>
        [HttpPost]
        public async Task<ActionResult> NovoProdutoAsync([FromBody] Produto produtos)
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

        /// <summary>
        /// Atualizar produto
        /// </summary>
        /// <param name="produtos">Construtor para atualizar produto</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// PUT /api/Produtos
        /// {
        /// "id": n°,
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
        /// <response code="200">Retorna produto atualizado</response>
        /// <response code="400">Produto nao localizado</response>
        [HttpPut]
        public async Task<ActionResult> AtualizarProduto([FromBody] Produto produtos)
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

        /// <summary>
        /// Deletar Produto
        /// </summary>
        /// <param name="idProduto">Deletar produto pelo Id</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// DELETE /api/Produtos
        /// {
        /// "Id": "valor do Id",
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna confirmação de produto deletado</response>
        /// <response code="401">Id não encontrado</response>
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

        /// <summary>
        /// Carregar produtos pelo Id
        /// </summary>
        /// <param name="idUsuario">Carregar meus produtos pelo Id</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna lista de produtos pelo Id do usuario</response>
        /// <response code="404">Id não existente</response>
        [HttpGet("id/empresas/{idUsuario}")]
        public async Task<ActionResult> CarregarMeusProdutosEmpresaAsync(int idUsuario)
        {
            try
            {
                var list = await _repositorio.CarregarMeusProdutosEmpresaAsync(idUsuario);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        #endregion

    }
}
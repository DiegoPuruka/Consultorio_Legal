using CL.Core.Domain;
using CL.Core.Shared.ModelViews;
using CL.Manager.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerilogTimings;
using System;
using System.Threading.Tasks;

namespace CL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteManager clienteManager;
        private readonly ILogger<ClientesController> logger;

        public ClientesController(IClienteManager clienteManager, ILogger<ClientesController> logger)
        {
            this.clienteManager = clienteManager;
            this.logger = logger;
        }

        /// <summary>
        /// Retorna todos clientes cadastrado na base.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("pegar todos os cliente Get");
            using (Operation.Time("Tempo de pegar todos os cliente."))
            {
                return Ok(await clienteManager.GetClientesAsync());
            }

        }

        /// <summary>
        /// Retorna um cliente consultado pelo id.
        /// </summary>
        /// <param name="id" example="123">Id do cliente.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            logger.LogInformation("Objeto recebido id: {@id}", id);
            using (Operation.Time("Tempo de pegar um cliente por id."))
            {
                return Ok(await clienteManager.GetClienteAsync(id));
            }
        }

        /// <summary>
        /// Insere um novo cliente
        /// </summary>
        /// <param name="novocliente"></param>
        [HttpPost]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] NovoCliente novocliente)
        {
            logger.LogInformation("Objeto recebido NovoCliente: {@novocliente}", novocliente);

            Cliente clienteInserido;
            using (Operation.Time("Tempo de adição de um novo cliente."))
            {
                logger.LogInformation("Foi requisitada a inserção de um novo cliente.");

                clienteInserido = await clienteManager.InsertClienteAsync(novocliente);

                return CreatedAtAction(nameof(Get), new { id = clienteInserido.Id }, clienteInserido);
            }
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="alteraCliente"></param>
        [HttpPut]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] AlteraCliente alteraCliente)
        {
            logger.LogInformation("Objeto recebido AlteraCliente: {@alteraCliente}", alteraCliente);

            using (Operation.Time("Tempo de alteração de um cliente."))
            {
                var clienteAtualizado = await clienteManager.UpdateClienteAsync(alteraCliente);

                if (clienteAtualizado == null)
                {
                    return NotFound(clienteAtualizado);
                }

                return Ok(clienteAtualizado);
            }
        }

        /// <summary>
        /// Exclui um cliente.
        /// </summary>
        /// <param name="id" example="123">Id do cliente.</param>
        /// <remarks>Ao excluir um cliente o mesmo será removido permanentemente da base.</remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("Objeto recebido cliente: {@id}", id);

            using (Operation.Time("Tempo de deleção de um cliente."))
            {
                await clienteManager.DeleteClienteAsync(id);

                return NoContent();
            }
        }
    }
}

using AutoMapper;
using CL.Core.Domain;
using CL.Core.Shared.ModelViews;
using CL.Manager.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CL.Manager.Implementation
{
    public class ClienteManager : IClienteManager
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public ClienteManager(IClienteRepository clienteRepository, IMapper mapper, ILogger<ClienteManager> logger)
        {
            this.clienteRepository = clienteRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            return await clienteRepository.GetClientesAsync();
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            return await clienteRepository.GetClienteAsync(id);
        }

        public async Task DeleteClienteAsync(int id)
        {
            await clienteRepository.DeleteClienteAsync(id);
        }

        public async Task<Cliente> InsertClienteAsync(NovoCliente novocliente)
        {
            logger.LogInformation("Chamada de negócio para inserir um cliente.");
            var cliente = mapper.Map<Cliente>(novocliente);
            return await clienteRepository.InsertClienteAsync(cliente);
        }

        public async Task<Cliente> UpdateClienteAsync(AlteraCliente alteraCliente)
        {
            var cliente = mapper.Map<Cliente>(alteraCliente);
            return await clienteRepository.UpdateClienteAsync(cliente);
        }
    }
}

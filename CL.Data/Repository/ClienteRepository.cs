using CL.Core.Domain;
using CL.Core.Shared.ModelViews;
using CL.Data.Context;
using CL.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CL.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly CLContext context;
        public ClienteRepository(CLContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            return await context.Clientes
                .Include(p => p.Endereco)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            //return await context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            return await context.Clientes
                .Include(p => p.Endereco)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        //Insert
        public async Task<Cliente> InsertClienteAsync(Cliente cliente)
        {
            await context.Clientes.AddAsync(cliente);
            await context.SaveChangesAsync();
            return cliente;
        }

        //Update
        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            var clienteConsultado = await context.Clientes.FindAsync(cliente.Id);

            if (clienteConsultado == null)
            {
                return clienteConsultado;
            }

            context.Entry(clienteConsultado).CurrentValues.SetValues(cliente);

            await context.SaveChangesAsync();

            return clienteConsultado;
        }

        //Delete
        public async Task DeleteClienteAsync(int id)
        {
            var clienteConsultado = await context.Clientes.FindAsync(id);

            context.Clientes.Remove(clienteConsultado);

            await context.SaveChangesAsync();
        }


    }
}

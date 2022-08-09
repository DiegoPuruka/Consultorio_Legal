using CL.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CL.Data.Context
{
    public class CLContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public CLContext(DbContextOptions options) : base(options)
        {
        }
    }
}

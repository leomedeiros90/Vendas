using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Vendas.Models;

namespace Vendas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbCliente> TbCliente { get; set; }
        public virtual DbSet<TbProduto> TbProduto { get; set; }
        public virtual DbSet<TbVenda> TbVenda { get; set; }
    }
}

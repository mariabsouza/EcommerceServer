using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Context
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Pedido_item> Pedidos_itens { get; set; }
    }
}

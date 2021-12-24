using ApiCatalogoJogos.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoJogos.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  // -> criar o contrutor da classe que passa as opçoes do DB (para o contrutor da classe Base)
        {

        }



        protected override void OnModelCreating(ModelBuilder builder) //-> para configurar os relacionamentos criar esse metodo
        {
        }



        public DbSet<Jogo> Jogo { get; set; }   // -> qual classe vai ser Mapeada e virar tabela (similar ao @Entity Java)



    }
}

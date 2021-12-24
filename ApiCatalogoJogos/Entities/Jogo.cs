using System;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoJogos.Entities
{
    public class Jogo
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Produtora { get; set; }
        public double Preco { get; set; }


    }
}

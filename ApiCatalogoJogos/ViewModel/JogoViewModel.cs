using System;

namespace ApiCatalogoJogos.ViewModel
{
    public class JogoViewModel
    {

        public Guid Id { get; set; }  //-> tipo GUID é um número inteiro de 128 bits (16 bytes) que você pode usar em todos os computadores e redes sempre que um identificador exclusivo for necessário.
        public string Nome { get; set; }
        public string Produtora { get; set; }
        public double Preco { get; set; }


    }
}

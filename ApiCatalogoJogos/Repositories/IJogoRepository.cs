using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public interface IJogoRepository : IDisposable // ->  IDisposable Ter a capacidade de é liberar recursos não gerenciados (liberar memoria não usada de forma manual)
    {

        Task<List<Jogo>> Obter(int pagina, int quantidade);
        Task<Jogo> Obter(Guid id);
        Task<List<Jogo>> Obter(string nome, string produtora);
        Task Inserir(Jogo jogo);
        Task Atualizar(Jogo jogo);
        Task Remover(Guid id);

    }
}

using GestaoFornecedores.Business.Interfaces.Services;
using GestaoFornecedores.Business.Models;
using System;
using System.Threading.Tasks;

namespace GestaoFornecedores.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        public Task Adicionar(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

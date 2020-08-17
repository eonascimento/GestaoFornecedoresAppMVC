using GestaoFornecedores.Business.Interfaces.Services;
using GestaoFornecedores.Business.Models;
using System;
using System.Threading.Tasks;

namespace GestaoFornecedores.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        public Task Adicionar(Fornecedor fornecedor)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Fornecedor fornecedor)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

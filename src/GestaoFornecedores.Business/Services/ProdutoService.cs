using GestaoFornecedores.Business.Interfaces.Services;
using GestaoFornecedores.Business.Models;
using GestaoFornecedores.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace GestaoFornecedores.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

using AutoMapper;
using GestaoFornecedores.App.ViewModels;
using GestaoFornecedores.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFornecedores.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoVIewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModels>().ReverseMap();
        }
    }
}

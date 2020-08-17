using GestaoFornecedores.App.Extensions;
using GestaoFornecedores.Business.Interfaces;
using GestaoFornecedores.Business.Interfaces.Respositories;
using GestaoFornecedores.Business.Interfaces.Services;
using GestaoFornecedores.Business.Notifications;
using GestaoFornecedores.Business.Services;
using GestaoFornecedores.Data.Repositories;
using GestaoFornecimento.Data.Context;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
namespace GestaoFornecedores.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            services.AddScoped<GestaoFornecedoresContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            return services;
        }
    }
}

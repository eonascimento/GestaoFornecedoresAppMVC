using GestaoFornecedores.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFornecedores.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificador _notificador;
        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(String.Empty,c.Mensagem));
            return View();
        }
    }
}

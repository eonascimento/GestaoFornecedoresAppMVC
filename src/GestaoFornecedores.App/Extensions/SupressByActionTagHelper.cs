using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;

namespace GestaoFornecedores.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-action")]
    public class SupressByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpAccessor;

        public SupressByActionTagHelper(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;
        }

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentException(nameof(context));
            if (context == null)
                throw new ArgumentException(nameof(output));

            var action = _httpAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (ActionName.Contains(action)) return;

            output.SuppressOutput();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFornecedores.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]
    public class SupressElementByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpAccessor;

        public SupressElementByClaimTagHelper(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;
        }

        [HtmlAttributeName("supress-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("supress-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentException(nameof(context));
            if (context == null)
                throw new ArgumentException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_httpAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (temAcesso) return;

            output.SuppressOutput();
        }
    }
}

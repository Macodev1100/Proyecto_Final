using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MotorTechService.TagHelpers
{
    /// <summary>
    /// TagHelper para mostrar/ocultar elementos basados en políticas de autorización
    /// Uso: <div asp-policy="CanManageClientes">Contenido solo para quienes pueden gestionar clientes</div>
    /// </summary>
    [HtmlTargetElement(Attributes = "asp-policy")]
    public class AuthorizationTagHelper : TagHelper
    {
        private readonly IAuthorizationService _authorizationService;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = null!;

        [HtmlAttributeName("asp-policy")]
        public string? Policy { get; set; }

        public AuthorizationTagHelper(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrWhiteSpace(Policy))
            {
                output.SuppressOutput();
                return;
            }

            var user = ViewContext.HttpContext.User;
            var authorized = await _authorizationService.AuthorizeAsync(user, Policy);

            if (!authorized.Succeeded)
            {
                output.SuppressOutput();
            }
        }
    }

    /// <summary>
    /// TagHelper para mostrar/ocultar elementos basados en roles
    /// Uso: <div asp-roles="Administrador,Supervisor">Contenido solo para Admin y Supervisor</div>
    /// </summary>
    [HtmlTargetElement(Attributes = "asp-roles")]
    public class RolesTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = null!;

        [HtmlAttributeName("asp-roles")]
        public string? Roles { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrWhiteSpace(Roles))
            {
                output.SuppressOutput();
                return;
            }

            var user = ViewContext.HttpContext.User;
            var roles = Roles.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var authorized = roles.Any(role => user.IsInRole(role.Trim()));

            if (!authorized)
            {
                output.SuppressOutput();
            }
        }
    }
}

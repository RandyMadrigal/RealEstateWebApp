#pragma checksum "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Agente\TipoPropiedades.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "990d40893927e6c1fbfa1da271c914c45d4e3138"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Agente_TipoPropiedades), @"mvc.1.0.view", @"/Views/Agente/TipoPropiedades.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\_ViewImports.cshtml"
using WebApp.RealEstateApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\_ViewImports.cshtml"
using WebApp.RealEstateApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Agente\TipoPropiedades.cshtml"
using RealEstateApp.Core.Application.ViewModels.TipoPropiedadCategory;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"990d40893927e6c1fbfa1da271c914c45d4e3138", @"/Views/Agente/TipoPropiedades.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"72394fdb40496eb5af2efc2dca8f56d4e5e04cb7", @"/Views/_ViewImports.cshtml")]
    public class Views_Agente_TipoPropiedades : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<TipoPropiedadVm>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Agente", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Mantenimiento", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger float-end mx-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Agente\TipoPropiedades.cshtml"
  
    ViewData["Title"] = "Categorias";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container text-white\">\r\n\r\n    <div class=\" col-md-12 float-end\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "990d40893927e6c1fbfa1da271c914c45d4e31384853", async() => {
                WriteLiteral("volver atras");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    </div>

    <div class=""row"">

        <div class=""col-md-12 col-sm-6 mt-2"">
            <div class=""card bg-dark text-white text-center"">
                <div class=""card-title"">
                    <h3 class=""card-text fw-bold "">Tipos de propiedades</h3>
                </div>
            </div>
        </div>

        <div class=""row"">

            <div class=""col-md-12 mt-2"">

                <div class=""container-fluid text-white"">

                    <div class=""row"">

");
#nullable restore
#line 34 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Agente\TipoPropiedades.cshtml"
                         if (Model.Count == 0 || Model == null)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <h3> No hay propiedades creadas </h3>\r\n");
#nullable restore
#line 37 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Agente\TipoPropiedades.cshtml"
                        }
                        else
                        {

                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Agente\TipoPropiedades.cshtml"
                             foreach (TipoPropiedadVm item in Model)
                            {


#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <div class=""col-4 mb-3"">

                                    <div class=""card bg-dark bg-opacity-50 animate__animated animate__fadeIn"">

                                        <div class=""card-body"">
                                            <h5 class=""text-center""> ");
#nullable restore
#line 49 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Agente\TipoPropiedades.cshtml"
                                                                Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h5>\r\n                                        </div>\r\n                                    </div>\r\n                                </div>\r\n");
#nullable restore
#line 53 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Agente\TipoPropiedades.cshtml"

                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Agente\TipoPropiedades.cshtml"
                             
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                    </div>\r\n                </div>\r\n\r\n            </div>\r\n\r\n        </div>\r\n\r\n    </div>\r\n\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<TipoPropiedadVm>> Html { get; private set; }
    }
}
#pragma warning restore 1591

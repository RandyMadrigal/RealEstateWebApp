#pragma checksum "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Admin\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5aa139452e07ce51999e80cc9241f5cfae6e1b36"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Index), @"mvc.1.0.view", @"/Views/Admin/Index.cshtml")]
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
#line 1 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Admin\Index.cshtml"
using RealEstateApp.Core.Application.ViewModels.Propiedades;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5aa139452e07ce51999e80cc9241f5cfae6e1b36", @"/Views/Admin/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"72394fdb40496eb5af2efc2dca8f56d4e5e04cb7", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Admin\Index.cshtml"
  
    if (true)
    {

    }
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">

    <div class=""row"">
        <div class=""col-md-12 col-sm-12 text-white text-center"">

            <h3>Home</h3>

        </div>
    </div>

    <div class=""row my-3"">

        <div class=""col-md-6 animate__animated animate__fadeIn"">
            <div class=""card text-bg-info mb-3 text-center fw-bold fs-4 "">
                <div class=""card-header "">Propiedades Registradas</div>
                <div class=""card-body"">
                    <div class=""row"">
                        <div class=""col-md-12"">
                                                          
                                <p> ");
#nullable restore
#line 30 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Admin\Index.cshtml"
                               Write(ViewBag.Propiedades);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class=""col-md-6 animate__animated animate__fadeIn"">
            <div class=""card text-bg-info mb-3 text-center fw-bold fs-4"">
                <div class=""card-header "">Agentes</div>
                <div class=""card-body"">
                    <div class=""row"">
                        <div class=""col-md-6"">
                            <p>Agentes Activos</p>
                            <hr />
                            <p> ");
#nullable restore
#line 46 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Admin\Index.cshtml"
                           Write(ViewBag.ActiveAgente);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n\r\n                        <div class=\"col-md-6\">\r\n                            <p>Agentes Inactivos</p>\r\n                            <hr />\r\n                            <p> ");
#nullable restore
#line 52 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Admin\Index.cshtml"
                           Write(ViewBag.InactiveAgente);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class=""col-md-6 animate__animated animate__fadeIn"">
            <div class=""card text-bg-info mb-3 text-center fw-bold fs-4"">
                <div class=""card-header "">Clientes</div>
                <div class=""card-body"">
                    <div class=""row"">
                        <div class=""col-md-6"">
                            <p> Clientes Activos</p>
                            <hr />
                            <p>");
#nullable restore
#line 67 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Admin\Index.cshtml"
                          Write(ViewBag.ActiveCliente);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n                        </div>\r\n\r\n                        <div class=\"col-md-6\">\r\n                            <p> Clientes Inactivos </p>\r\n                            <hr />\r\n                            <p>");
#nullable restore
#line 74 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Admin\Index.cshtml"
                          Write(ViewBag.InactiveCliente);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class=""col-md-6 animate__animated animate__fadeIn"">
            <div class=""card text-bg-info mb-3 text-center fw-bold fs-4"">
                <div class=""card-header "">Desarrolladores</div>
                <div class=""card-body"">
                    <div class=""row"">
                        <div class=""col-md-6"">
                            <p> Desarrollador Activo</p>
                            <hr />
                            <p>");
#nullable restore
#line 90 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Admin\Index.cshtml"
                          Write(ViewBag.ActiveDeveloper);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n                        </div>\r\n\r\n                        <div class=\"col-md-6\">\r\n                            <p> Desarrollador Inactivo </p>\r\n                            <hr />\r\n                            <p>");
#nullable restore
#line 97 "C:\Users\Randy\Desktop\Programacion 3\RealEstateApp\WebApp.RealEstateApp\Views\Admin\Index.cshtml"
                          Write(ViewBag.InactiveDeveloper);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3b095f2bb9b5cbdeb3ec2473110afb8c654a467f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Warehouse135_WarehouseViewAll), @"mvc.1.0.view", @"/Views/Warehouse135/WarehouseViewAll.cshtml")]
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
#line 1 "D:\ASPProjects\AppNov14\AppNov14\Views\_ViewImports.cshtml"
using AppNov14;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ASPProjects\AppNov14\AppNov14\Views\_ViewImports.cshtml"
using AppNov14.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b095f2bb9b5cbdeb3ec2473110afb8c654a467f", @"/Views/Warehouse135/WarehouseViewAll.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc27da020e02670cc1c8035b0a5e0363b72054d0", @"/Views/_ViewImports.cshtml")]
    public class Views_Warehouse135_WarehouseViewAll : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AppNov14.Models.Warehouse135Model>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-secondary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "MainTableDB", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 4 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
  
    ViewData["Title"] = "Складская таблица";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"text-center\">Складская таблица</h1>\r\n\r\n\r\n\r\n");
#nullable restore
#line 12 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
  

    int j = -1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
 foreach (var item in Model.listil)
{
    j++;


#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"jumbotron\">\r\n        <h4 class=\"text-center\">");
#nullable restore
#line 21 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
                           Write(Model.list[j]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n    </div>\r\n");
            WriteLiteral(@"    <table align=""center"" class=""table table-bordered"">
        <thead class=""thead-dark"">
            <tr>
                <th>
                    #Id
                </th>
                <th>
                    Тип материала
                </th>
                <th>
                    Наименование типа материала
                </th>
                <th>
                    Поставщик
                </th>
                <th>
                    Производитель
                </th>
                <th>
                    Остатки
                </th>
            </tr>

        </thead>

        <tbody>
");
#nullable restore
#line 50 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
             for (int i = 0; i < item.Rows.Count; i++)

            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 55 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
                   Write(item.Rows[i]["Id"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 58 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
                   Write(item.Rows[i]["TypeOfMaterial"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 61 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
                   Write(item.Rows[i]["NameOfTypeMaterial"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 64 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
                   Write(item.Rows[i]["Provider"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 67 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
                   Write(item.Rows[i]["Manufacturer"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 70 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
                   Write(item.Rows[i]["Leftovers"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 74 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n        <hr />\r\n        <br />\r\n");
#nullable restore
#line 79 "D:\ASPProjects\AppNov14\AppNov14\Views\Warehouse135\WarehouseViewAll.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br>\r\n<br>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3b095f2bb9b5cbdeb3ec2473110afb8c654a467f8630", async() => {
                WriteLiteral("Назад");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<br />\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AppNov14.Models.Warehouse135Model> Html { get; private set; }
    }
}
#pragma warning restore 1591
#pragma checksum "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Profile\Portfolio.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fbe44cc4cb781e3bcd0f38da1fbe967cd5df68af"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Profile_Portfolio), @"mvc.1.0.view", @"/Views/Profile/Portfolio.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Profile/Portfolio.cshtml", typeof(AspNetCore.Views_Profile_Portfolio))]
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
#line 1 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\_ViewImports.cshtml"
using MainProjectFull;

#line default
#line hidden
#line 2 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 3 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\_ViewImports.cshtml"
using MainProjectFull.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fbe44cc4cb781e3bcd0f38da1fbe967cd5df68af", @"/Views/Profile/Portfolio.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2ea5109a68739a776e573f33ef734ff6fb8cc10", @"/Views/_ViewImports.cshtml")]
    public class Views_Profile_Portfolio : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MainProjectFull.ViewModel.PortVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/profile/main.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/profile/cv.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/profile/cvmedia.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(85, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Profile\Portfolio.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(141, 14, true);
            WriteLiteral("\r\n<main>\r\n    ");
            EndContext();
            BeginContext(156, 51, false);
#line 9 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Profile\Portfolio.cshtml"
Write(await Html.PartialAsync("_Cover", Model.CoverClass));

#line default
#line hidden
            EndContext();
            BeginContext(207, 166, true);
            WriteLiteral("\r\n    <!--=============================== Partial view area ===================================-->\r\n    <!-- Main section -->\r\n\r\n    <div id=\"partials\" data-profile=\"");
            EndContext();
            BeginContext(374, 27, false);
#line 13 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Profile\Portfolio.cshtml"
                                Write(Model.CoverClass.Profile.id);

#line default
#line hidden
            EndContext();
            BeginContext(401, 12, true);
            WriteLiteral("\">\r\n        ");
            EndContext();
            BeginContext(414, 44, false);
#line 14 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Profile\Portfolio.cshtml"
   Write(await Html.PartialAsync("_Portfolio", Model));

#line default
#line hidden
            EndContext();
            BeginContext(458, 121, true);
            WriteLiteral("\r\n    </div>\r\n    <!--=============================== Partial view area ===================================-->\r\n</main>\r\n");
            EndContext();
            BeginContext(580, 40, false);
#line 18 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Profile\Portfolio.cshtml"
Write(await Html.PartialAsync("_AjaxSections"));

#line default
#line hidden
            EndContext();
            BeginContext(620, 156, true);
            WriteLiteral("\r\n\r\n<script>\r\n    $(\".profile-navbar .ul-menu li\").removeClass(\'active\');\r\n    $(\".profile-navbar .ul-menu li#Portfolio\").addClass(\'active\');\r\n</script>\r\n\r\n");
            EndContext();
            DefineSection("Styles", async() => {
                BeginContext(792, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(798, 53, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "fbe44cc4cb781e3bcd0f38da1fbe967cd5df68af7362", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(851, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(857, 51, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "fbe44cc4cb781e3bcd0f38da1fbe967cd5df68af8695", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(908, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(914, 56, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "fbe44cc4cb781e3bcd0f38da1fbe967cd5df68af10028", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(970, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            BeginContext(975, 2, true);
            WriteLiteral("\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<Users> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MainProjectFull.ViewModel.PortVM> Html { get; private set; }
    }
}
#pragma warning restore 1591

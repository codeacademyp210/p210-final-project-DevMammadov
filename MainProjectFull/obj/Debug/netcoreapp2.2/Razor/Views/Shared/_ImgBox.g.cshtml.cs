#pragma checksum "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e1feb0852f0b7c2a172974dc14aba7f4daf9567a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ImgBox), @"mvc.1.0.view", @"/Views/Shared/_ImgBox.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_ImgBox.cshtml", typeof(AspNetCore.Views_Shared__ImgBox))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e1feb0852f0b7c2a172974dc14aba7f4daf9567a", @"/Views/Shared/_ImgBox.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2ea5109a68739a776e573f33ef734ff6fb8cc10", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__ImgBox : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MainProjectFull.ViewModel.ImgBoxVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(87, 199, true);
            WriteLiteral("\r\n<div class=\"cancel\">\r\n    <a href=\"#\">\r\n        <i class=\"far fa-times-circle\"></i>\r\n    </a>\r\n</div>\r\n\r\n<div class=\"container window-main\">\r\n    <div class=\"window-header main-back\">\r\n        <h2>");
            EndContext();
            BeginContext(287, 20, false);
#line 12 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
       Write(Model.Portfolio.Name);

#line default
#line hidden
            EndContext();
            BeginContext(307, 24, true);
            WriteLiteral("</h2>\r\n        <div>\r\n\r\n");
            EndContext();
#line 15 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
             if (Model.CurrentUser.Id == Model.Profile.UsersId)
            {

#line default
#line hidden
            BeginContext(411, 157, true);
            WriteLiteral("                <a href=\"#\" class=\"port-img-add\" title=\"Portfolioya şəkil əlavə et\"><i class=\"fas fa-folder-plus\"></i></a>\r\n                <input data-url=\"");
            EndContext();
            BeginContext(569, 28, false);
#line 18 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                            Write(Url.Action("AddImg", "Port"));

#line default
#line hidden
            EndContext();
            BeginContext(597, 13, true);
            WriteLiteral("\" data-port=\"");
            EndContext();
            BeginContext(611, 18, false);
#line 18 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                                                                      Write(Model.Portfolio.id);

#line default
#line hidden
            EndContext();
            BeginContext(629, 12, true);
            WriteLiteral("\" data-src=\"");
            EndContext();
            BeginContext(642, 43, false);
#line 18 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                                                                                                     Write(Url.Content("~/Content/Images/PortImages/"));

#line default
#line hidden
            EndContext();
            BeginContext(685, 64, true);
            WriteLiteral("\" type=\"file\" class=\"port-img-select hidden-input\" value=\"\" />\r\n");
            EndContext();
#line 19 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
            }

#line default
#line hidden
            BeginContext(764, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 21 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
             if (Model.CurrentUser.Id == Model.Profile.UsersId)
            {

#line default
#line hidden
            BeginContext(846, 59, true);
            WriteLiteral("                <a href=\"#\" class=\"remove-port\" data-port=\"");
            EndContext();
            BeginContext(906, 18, false);
#line 23 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                                                      Write(Model.Portfolio.id);

#line default
#line hidden
            EndContext();
            BeginContext(924, 63, true);
            WriteLiteral("\" title=\"Portfolinu sil\"><i class=\"fas fa-trash-alt\"></i></a>\r\n");
            EndContext();
#line 24 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
            }

#line default
#line hidden
            BeginContext(1002, 16, true);
            WriteLiteral("\r\n            <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1018, "\"", 1089, 1);
#line 26 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
WriteAttributeValue("", 1025, Url.Action("Portfolio","Profile",new { id = Model.Profile.id }), 1025, 64, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1090, 19, true);
            WriteLiteral(">\r\n                ");
            EndContext();
            BeginContext(1109, 62, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "e1feb0852f0b7c2a172974dc14aba7f4daf9567a8051", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1119, "~/Content/Images/Users/", 1119, 23, true);
#line 27 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
AddHtmlAttributeValue("", 1142, Model.Profile.Photo, 1142, 20, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1171, 88, true);
            WriteLiteral("\r\n            </a>\r\n        </div>\r\n    </div>\r\n    <div class=\"window-body\" data-port=\"");
            EndContext();
            BeginContext(1260, 18, false);
#line 31 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                                   Write(Model.Portfolio.id);

#line default
#line hidden
            EndContext();
            BeginContext(1278, 15, true);
            WriteLiteral("\" data-remove=\"");
            EndContext();
            BeginContext(1294, 30, false);
#line 31 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                                                                     Write(Url.Action("RemoveImg","Port"));

#line default
#line hidden
            EndContext();
            BeginContext(1324, 4, true);
            WriteLiteral("\">\r\n");
            EndContext();
#line 32 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
         foreach (var item in Model.PortImages)
        {

#line default
#line hidden
            BeginContext(1388, 40, true);
            WriteLiteral("            <div class=\"port-img-div\">\r\n");
            EndContext();
#line 35 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                 if (Model.CurrentUser.Id == Model.Profile.UsersId)
                {

#line default
#line hidden
            BeginContext(1516, 144, true);
            WriteLiteral("                    <a href=\"#\" class=\"remove-port-img\">\r\n\r\n                        <i class=\"fas fa-trash-alt\"></i>\r\n                    </a>\r\n");
            EndContext();
#line 41 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                }

#line default
#line hidden
            BeginContext(1679, 16, true);
            WriteLiteral("                ");
            EndContext();
            BeginContext(1695, 66, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e1feb0852f0b7c2a172974dc14aba7f4daf9567a11751", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 42 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
AddHtmlAttributeValue("", 1704, item.id, 1704, 8, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1719, "~/Content/Images/PortImages/", 1719, 28, true);
#line 42 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
AddHtmlAttributeValue("", 1747, item.Name, 1747, 10, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1761, 22, true);
            WriteLiteral("\r\n            </div>\r\n");
            EndContext();
#line 44 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
        }

#line default
#line hidden
            BeginContext(1794, 43, true);
            WriteLiteral("\r\n        <div class=\"about\">\r\n            ");
            EndContext();
            BeginContext(1838, 21, false);
#line 47 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
       Write(Model.Portfolio.About);

#line default
#line hidden
            EndContext();
            BeginContext(1859, 144, true);
            WriteLiteral("\r\n        </div>\r\n        <div class=\"links\">\r\n            <div class=\"port-view main-back-color\">\r\n                <i class=\"far fa-eye\"></i>  ");
            EndContext();
            BeginContext(2004, 20, false);
#line 51 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                                       Write(Model.Portfolio.View);

#line default
#line hidden
            EndContext();
            BeginContext(2024, 41, true);
            WriteLiteral("\r\n            </div>\r\n            <div>\r\n");
            EndContext();
#line 54 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                 if (Model.Portfolio.Behance != null)
                {

#line default
#line hidden
            BeginContext(2139, 22, true);
            WriteLiteral("                    <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2161, "\"", 2192, 1);
#line 56 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
WriteAttributeValue("", 2168, Model.Portfolio.Behance, 2168, 24, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2193, 132, true);
            WriteLiteral(" target=\"_blank\" class=\"main-back-color\">\r\n                        <i class=\"fab fa-behance-square\"></i>\r\n                    </a>\r\n");
            EndContext();
#line 59 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                }

#line default
#line hidden
            BeginContext(2344, 16, true);
            WriteLiteral("                ");
            EndContext();
#line 60 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                 if (Model.Portfolio.Github != null)
                {

#line default
#line hidden
            BeginContext(2417, 22, true);
            WriteLiteral("                    <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2439, "\"", 2469, 1);
#line 62 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
WriteAttributeValue("", 2446, Model.Portfolio.Github, 2446, 23, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2470, 131, true);
            WriteLiteral(" target=\"_blank\" class=\"main-back-color\">\r\n                        <i class=\"fab fa-github-square\"></i>\r\n                    </a>\r\n");
            EndContext();
#line 65 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                }

#line default
#line hidden
            BeginContext(2620, 16, true);
            WriteLiteral("                ");
            EndContext();
#line 66 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                 if (Model.Portfolio.Website != null)
                {

#line default
#line hidden
            BeginContext(2694, 22, true);
            WriteLiteral("                    <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2716, "\"", 2747, 1);
#line 68 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
WriteAttributeValue("", 2723, Model.Portfolio.Website, 2723, 24, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2748, 123, true);
            WriteLiteral(" target=\"_blank\" class=\"main-back-color\">\r\n                        <i class=\"fas fa-globe\"></i>\r\n                    </a>\r\n");
            EndContext();
#line 71 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                }

#line default
#line hidden
            BeginContext(2890, 81, true);
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"window-footer\">\r\n");
            EndContext();
#line 76 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
         if(SignInManager.IsSignedIn(User))
        {

#line default
#line hidden
            BeginContext(3027, 48, true);
            WriteLiteral("            <div class=\"comment-form\" data-url=\"");
            EndContext();
            BeginContext(3076, 32, false);
#line 78 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                                           Write(Url.Action("AddComment", "Port"));

#line default
#line hidden
            EndContext();
            BeginContext(3108, 12, true);
            WriteLiteral("\" data-src=\"");
            EndContext();
            BeginContext(3121, 38, false);
#line 78 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                                                                                        Write(Url.Content("~/Content/Images/Users/"));

#line default
#line hidden
            EndContext();
            BeginContext(3159, 189, true);
            WriteLiteral("\">\r\n                <textarea class=\"form-control\" placeholder=\"Şərh yaz\"></textarea>\r\n                <button class=\"butt butt-gray mt-2 send-comment\">Paylaş</button>\r\n            </div>\r\n");
            EndContext();
#line 82 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
        }

#line default
#line hidden
            BeginContext(3359, 34, true);
            WriteLiteral("        <div class=\"comments\">\r\n\r\n");
            EndContext();
#line 85 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
             foreach (var item in Model.Comments)
            {
                var user = Model.Users.FirstOrDefault(u => u.Id == item.SenderId);
                var profile = Model.Profiles.FirstOrDefault(p => p.UsersId == user.Id);

#line default
#line hidden
            BeginContext(3632, 104, true);
            WriteLiteral("                <div class=\"comment\">\r\n                    <div class=\"image\">\r\n                        ");
            EndContext();
            BeginContext(3736, 56, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "e1feb0852f0b7c2a172974dc14aba7f4daf9567a20873", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 3746, "~/Content/Images/Users/", 3746, 23, true);
#line 91 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
AddHtmlAttributeValue("", 3769, profile.Photo, 3769, 14, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3792, 133, true);
            WriteLiteral("\r\n                    </div>\r\n                    <div class=\"text\">\r\n                        <div class=\"user-name main-back-color\">");
            EndContext();
            BeginContext(3926, 9, false);
#line 94 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                                                          Write(user.Name);

#line default
#line hidden
            EndContext();
            BeginContext(3935, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(3937, 12, false);
#line 94 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                                                                     Write(user.Surname);

#line default
#line hidden
            EndContext();
            BeginContext(3949, 10, true);
            WriteLiteral(" - <small>");
            EndContext();
            BeginContext(3960, 9, false);
#line 94 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                                                                                            Write(item.Date);

#line default
#line hidden
            EndContext();
            BeginContext(3969, 41, true);
            WriteLiteral("</small> </div>\r\n                        ");
            EndContext();
            BeginContext(4011, 9, false);
#line 95 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
                   Write(item.Text);

#line default
#line hidden
            EndContext();
            BeginContext(4020, 54, true);
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n");
            EndContext();
#line 98 "C:\Users\santa\OneDrive\Desktop\MainProjectFull\MainProjectFull\Views\Shared\_ImgBox.cshtml"
            }

#line default
#line hidden
            BeginContext(4089, 36, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MainProjectFull.ViewModel.ImgBoxVM> Html { get; private set; }
    }
}
#pragma warning restore 1591

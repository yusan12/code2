using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text.Encodings.Web;
namespace Yotsuba.Pages.En
{
    public class KanaTableModel : PageModel
    {

    }
    public sealed class KanaTagHelper : TagHelper
    {
        public string Romaji{get; set; }
        public string KanaChar{ get;set;}
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "div";
            output.AddClass("kana", HtmlEncoder.Default);
            output.Content.AppendHtml($"<div>{KanaChar}</div>");
            output.Content.AppendHtml($"<div>{Romaji}</div>");
        }
    }
}

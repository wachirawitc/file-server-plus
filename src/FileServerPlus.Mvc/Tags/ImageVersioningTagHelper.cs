using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text.Encodings.Web;

namespace FileServerPlus.Mvc.Tags
{
    [HtmlTargetElement("img", Attributes = AppendVersionAttributeName + "," + SrcAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class ImageVersioningTagHelper : UrlResolutionTagHelper
    {
        private const string AppendVersionAttributeName = "asp-file-server-version";

        private const string SrcAttributeName = "src";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }
        }

        [HtmlAttributeName(AppendVersionAttributeName)]
        public bool AppendVersion { get; set; }

        public ImageVersioningTagHelper(IUrlHelperFactory urlHelperFactory, HtmlEncoder htmlEncoder) : base(urlHelperFactory, htmlEncoder)
        {
        }
    }
}
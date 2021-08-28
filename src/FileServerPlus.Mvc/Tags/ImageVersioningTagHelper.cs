﻿using FileServerPlus.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Text.Encodings.Web;

namespace FileServerPlus.Mvc.Tags
{
    [HtmlTargetElement("img", Attributes = AppendVersionAttributeName + "," + SrcAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class ImageVersioningTagHelper : UrlResolutionTagHelper
    {
        private const string AppendVersionAttributeName = "asp-file-server-version";

        private const string SrcAttributeName = "src";

        [HtmlAttributeName(SrcAttributeName)]
        public string Src { get; set; }

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

            output.CopyHtmlAttribute(SrcAttributeName, context);

            ProcessUrlAttribute(SrcAttributeName, output);

            if (AppendVersion)
            {
                var option = FileServerRegister.Instance.GetOption(Src);
                if (option != null)
                {
                    Src = output.Attributes[SrcAttributeName].Value as string;

                    var fileServerVersionProvider = new FileServerVersionProvider(option.RequestPath, option.FileProvider, _cache);
                    var path = fileServerVersionProvider.AddFileVersionToPath(ViewContext.HttpContext.Request.PathBase, Src);

                    output.Attributes.SetAttribute(SrcAttributeName, path);
                }
            }
        }

        [HtmlAttributeName(AppendVersionAttributeName)]
        public bool AppendVersion { get; set; }

        private readonly IMemoryCache _cache;

        public ImageVersioningTagHelper(IMemoryCache cache,
            IUrlHelperFactory urlHelperFactory,
            HtmlEncoder htmlEncoder) : base(urlHelperFactory, htmlEncoder)
        {
            _cache = cache;
        }
    }
}
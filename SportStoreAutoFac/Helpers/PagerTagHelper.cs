using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportStoreAutoFac.UI.Paging;

namespace SportStoreAutoFac.Helpers
{
    public class PagerTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PagerTagHelper(IUrlHelperFactory urlHelperFactory) {
            this.urlHelperFactory = urlHelperFactory;
        }

        public IPaginatedList Source { get; set; }

        // The ViewContext object is the object that provides access to things like the HttpContext,
        // HttpRequest, HttpResponse and so on.
        // The way that you can gain access to it in a TagHelper is via a property0
        // but in such a case you need to set the [ViewContext]
        // attribute so that the property gets set to the current ViewContext. 
        [ViewContext]

        //[HtmlAttributeNotBound] basically says that this attribute isn't one
        //that you intend to set via a tag helper attribute in the html.

        // Your tag helper may not need access to the ViewContext object and all it's subobjects.
        // If not, you can omit the ViewContext property and associated attributes from your TagHelper.
        // It's certainly not a required property for a TagHelper and
        // most of my own tag helpers so far have not needed access to it.
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public Dictionary<string, object> PageUrlValues { get; set; }
            = new Dictionary<string, object>();

        public string PageAction { get; set; }

        public bool PageClassesEnabled { get; set; } = false;

        public string PageClass { get; set; }

        public string PageClassNormal { get; set; }

        public string PageClassSelected { get; set; }

        public int CurrentPage { get; set; }

        public override void Process(TagHelperContext context,
            TagHelperOutput output) {
            output.TagName = "div";
            output.Attributes.Add("class", PageClass);
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            for (int i = 1; i <= Source.TotalPages; i++) {
                TagBuilder tag = new TagBuilder("a");
                PageUrlValues["pageIndex"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                if (PageClassesEnabled) {
                    tag.AddCssClass(i == CurrentPage
                        ? PageClassSelected
                        : PageClassNormal);
                    tag.AddCssClass("item");
                }

                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportStoreAutoFac.UI.Paging;

namespace SportStoreAutoFac.Helpers
{
    public class PagerNewTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public PagerNewTagHelper (IUrlHelperFactory urlHelperFactory) {
            _urlHelperFactory = urlHelperFactory;
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

        public int CurrentPage { get; set; }

        public override void Process (TagHelperContext context, TagHelperOutput output) {
            output.TagName = "ul";
            output.Attributes.Add("class", "pagination");
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            TagBuilder result = new TagBuilder("ul");

            TagBuilder liPrevTag = new TagBuilder("li");
            liPrevTag.AddCssClass("page-item");
            if (!Source.HasPreviousPage)
                liPrevTag.AddCssClass("disabled");

            TagBuilder aPrevTag = new TagBuilder("a");
            aPrevTag.AddCssClass("page-link");
            PageUrlValues["pageIndex"] = Source.PageIndex - 1;

            aPrevTag.Attributes["href"] = Source.PageIndex > 1 
                ? urlHelper.Action(PageAction, PageUrlValues) 
                : "#";

            aPrevTag.InnerHtml.Append("Previous");
            liPrevTag.InnerHtml.AppendHtml(aPrevTag);
            result.InnerHtml.AppendHtml(liPrevTag);

            for (int i = 1; i <= Source.TotalPages; i++) {
                TagBuilder liTag = new TagBuilder("li");
                liTag.AddCssClass("page-item");
                liTag.AddCssClass(i == CurrentPage ? "active" : "");

                TagBuilder aTag = new TagBuilder("a");
                aTag.AddCssClass("page-link");

                PageUrlValues["pageIndex"] = i;
                aTag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                aTag.InnerHtml.Append(i.ToString());
                liTag.InnerHtml.AppendHtml(aTag);

                result.InnerHtml.AppendHtml(liTag);
            }

            TagBuilder liNextTag = new TagBuilder("li");

            liNextTag.AddCssClass("page-item");
            if (!Source.HasNextPage)
                liNextTag.AddCssClass("disabled");

            TagBuilder aNextTag = new TagBuilder("a");
            aNextTag.AddCssClass("page-link");
            PageUrlValues["pageIndex"] = Source.PageIndex + 1;

            aNextTag.Attributes["href"] = Source.PageIndex == Source.TotalPages 
                ? "#" 
                : urlHelper.Action(PageAction, PageUrlValues);

            aNextTag.InnerHtml.Append("Next");
            liNextTag.InnerHtml.AppendHtml(aNextTag);
            result.InnerHtml.AppendHtml(liNextTag);

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
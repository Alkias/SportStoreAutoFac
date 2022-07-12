using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SportStoreAutoFac.Helpers
{
    public class CustomEmailTagHelper : TagHelper
    {
        #region Overrides of TagHelper

        public string MyEmail { get; set; }

        public override void Process(TagHelperContext context,
            TagHelperOutput output) {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"mailto:{MyEmail}");
            output.Attributes.Add("id", "my-email-id");
            output.Content.SetContent("my-email");
        }

        #endregion
    }

}
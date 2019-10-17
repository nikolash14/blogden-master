using System.Web.Mvc;

namespace MindfireSolutions.Custom
{
    /// <summary>
    /// Html helper for Image
    /// </summary>
    public static class CustomRazorHtml
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText,string id)
        {
            var builder = new TagBuilder("img");
            builder.Attributes.Add("src", src);
            builder.Attributes.Add("alt", altText);
            builder.Attributes.Add("class", "image-view");
            builder.Attributes.Add("id", id);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString FileFor(this HtmlHelper helper, string Id, string File)
        {
            var builder = new TagBuilder("input");

            builder.MergeAttribute("type", "file");

            builder.MergeAttribute("Id", Id);

            builder.MergeAttribute("name", File);

            builder.MergeAttribute("style", "color:black");

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));

        }
    }
}
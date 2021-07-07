using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Phytime.Models;

namespace Phytime.Helpers
{
    public static class PagingHelper
    {
        public static HtmlString PageLinks(this IHtmlHelper html,
        PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml.Append(i.ToString());
                // если текущая страница, то выделяем ее,
                // например, добавляя класс
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                var writer = new System.IO.StringWriter();
                tag.WriteTo(writer, HtmlEncoder.Default);
                result.Append(writer.ToString());
            }
            return new HtmlString(result.ToString());
        }
    }
}

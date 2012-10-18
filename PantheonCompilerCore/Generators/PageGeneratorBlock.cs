using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Pantheon.Core;

namespace Pantheon.Compiler.Core.Generators
{
    public class PageGeneratorBlock : GeneratorBlock
    {
        public override void TransformHtml(Generator generator, Drawable element, HtmlNode node)
        {
            var document = node.OwnerDocument;
            var body = document.CreateElement("body");
            var head = document.CreateElement("head");
            var title = document.CreateElement("title");
            var cssLink = document.CreateElement("link");

            cssLink.Attributes.Add(document.CreateAttribute("rel", "stylesheet"));
            cssLink.Attributes.Add(document.CreateAttribute("type", "text/css"));
            cssLink.Attributes.Add(document.CreateAttribute("href", "xamlcore.css"));

            // TODO: Add Scripts from Page.

            title.AppendChild(document.CreateTextNode(((Page)element).Title));

            node.AppendChild(head);
            head.AppendChild(title);
            head.AppendChild(cssLink); 
            node.AppendChild(body);

            base.TransformHtml(generator, element, body);
        }
    }
}

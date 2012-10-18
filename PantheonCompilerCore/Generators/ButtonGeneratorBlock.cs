using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Pantheon.Core;

namespace Pantheon.Compiler.Core.Generators
{
    public sealed class ButtonGeneratorBlock : GeneratorBlock
    {
        public override void TransformHtml(Generator generator, Drawable element, HtmlNode node)
        {
            var document = node.OwnerDocument;
            var buttonContainer = document.CreateElement("div");
            var style = element.Style;

            // TODO: Move this to GeneratorBlock
            if (style != null)
                buttonContainer.Attributes.Add(document.CreateAttribute("class", style));

            // Add our created element to the parent.
            node.AppendChild(buttonContainer);

            // For now we'll just add "Hello world" if a Button contains no content.
            if (((Button)element).Text == null)
                buttonContainer.AppendChild(document.CreateTextNode("Hello world"));

            // IMPORTANT: Unless you know better, always call this at the end. Otherwise the Content of this
            // node will NOT be turned into HTML.
            base.TransformHtml(generator, element, buttonContainer);
        }
    }
}

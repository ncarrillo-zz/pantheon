﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Pantheon.Core;

namespace Pantheon.Compiler.Core.Generators
{
    public sealed class ButtonGeneratorBlock : GeneratorBlock
    {
        public override string TransformStyle(Style style)
        {
            var cssBuilder = new StringBuilder();
            cssBuilder.AppendLine("display: -webkit-flex;");
            cssBuilder.AppendLine("display: -ms-flexbox;");
            cssBuilder.AppendLine("-ms-flex-pack: center;"); 
            cssBuilder.AppendLine("-ms-flex-align: center;");
            cssBuilder.AppendLine("-webkit-flex-pack: center;");
            cssBuilder.AppendLine("-webkit-flex-align: center;"); 

            // Get our default Drawable CSS class-level properties too. 
            var cssResult = base.TransformStyle(style);
            cssBuilder.AppendLine(cssResult);

            return cssBuilder.ToString();
        }

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
                buttonContainer.AppendChild(document.CreateTextNode("Browse"));

            // IMPORTANT: Unless you know better, always call this at the end. Otherwise the Content of this
            // node will NOT be turned into HTML.
            base.TransformHtml(generator, element, buttonContainer);
        }
    }
}

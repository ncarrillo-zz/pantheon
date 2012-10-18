using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Pantheon.Core;

namespace Pantheon.Compiler.Core.Generators
{
    public sealed class StackPanelGeneratorBlock : GeneratorBlock
    {
        public override string TransformStyle(Style style)
        {
            // Add some of our custom CSS properties here.
            // TODO: Look into FlexBox stuff more, add Moz and Opera stuff.
            var cssBuilder = new StringBuilder();
            cssBuilder.AppendLine("padding: 0px;");
            cssBuilder.AppendLine("display: -ms-flexbox;");
            cssBuilder.AppendLine("display: -webkit-flex;");

            // Get our default Drawable CSS class-level properties too. 
            var cssResult = base.TransformStyle(style);
            cssBuilder.AppendLine(cssResult);

            return cssBuilder.ToString();
        }

        public override string TransformSet(Set set)
        {
            // Property we care about catching here is Orientation. TODO: Stronger typing here.
            if (set.Property.ToLower() == "orientation")
            {
                switch (set.To.ToLower())
                {
                    // Some CSS3 FlexBox-isms. TODO: WebKit and Moz vendor extensions too.
                    case "vertical":
                        return "-ms-flex-direction: column; -webkit-flex-direction: column;";
                    case "horizontal":
                        return "-ms-flex-direction: row; -webkit-flex-direction: row;";
                    default:
                        // We should catch and throw some sort of error here. 
                        break;
                }
            }

            // IMPORTANT: We need default Drawable CSS property.
            return base.TransformSet(set);
        }

        public override void TransformHtml(Generator generator, Drawable element, HtmlNode node)
        {
            // TODO: Flesh this out. Rather bare bones at the moment.
            var document = node.OwnerDocument;
            var newElement = document.CreateElement("ul");
            var style = element.Style;

            // TODO: Move this to GeneratorBlock, or not, at least not for this GeneratorBlock impl.
            // Reason being is that I do some ContentProperty voodoo, which isnt appropriate for us here.
            if (style != null)
                newElement.Attributes.Add(document.CreateAttribute("class", style));

            // Add our created element to the parent.
            node.AppendChild(newElement);

            // This is where special handling of Content comes in. I simply loop through the StackPanel and call generate.
            // TODO: Try to generalize this and move it to GeneratorBlock. Will clean things up a bit.
            foreach (var child in ((StackPanel)element).Children)
            {
                generator.Generate(newElement, child);
            }
        }
    }
}

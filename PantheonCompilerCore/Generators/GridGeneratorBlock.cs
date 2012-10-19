using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Pantheon.Core;

namespace Pantheon.Compiler.Core.Generators
{
    public class GridGeneratorBlock : GeneratorBlock
    {
        // TODO: Need to land some changes in CSS world. Generate ID vs Classes..because Grids are unqiue..
        // They likely wont be Stylable ..
        // DISREGARD ^ NOT FUCKING TRUE.
        // We need to land Strong typing in Setters. So I can do <Set Property="GridRows"><GridRow /><GridRow /><GridRow /></Set>
        public override string TransformStyle(Style style)
        {
            // Custom CSS properties for Grid. ATM -ms- seems to be the only one. 
            var cssBuilder = new StringBuilder();
            cssBuilder.AppendLine("padding: 0px;");
            cssBuilder.AppendLine("display: -ms-grid;");

            cssBuilder.AppendLine("display: -webkit-flex;");
            // Get our default Drawable CSS class-level properties too. 
            var cssResult = base.TransformStyle(style);
            cssBuilder.AppendLine(cssResult);

            return cssBuilder.ToString();
        }

        public override void TransformHtml(Generator generator, Drawable element, HtmlNode node)
        {
            var document = node.OwnerDocument;
            var gridContainer = document.CreateElement("div");
            var style = element.Style;

            // TODO: Move this to GeneratorBlock
            if (style != null)
                gridContainer.Attributes.Add(document.CreateAttribute("class", style));

            // Add our created element to the parent.
            node.AppendChild(gridContainer);

            
            // IMPORTANT: Unless you know better, always call this at the end. Otherwise the Content of this
            // node will NOT be turned into HTML.
            base.TransformHtml(generator, element, gridContainer);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Pantheon.Core;

namespace Pantheon.Compiler.Core.Generators
{
    /// <summary>
    /// The Generator is the heart and soul of the Pantheon Compiler.
    /// Here is where HTML and CSS is generated.
    /// </summary>
    public sealed class Generator
    {
        /// <summary>
        /// Mapping of Types to GeneratorBlocks
        /// This is what tells the Generator that "Button" in PantheonCorLib corresponds to ButtonGeneratorBlock for example.
        /// It is super easy to override the mappings and customize HTML output.
        /// </summary>
        private IDictionary<Type, GeneratorBlock> generatorMapping = new Dictionary<Type, GeneratorBlock>();

        /// <summary>
        /// Generates HTML from a Pantheon Drawable node.
        /// </summary>
        /// <param name="node">The parent HTML node.</param>
        /// <param name="drawable">The drawable to generate HTML from.</param>
        public void Generate(HtmlNode node, Drawable drawable)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (drawable == null)
                throw new ArgumentNullException("drawable");

            if (generatorMapping.ContainsKey(drawable.GetType()) && node != null)
                generatorMapping[drawable.GetType()].TransformHtml(this, drawable, node);
        }

        // TODO: Don't hardcode Page? Ugh.
        public string GenerateStyles(Page p)
        {
            if (p == null)
                throw new ArgumentNullException("p");

            var cssBuilder = new StringBuilder();

            // TODO: Figure out a way to have a Style apply to Page's node. 
            // Probably doable once PageGeneratorBlock is up and running.
            cssBuilder.AppendLine("body,html { height: 100%; width: 100%; display: -webkit-flex; display: -ms-flexbox; background-color: #CCCCCC; font-family: Segoe UI;  }");

            // Go through all of the Page's Resources and generate the appropriate CSS
            foreach (var resource in p.Resources.Values)
            {
                if (resource is Style)
                {
                    var style = (Style)resource;

                    // Since more than one element can have a style; use the CSS class syntax.
                    cssBuilder.AppendLine(string.Format(".{0} {{", resource.Name));

                    GeneratorBlock block;

                    // Check to see if we have a GeneratorBlock for the Type, if we do call TransformStyle on it.
                    if (generatorMapping.TryGetValue(style.For, out block))
                    {
                        var cssResult = block.TransformStyle(style);

                        if (!string.IsNullOrEmpty(cssResult))
                            cssBuilder.AppendLine(string.Format("{0}}}", cssResult));
                    }
                }
            }

            // After all Styles have been transformed into CSS, return the final string.
            return cssBuilder.ToString();
        }

        /// <summary>
        /// Maps a Pantheon drawable to a GeneratorBlock
        /// </summary>
        /// <typeparam name="T">The Pantheon drawable.</typeparam>
        /// <typeparam name="S">The GeneratorBlock.</typeparam>
        public void Map<T, S>()
            where T : Drawable
            where S : GeneratorBlock, new()
        {
            if (generatorMapping.ContainsKey(typeof(T)))
                generatorMapping[typeof(T)] = new S();
            else
                generatorMapping.Add(typeof(T), new S());
        }
    }
}

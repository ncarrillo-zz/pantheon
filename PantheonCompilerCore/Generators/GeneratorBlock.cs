using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using HtmlAgilityPack;
using Pantheon.Core;

namespace Pantheon.Compiler.Core.Generators
{
    /// <summary>
    /// The GeneratorBlock is an object that corresponds to a Pantheon Drawable.
    /// You set up their relationship by using Generator.Map of T,S where T is a Drawable and S is a GeneratorBlock.
    /// For every Drawable in Pantheon, there is a GeneratorBlock that transforms the object into HTML/CSS
    /// </summary>
    public abstract class GeneratorBlock
    {
        /// <summary>
        /// Given a Style, TransformStyle produces a CSS class.
        /// Implementers can override this to customize CSS output on a per-object basis.
        /// Unless you know better, you should call this base method from an overriden one. 
        /// </summary>
        /// <param name="style">The target Style</param>
        /// <returns>The CSS class body, in string form.</returns>
        public virtual string TransformStyle(Style style)
        {
            var cssBuilder = new StringBuilder();

            // Some default CSS properties
            cssBuilder.AppendLine("overflow: auto;");
            cssBuilder.AppendLine("display: -webkit-flex;");
            cssBuilder.AppendLine("display: -ms-flexbox;");
            cssBuilder.AppendLine("-webkit-flex: 1 0 auto;");
            cssBuilder.AppendLine("-ms-flex: 1 0 auto;");

            // Go through all of our Setters and transform them too.
            foreach (var set in style.Setters.Values)
            {
                var cssResult = TransformSet(set);

                if (!string.IsNullOrEmpty(cssResult))
                {
                    cssBuilder.AppendLine(cssResult);
                }
            }

            return cssBuilder.ToString();
        }


        /// <summary>
        /// Given a Set, produces a CSS property. 
        /// Implementers can override this method to customize support for CSS properties given a Set.
        /// For example if you want a Setter of "Foo" to produce a CSS property "Bar", you can filter it here.
        /// You must call this methods for Setter properties you don't wish to filter, so that the default implementation
        /// can generate CSS.
        /// </summary>
        /// <param name="set">The target Set.</param>
        /// <returns>The CSS property, in string form.</returns>
        public virtual string TransformSet(Set set)
        {
            // TODO: Some more type checking. I really don't like this entire method.
            switch (set.Property.ToLower())
            {
                case "width":
                    int widthResult;

                    if (int.TryParse(set.To, out widthResult))
                        return string.Format("width: {0}px;", widthResult);

                    break;
                case "height":
                    int heightResult;

                    if (int.TryParse(set.To, out heightResult))
                        return string.Format("height: {0}px;", heightResult);

                    break;
                case "background":
                    return string.Format("background-color: {0};", set.To);

                case "foreground":
                    return string.Format("color: {0};", set.To);

                case "padding":
                    return string.Format("padding: {0}px;", set.To);

                case "margin":
                    return string.Format("margin: {0}px;", set.To);

            }

            return string.Empty;
        }

        /// <summary>
        /// Given a Pantheon Drawable, produces a node of HTML.
        /// </summary>
        /// <param name="generator">The current Generator.</param>
        /// <param name="element">The current Pantheon Drawable.</param>
        /// <param name="node">The current node. NOTE: If you're calling base.TransformHtml(..) be sure to call it
        /// with a child node you create using the HTMLAgility document. See: ButtonGeneratorBlock.</param>
        public virtual void TransformHtml(Generator generator, Drawable element, HtmlNode node)
        {
            // Do some reflection to get the [ContentProperty] from a Drawable and call Generator.Generate(..) on it.
            var axtt = element.GetType().CustomAttributes.Where(a => a.AttributeType == typeof(ContentPropertyAttribute)).SingleOrDefault();
            if (axtt.ConstructorArguments.Count > 0)
            {
                // Get the Property name by scanning the Attribute for a Constuctor arg.
                var propName = (string)axtt.ConstructorArguments[0].Value;

                // Make sure our element actually contains the Property.
                // TODO: Does ContentPropertyAttribute make this check redundant? 
                if (element.GetType().GetProperty(propName) != null)
                {
                    // TODO: Cache to reduce the double check on GetProperty(..)
                    var returnedType = element.GetType().GetProperty(propName).GetValue(element);

                    // If the type is a Drawable call generate.
                    // There will be cases where its an IList<...> but that's up to the implementer to deal with
                    // See StackPanelGeneratorBlock
                    if (returnedType is Drawable)
                        generator.Generate(node, (Drawable)returnedType);
                }
            }
        }
    }
}

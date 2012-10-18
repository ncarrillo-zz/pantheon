using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

[assembly: XmlnsDefinition("Pantheon/v1", "Pantheon.Core")]
namespace Pantheon.Core
{
    /// <summary>
    /// The Root Page object in Pantheon.
    /// Page is treated specially by the compiler. Page must be the root node.
    /// No other node can contain Resources other than Page. 
    /// </summary>
    [ContentProperty("Content")]
    public class Page : Drawable
    {
        public Drawable Content { get; set; }
        public IDictionary<string, Resource> Resources { get; set; }

        public string Title
        {
            get;
            set;
        }

        public Page()
        {
            Resources = new Dictionary<string, Resource>();
        }    
    }
}

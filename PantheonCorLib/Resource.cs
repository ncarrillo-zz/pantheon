using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Pantheon.Core
{
    /// <summary>
    /// Represents a Script in Pantheon. TODO.
    /// </summary>
    public class Script : Resource
    {
        public string Uri { get; set; }
        public string Type { get; set; }
    }

    /// <summary>
    /// Base Resource class. TODO: Flesh this out.
    /// </summary>
    [DictionaryKeyProperty("Name")]
    public abstract class Resource : Element
    {
    }
}

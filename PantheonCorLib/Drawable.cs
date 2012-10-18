using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantheon.Core
{
    /// <summary>
    /// Base class for everything Drawable in Pantheon. 
    /// Extend this to support custom XAML nodes that draw something on screen.
    /// </summary>
    public abstract class Drawable : Element
    {
        public string Style { get; set; }
        public Source Source { get; set; }
    }
}

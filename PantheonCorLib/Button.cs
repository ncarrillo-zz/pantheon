using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace Pantheon.Core
{
    /// <summary>
    /// Represents a simple Button in Panthon.
    /// </summary>
    [ContentProperty("Text")]
    public class Button : Drawable
    {
        public Drawable Text { get; set; }
    }
}

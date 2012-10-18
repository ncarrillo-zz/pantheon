using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantheon.Core
{
    /// <summary>
    /// Base XAML node in Pantheon. This should almost never be derived directly.
    /// Derive from other fundamental objects like Resource or Drawable instead.
    /// </summary>
    public abstract class Element
    {
        public string Name { get; set; }
    }
}

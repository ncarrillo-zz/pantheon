using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Pantheon.Core
{
    /// <summary>
    /// Arranges items in a horizontal or vertical stack with proportional sizing.
    /// </summary>
    [ContentProperty("Children")]
    public class StackPanel : Drawable
    {
        public IList<Drawable> Children { get; set; }

        public StackPanel()
        {
            Children = new List<Drawable>();
        }
    }
}

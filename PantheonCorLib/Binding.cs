using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace Pantheon.Core
{
    /// <summary>
    /// Represents a path on a Knockout.js ViewModel to bind to.
    /// TODO: Flesh this out.
    /// </summary>
    public class Binding
    {
        public string Path { get; set; }
    }

    /// <summary>
    /// Represents a Binding source.
    /// </summary>
    public class Source : Resource
    {
        /// <summary>
        /// The Knockout.js ViewModel to bind to.
        /// </summary>
        public string ViewModel
        {
            get;
            set;
        }
    }
}

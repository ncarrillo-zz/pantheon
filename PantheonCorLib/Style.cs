using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace Pantheon.Core
{
    /// <summary>
    /// A special type of Resource that defines the visual representation of a Pantheon Drawable.
    /// </summary>
    [ContentProperty("Setters")]
    public class Style : Resource
    {
        [TypeConverter(typeof(StringToTypeConverter))]
        public Type For { get; set; }

        public IDictionary<string, Set> Setters { get; set; }

        public Style()
        {
            Setters = new Dictionary<string, Set>();
        }
    }

    /// <summary>
    /// A Setter within a Style. 
    /// </summary>
    [ContentProperty("To")]
    [DictionaryKeyProperty("Property")]
    public class Set
    {
        public string Property { get; set; }
        public string To { get; set; }
    }
    
    /// <summary>
    /// Converts a string to a type. TODO: Do more of this within the object model.
    /// </summary>
    public class StringToTypeConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // TODO: Don't hardcode this.
            return Type.GetType(string.Format("Pantheon.Core.{0}", (string)value));
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return false;
        }
    }
}

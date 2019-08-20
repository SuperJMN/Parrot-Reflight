using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflight.Universal.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DefaultSettingValueAttribute : Attribute
    {
        public DefaultSettingValueAttribute(object value)
        {
            Value = value;
        }

        public object Value { get; set; }
    }
}

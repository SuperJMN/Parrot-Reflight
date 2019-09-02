using System;

namespace Zafiro.Uwp.Core
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

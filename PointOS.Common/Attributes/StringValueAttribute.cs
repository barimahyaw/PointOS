using System;

namespace PointOS.Common.Attributes
{
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; }
        public StringValueAttribute(string value)
        {
            StringValue = value;
        }
    }
}
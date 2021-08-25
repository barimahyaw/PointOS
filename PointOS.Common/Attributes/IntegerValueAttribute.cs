using System;

namespace PointOS.Common.Attributes
{
    public class IntegerValueAttribute : Attribute
    {
        public int IntegerValue { get; }
        public IntegerValueAttribute(int value)
        {
            IntegerValue = value;
        }
    }
}

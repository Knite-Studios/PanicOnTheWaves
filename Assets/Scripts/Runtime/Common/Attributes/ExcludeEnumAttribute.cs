using System;
using NaughtyAttributes;

namespace Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ExcludeEnumAttribute : ValidatorAttribute
    {
        public int[] ExcludedValues { get; private set; }

        public ExcludeEnumAttribute(params int[] excludedValues)
        {
            ExcludedValues = excludedValues;
        }
    }
}
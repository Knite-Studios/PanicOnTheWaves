using System;
using NaughtyAttributes;

namespace Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RequiredEnumAttribute : ValidatorAttribute
    {
        public string Message { get; private set; }
        public int ExcludedValue { get; private set; }

        public RequiredEnumAttribute(int excludedValue, string message = null)
        {
            ExcludedValue = excludedValue;
            Message = message;
        }
    }
}
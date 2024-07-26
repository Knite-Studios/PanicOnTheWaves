using System;

namespace Common.Utils
{
    public static class MathUtilities
    {
        /// <summary>
        /// Attempts to reflectively add two values.
        /// </summary>
        public static T Add<T>(T a, T b)
        {
            var type = typeof(T);
            return type.GetMethod("op_Addition") != null
                ? (T) type.GetMethod("op_Addition")!
                    .Invoke(null, new object[] { a, b })
                : throw new NotSupportedException("Addition is not supported for this type");
        }

        /// <summary>
        /// Attempts to reflectively multiply two values.
        /// </summary>
        public static T Multiply<T>(T a, T b)
        {
            var type = typeof(T);
            return type.GetMethod("op_Multiply") != null
                ? (T) type.GetMethod("op_Multiply")!
                    .Invoke(null, new object[] { a, b })
                : throw new NotSupportedException("Multiplication is not supported for this type");
        }
    }
}
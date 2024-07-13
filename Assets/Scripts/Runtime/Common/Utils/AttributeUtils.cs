using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Utils
{
    public static partial class Utils
    {
        /// <summary>
        /// Fetches all types with the attribute specified.
        /// </summary>
        public static List<Type> GetTypesFor<T>()
            where T : Attribute
        {
            var allTypes =
                from type in Assembly.GetExecutingAssembly().GetTypes()
                where type.IsDefined(typeof(T), false)
                select type;
            return allTypes.ToList();
        }
    }
}
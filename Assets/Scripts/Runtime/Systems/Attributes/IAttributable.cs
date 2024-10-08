using System;
using System.Collections.Generic;

namespace Systems.Attributes
{
    /// <summary>
    /// Interface for entities that have attributes.
    /// </summary>
    public interface IAttributable
    {
        /// <summary>
        /// Dictionary to hold attributes with different types.
        /// </summary>
        Dictionary<GameAttribute, object> Attributes { get; }
    }

    /// <summary>
    /// Methods for the IAttributable interface.
    /// </summary>
    public static class AttributableMethods
    {
        /// <summary>
        /// Gets the value of an attribute.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="obj">The attributable object.</param>
        /// <param name="gameAttribute">The attribute type.</param>
        /// <returns>The calculated value of the attribute.</returns>
        public static T GetAttributeValue<T>(this IAttributable obj, GameAttribute gameAttribute)
            where T : struct, IComparable, IConvertible, IFormattable
        {
            return obj.GetOrCreateAttribute<T>(gameAttribute)?.Value ?? default;
        }

        /// <summary>
        /// Sets the base value of an attribute.
        /// </summary>
        /// <param name="obj">The attributable object.</param>
        /// <param name="gameAttribute">The attribute type.</param>
        /// <param name="value">The new base value.</param>
        public static void SetAttributeValue<T>(this IAttributable obj, GameAttribute gameAttribute, T value)
            where T : struct, IComparable, IConvertible, IFormattable
        {
            var val = obj.GetOrCreateAttribute<T>(gameAttribute);
            val.BaseValue = value;
        }

        /// <summary>
        /// Checks if the object has an attribute.
        /// </summary>
        /// <param name="obj">The attributable object.</param>
        /// <param name="gameAttribute">The attribute type.</param>
        /// <returns>True if the attribute is on the object.</returns>
        public static bool HasAttribute(this IAttributable obj, GameAttribute gameAttribute)
        {
            return obj.Attributes.ContainsKey(gameAttribute);
        }

        /// <summary>
        /// Gets or creates an attribute instance.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="obj">The attributable object.</param>
        /// <param name="gameAttribute">The attribute type.</param>
        /// <param name="defaultValue">The default value if the attribute doesn't exist.</param>
        /// <returns>The attribute instance.</returns>
        public static AttributeInstance<T> GetOrCreateAttribute<T>(
            this IAttributable obj,
            GameAttribute gameAttribute,
            T defaultValue = default)
            where T : struct, IComparable, IConvertible, IFormattable
        {
            if (obj.Attributes.TryGetValue(gameAttribute, out var instance))
            {
                return instance as AttributeInstance<T>;
            }

            var newInstance = new AttributeInstance<T> { BaseValue = defaultValue };
            obj.Attributes[gameAttribute] = newInstance;

            return newInstance;
        }
    }
}

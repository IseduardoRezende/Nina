using System.Reflection;

namespace Nina.SourceCode
{
    /// <summary>
    /// Provides extension methods for the class <see cref="Type"/>. 
    /// </summary>    
    internal static class TypeExtensions
    {
        /// <summary>
        /// Sets a <paramref name="propertyValue"/> for an property with <paramref name="propertyName"/>.
        /// </summary>
        /// <typeparam name="T">Any type with default constructor.</typeparam>
        /// <typeparam name="TProperty">The type of the property to be set.</typeparam>
        /// <param name="type">The <see cref="Type"/> of <typeparamref name="T"/></param>.
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyValue">The property value to be set.</param>
        /// <param name="obj">The <typeparamref name="T"/> object.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        internal static void SetProperty<T, TProperty>(this Type type, string propertyName, TProperty propertyValue, T obj)
            where T : new()
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type), "The type must not be null.");

            if (obj is null)
                throw new ArgumentNullException(nameof(obj), "The obj must not be null.");

            if (!IsSameType<T>(type))
                throw new ArgumentException($"The type '{type.Name}' must match type '{obj.GetType().Name}'.", nameof(type));

            var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (property is null)
                throw new ArgumentException($"Property '{propertyName}' not found in type '{type.Name}'.", nameof(propertyName));

            if (!IsSettableProperty(property))
                throw new ArgumentException($"The property '{propertyName}' must be settable.", nameof(propertyName));

            if (!IsValidPropertyValue(property, propertyValue))
                throw new ArgumentException($"The property value '{propertyValue}' is not valid for property '{propertyName}' with type '{property}'.", nameof(propertyValue));

            property.SetValue(obj, propertyValue);
        }

        /// <summary>
        /// Indicates whether the <paramref name="property"/> is settable.
        /// </summary>
        /// <param name="property">The property to be checked.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="property"/> is settable; otherwise, <see langword="false"/>.
        /// </returns>        
        private static bool IsSettableProperty(PropertyInfo property)
        {
            return property is { CanWrite: true };
        }

        /// <summary>
        /// Indicates whether the <paramref name="propertyValue"/> is valid for <paramref name="property"/>.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to be set.</typeparam>
        /// <param name="property">The target property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <returns>
        ///  <see langword="true"/> if <paramref name="propertyValue"/> is valid for <paramref name="property"/>; otherwise, <see langword="false"/>.
        /// </returns>
        private static bool IsValidPropertyValue<TProperty>(PropertyInfo property, TProperty propertyValue)
        {
            if (!IsNullableProperty(property) && propertyValue is null)
                return false;

            return property.PropertyType.IsAssignableFrom(typeof(TProperty));
        }

        /// <summary>
        /// Indicates whether the <paramref name="property"/> is <see cref="NullabilityState.Nullable"/>.
        /// </summary>
        /// <param name="property">The property to be checked.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="property"/> is <see cref="NullabilityState.Nullable"/>; otherwise, <see langword="false"/>.
        /// </returns>
        private static bool IsNullableProperty(PropertyInfo property)
        {
            return new NullabilityInfoContext().Create(property).ReadState is NullabilityState.Nullable;
        }

        /// <summary>
        /// Indicates whether the specified <paramref name="type"/> is type of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Any type with default constructor.</typeparam>
        /// <param name="type">The <see cref="Type"/> of the class to check for equality of types.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="type"/> is type of <typeparamref name="T"/>; otherwise, <see langword="false"/>.
        /// </returns>
        private static bool IsSameType<T>(this Type type)
            where T : new()
        {
            return type == typeof(T);
        }
    }
}

using System.Reflection;

namespace Nina.SourceCode
{
    /// <summary>
    /// Provides extension methods for the class <see cref="Type"/>. 
    /// </summary>    
    internal static class TypeExtensions
    {
        /// <summary>
        /// Set a <paramref name="propertyValue"/> for the <typeparamref name="TProperty"/> with <paramref name="propertyName"/>.
        /// </summary>
        /// <typeparam name="T">Any type with default constructor.</typeparam>
        /// <typeparam name="TProperty">The property to be set.</typeparam>
        /// <param name="type">The <see cref="Type"/> of <typeparamref name="T"/></param>.
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyValue">The property value to be set.</param>
        /// <param name="t">The <typeparamref name="T"/> object.</param>
        /// <exception cref="ArgumentException"></exception>
        internal static void SetProperty<T, TProperty>(this Type type, string propertyName, TProperty propertyValue, T t)
            where T : new()
        {            
            if (!IsSettableProperty(type, propertyName))
                throw new ArgumentException("Property must be public and settable.");

            type.GetProperty(propertyName)!.SetValue(t, propertyValue);
        }

        /// <summary>
        /// Indicates whether the specified <paramref name="type"/> contains a settable property with <paramref name="propertyName"/> and <paramref name="bindingFlags"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> of the class to check for a settable property.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="bindingFlags">The binding flags to use for reflection when searching for the property.</param>
        /// <returns><see langword="true"/> if <paramref name="type"/> contains a settable property with <paramref name="propertyName"/> and <paramref name="bindingFlags"/>; otherwise, <see langword="false"/>.</returns>        
        internal static bool IsSettableProperty(this Type type, string propertyName, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance)
        {            
            var propertyInfo = type.GetProperty(propertyName, bindingFlags);
            return propertyInfo is { CanWrite: true };
        }
    }
}

using System.Linq.Expressions;

namespace Nina.SourceCode
{
    /// <summary>
    /// Provides extension methods for the class <see cref="Expression{TDelegate}"/>.
    /// </summary>
    internal static class Expression_TDelegate_Extensions
    {
        /// <summary>
        /// Gets the name of the <typeparamref name="TProperty"/> by <see cref="Expression"/>.
        /// </summary>
        /// <typeparam name="T">Any type with default constructor.</typeparam>
        /// <typeparam name="TProperty">The defined property.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>The <typeparamref name="TProperty"/> name.</returns>
        /// <exception cref="InvalidCastException"></exception>
        internal static string GetPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> property)
            where T : new()
        {            
            var memberExpression = property.Body as MemberExpression;

            if (memberExpression is null) 
                throw new InvalidCastException($"Invalid expression for {typeof(T).Name}.");

            return memberExpression!.Member.Name;
        }

        /// <summary>
        /// Gets the name of the <typeparamref name="TProperty"/> by <see cref="Expression"/>.
        /// </summary>
        /// <typeparam name="T">Any type with default constructor.</typeparam>
        /// <typeparam name="TProperty">The defined property.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>The <typeparamref name="TProperty"/> name.</returns>
        /// <exception cref="InvalidCastException"></exception>
        internal static string GetPropertyName<T, TProperty>(this Expression<Func<T, ICollection<TProperty>>> property)
            where T : new()
        {            
            var memberExpression = property.Body as MemberExpression;

            if (memberExpression is null)
                throw new InvalidCastException($"Invalid expression for {typeof(T).Name}.");

            return memberExpression!.Member.Name;
        }

        /// <summary>
        /// Gets the name of the <typeparamref name="TProperty"/> by <see cref="Expression"/>.
        /// </summary>
        /// <typeparam name="T">Any type with default constructor.</typeparam>
        /// <typeparam name="TProperty">The defined property.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>The <typeparamref name="TProperty"/> name.</returns>
        /// <exception cref="InvalidCastException"></exception>
        internal static string GetPropertyName<T, TProperty>(this Expression<Func<T, ICollection<ICollection<TProperty>>>> property)
            where T : new()
        {
            var memberExpression = property.Body as MemberExpression;

            if (memberExpression is null)
                throw new InvalidCastException($"Invalid expression for {typeof(T).Name}.");

            return memberExpression!.Member.Name;
        }
    }
}

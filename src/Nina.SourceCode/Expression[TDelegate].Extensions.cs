using System.Linq.Expressions;

namespace Nina.SourceCode
{
    /// <summary>
    /// Provides extension methods for the class <see cref="Expression{TDelegate}"/>.
    /// </summary>
    internal static class Expression_TDelegate_Extensions
    {
        /// <summary>
        /// Gets a property name of <typeparamref name="T"/> using <paramref name="expression"/>.
        /// </summary>
        /// <typeparam name="T">Any type with default constructor.</typeparam>
        /// <typeparam name="TProperty">The type of the property to be set.</typeparam>
        /// <param name="expression">The expression containing the property.</param>
        /// <returns>The property name of type <typeparamref name="TProperty"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        internal static string GetPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> expression)
            where T : new()
        {            
            if (expression is null)
                throw new ArgumentNullException(nameof(expression), "The expression must not be null.");

            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression is null) 
                throw new InvalidCastException($"Invalid expression for {typeof(T).Name}.");

            return memberExpression!.Member.Name;
        }

        /// <summary>
        /// Gets a property name of <typeparamref name="T"/> using <paramref name="expression"/>.
        /// </summary>
        /// <typeparam name="T">Any type with default constructor.</typeparam>
        /// <typeparam name="TProperty">The type of the property to be set.</typeparam>
        /// <param name="expression">The expression containing the property.</param>
        /// <returns>The property name of type ICollection&lt;<typeparamref name="TProperty"/>&gt;.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        internal static string GetPropertyName<T, TProperty>(this Expression<Func<T, ICollection<TProperty>>> expression)
            where T : new()
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression), "The expression must not be null.");

            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression is null)
                throw new InvalidCastException($"Invalid expression for {typeof(T).Name}.");

            return memberExpression!.Member.Name;
        }

        /// <summary>
        /// Gets a property name of <typeparamref name="T"/> using <paramref name="expression"/>.
        /// </summary>
        /// <typeparam name="T">Any type with default constructor.</typeparam>
        /// <typeparam name="TProperty">The type of the property to be set.</typeparam>
        /// <param name="expression">The expression containing the property.</param>
        /// <returns>The property name of type ICollection&lt;ICollection&lt;<typeparamref name="TProperty"/>&gt;&gt;.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        internal static string GetPropertyName<T, TProperty>(this Expression<Func<T, ICollection<ICollection<TProperty>>>> expression)
            where T : new()
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression), "The expression must not be null.");

            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression is null)
                throw new InvalidCastException($"Invalid expression for {typeof(T).Name}.");

            return memberExpression!.Member.Name;
        }
    }
}

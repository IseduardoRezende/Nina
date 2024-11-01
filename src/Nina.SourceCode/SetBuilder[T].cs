using System.Linq.Expressions;

namespace Nina.SourceCode
{
    /// <summary>
    /// Represents a generic fluent builder class for building objects.
    /// </summary>
    /// <typeparam name="T">Any type with default constructor.</typeparam>
    public class SetBuilder<T> where T : new()
    {
        /// <summary>
        /// The <typeparamref name="T"/> object.
        /// </summary>
        protected readonly T _t;

        /// <summary>
        /// Initializes a new instance of <see cref="SetBuilder{T}"/> class.
        /// </summary>
        public SetBuilder()
        {
            _t = new();
        }

        /// <summary>
        /// Builds the <typeparamref name="T"/> object.
        /// </summary>
        /// <returns>The <typeparamref name="T"/> object.</returns>
        public virtual T Build()
        {
            return _t;
        }

        /// <summary>
        /// Assigns a value to the specified property of the current object being built.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to be set.</typeparam>
        /// <param name="property">
        /// An expression that identifies the property to be set, such as <c>x => x.PropertyName</c>.
        /// </param>
        /// <param name="setter">
        /// A delegate that provides the value to set for the property. The current value (default if not set) is passed as input and the new value is returned.
        /// </param>
        /// <returns>
        /// Returns the current <see cref="SetBuilder{T}"/> instance to allow for method chaining.
        /// </returns>
        public SetBuilder<T> WithProperty<TProperty>(Expression<Func<T, TProperty>> property, Func<TProperty, TProperty> setter)
        {
            var propertyName = property.GetPropertyName();
            var propertyValue = setter.Invoke(default!);
            typeof(T).SetProperty(propertyName, propertyValue, _t);
            return this;
        }

        /// <summary>
        /// Assigns a value to the specified property of the current object being built.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to be set.</typeparam>
        /// <param name="property">
        /// An expression that identifies the property to be set, such as <c>x => x.PropertyName</c>.
        /// </param>
        /// <param name="setter">
        /// The value to set for the property.
        /// </param>
        /// <returns>
        /// Returns the current <see cref="SetBuilder{T}"/> instance to allow for method chaining.
        /// </returns>
        public SetBuilder<T> WithProperty<TProperty>(Expression<Func<T, TProperty>> property, TProperty setter)
        {
            var propertyName = property.GetPropertyName();
            typeof(T).SetProperty(propertyName, setter, _t);
            return this;
        }

        /// <summary>
        /// Assigns a value to the specified property of the current object being built.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to be set.</typeparam>
        /// <param name="property">
        /// An expression that identifies the property to be set, such as <c>x => x.PropertyName</c>.
        /// </param>
        /// <param name="setter">
        /// A delegate that provides the value to set for the property. The current value (default if not set) is passed as input and the new value is returned.
        /// </param>
        /// <returns>
        /// Returns the current <see cref="SetBuilder{T}"/> instance to allow for method chaining.
        /// </returns>
        public SetBuilder<T> WithProperty<TProperty>(Expression<Func<T, ICollection<TProperty>>> property, Action<ICollection<Func<TProperty, TProperty>>> setter)
        {
            var propertyName = property.GetPropertyName();

            var collection = new List<Func<TProperty, TProperty>>();
            setter.Invoke(collection);

            var propertyValue = collection.Select(c =>
            {
                return c.Invoke(default!);
            }).ToList();

            typeof(T).SetProperty(propertyName, propertyValue, _t);
            return this;
        }

        /// <summary>
        /// Assigns a value to the specified property of the current object being built.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to be set.</typeparam>
        /// <param name="property">
        /// An expression that identifies the property to be set, such as <c>x => x.PropertyName</c>.
        /// </param>
        /// <param name="setter">
        /// The value to set for the property.
        /// </param>
        /// <returns>
        /// Returns the current <see cref="SetBuilder{T}"/> instance to allow for method chaining.
        /// </returns>
        public SetBuilder<T> WithProperty<TProperty>(Expression<Func<T, ICollection<TProperty>>> property, ICollection<TProperty> setter)
        {
            var propertyName = property.GetPropertyName();
            typeof(T).SetProperty(propertyName, setter, _t);
            return this;
        }

        /// <summary>
        /// Assigns a value to the specified property of the current object being built.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to be set.</typeparam>
        /// <param name="property">
        /// An expression that identifies the property to be set, such as <c>x => x.PropertyName</c>.
        /// </param>
        /// <param name="setter">
        /// A delegate that provides the value to set for the property. The current value (default if not set) is passed as input and the new value is returned.
        /// </param>
        /// <returns>
        /// Returns the current <see cref="SetBuilder{T}"/> instance to allow for method chaining.
        /// </returns>
        public SetBuilder<T> WithProperty<TProperty>(Expression<Func<T, ICollection<ICollection<TProperty>>>> property, Action<ICollection<Action<ICollection<Func<TProperty, TProperty>>>>> setter)
        {
            var propertyName = property.GetPropertyName();

            var collections = new List<Action<ICollection<Func<TProperty, TProperty>>>>();
            setter.Invoke(collections);

            var propertyValue = collections.Select(c =>
            {
                var internCollections = new List<Func<TProperty, TProperty>>();

                c.Invoke(internCollections);

                return (ICollection<TProperty>)internCollections.Select(f =>
                {
                    return f.Invoke(default!);
                }).ToList();
            }).ToList();

            typeof(T).SetProperty(propertyName, propertyValue, _t);
            return this;
        }

        /// <summary>
        /// Assigns a value to the specified property of the current object being built.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to be set.</typeparam>
        /// <param name="property">
        /// An expression that identifies the property to be set, such as <c>x => x.PropertyName</c>.
        /// </param>
        /// <param name="setter">The value to set for the property.</param>
        /// <returns>
        /// Returns the current <see cref="SetBuilder{T}"/> instance to allow for method chaining.
        /// </returns>
        public SetBuilder<T> WithProperty<TProperty>(Expression<Func<T, ICollection<ICollection<TProperty>>>> property, ICollection<ICollection<TProperty>> setter)
        {
            var propertyName = property.GetPropertyName();
            typeof(T).SetProperty(propertyName, setter, _t);
            return this;
        }
    }
}

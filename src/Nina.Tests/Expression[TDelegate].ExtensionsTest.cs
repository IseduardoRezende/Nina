using Nina.SourceCode;
using Nina.Tests.Models;
using System.Linq.Expressions;

namespace Nina.Tests
{
    public class Expression_TDelegate_ExtensionsTest
    {
        [Fact]
        public void GetPropertyName_WhenInvoked_GetsThePropertyNameCorrectly()
        {
            Expression<Func<Person, string>> expression = p => p.Name;
            string expectedPropertyName = "Name";

            string actualPropertyName = Expression_TDelegate_Extensions.GetPropertyName(expression);

            Assert.Equal(expectedPropertyName, actualPropertyName);
        }

        [Fact]
        public void GetPropertyName_WhenInvokedUsingCollection_GetsThePropertyNameCorrectly()
        {
            Expression<Func<Product, ICollection<Product>>> expression = p => p.DerivedProducts;
            string expectedPropertyName = "DerivedProducts";

            string actualPropertyName = Expression_TDelegate_Extensions.GetPropertyName(expression);

            Assert.Equal(expectedPropertyName, actualPropertyName);
        }

        [Fact]
        public void GetPropertyName_WhenInvokedUsingNestedCollection_GetsThePropertyNameCorrectly()
        {
            Expression<Func<Producer, ICollection<ICollection<Person>>>> expression = p => p.EmployeesBySector;
            string expectedPropertyName = "EmployeesBySector";

            string actualPropertyName = Expression_TDelegate_Extensions.GetPropertyName(expression);

            Assert.Equal(expectedPropertyName, actualPropertyName);
        }

        [Fact]
        public void GetPropertyName_WhenNullExpression_ThrowsArgumentNullException()
        {
            Expression<Func<Person, string>> expression = null!;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = Expression_TDelegate_Extensions.GetPropertyName(expression);
            });
        }

        [Fact]
        public void GetPropertyName_WhenNullExpressionUsingCollection_ThrowsArgumentNullException()
        {
            Expression<Func<Product, ICollection<Product>>> expression = null!;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = Expression_TDelegate_Extensions.GetPropertyName(expression);
            });
        }

        [Fact]
        public void GetPropertyName_WhenNullExpressionUsingNestedCollection_ThrowsArgumentNullException()
        {
            Expression<Func<Producer, ICollection<ICollection<Person>>>> expression = null!;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = Expression_TDelegate_Extensions.GetPropertyName(expression);
            });
        }

        [Fact]
        public void GetPropertyName_WhenInvalidExpression_ThrowsInvalidCastException()
        {
            Expression<Func<Person, string>> expression = p => "Name";

            Assert.Throws<InvalidCastException>(() =>
            {
                _ = Expression_TDelegate_Extensions.GetPropertyName(expression);
            });
        }

        [Fact]
        public void GetPropertyName_WhenInvalidExpressionUsingCollection_ThrowsInvalidCastException()
        {
            Expression<Func<Product, ICollection<Product>>> expression = p => new List<Product>();

            Assert.Throws<InvalidCastException>(() =>
            {
                _ = Expression_TDelegate_Extensions.GetPropertyName(expression);
            });
        }

        [Fact]
        public void GetPropertyName_WhenInvalidExpressionUsingNestedCollection_ThrowsInvalidCastException()
        {
            Expression<Func<Producer, ICollection<ICollection<Person>>>> expression = p => new List<ICollection<Person>>();

            Assert.Throws<InvalidCastException>(() =>
            {
                _ = Expression_TDelegate_Extensions.GetPropertyName(expression);
            });
        }
    }
}

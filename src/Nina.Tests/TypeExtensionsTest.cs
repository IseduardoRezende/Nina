using Nina.SourceCode;
using Nina.Tests.Models;

namespace Nina.Tests
{
    public class TypeExtensionsTest
    {
        [Fact]
        public void SetProperty_WhenInvoked_SetsThePropertyCorrectly()
        {
            Person person = new();
            string expectedName = "Eduardo";

            TypeExtensions.SetProperty(typeof(Person), nameof(person.Name), expectedName, person);

            Assert.Equal(expectedName, person.Name);            
        }

        [Fact]
        public void SetProperty_WhenInvalidPropertyName_ThrowsArgumentException()
        {
            Person person = new();
            string personName = "Lívia";

            string invalidPropertyName = "InvalidPropertyName";

            Assert.Throws<ArgumentException>(() =>
            {
                TypeExtensions.SetProperty(typeof(Person), invalidPropertyName, personName, person);
            });
        }

        [Fact]
        public void SetProperty_WhenIncompatiblePropertyValue_ThrowsArgumentException()
        {
            Person person = new();
            char invalidPropertyValue = 'F';

            Assert.Throws<ArgumentException>(() =>
            {
                TypeExtensions.SetProperty(typeof(Person), nameof(person.Name), invalidPropertyValue, person);
            });
        }

        [Fact]
        public void SetProperty_WhenPropertyNotSettable_ThrowsArgumentException()
        {
            Person person = new();

            Assert.Throws<ArgumentException>(() =>
            {
                TypeExtensions.SetProperty(typeof(Person), nameof(person.Secret), "Afraid of dark", person);
            });
        }

        [Fact]
        public void SetProperty_WhenNullObject_ThrowsArgumentNullException()
        {
            Person person = null!;
            byte personAge = 19;

            Assert.Throws<ArgumentNullException>(() =>
            {
                TypeExtensions.SetProperty(typeof(Person), nameof(person.Age), personAge, person);
            });
        }

        [Fact]
        public void SetProperty_WhenIncompatibleType_ThrowsArgumentException()
        {
            Person person = new();
            byte personAge = 19;

            Assert.Throws<ArgumentException>(() =>
            {
                TypeExtensions.SetProperty(typeof(Product), nameof(person.Age), personAge, person);
            });
        }

        [Fact]
        public void SetProperty_WhenIncompatibleObject_ThrowsArgumentException()
        {
            Person person = new();
            Guid productCode = Guid.NewGuid();

            Assert.Throws<ArgumentException>(() =>
            {
                TypeExtensions.SetProperty(typeof(Product), "Code", productCode, person);
            });
        }
    }
}
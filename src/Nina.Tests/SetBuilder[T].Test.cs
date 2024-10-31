using AutoFixture;
using Nina.SourceCode;
using Nina.Tests.Models;

namespace Nina.Tests
{
    public class SetBuilderTest
    {
        private readonly Fixture _fixture;

        public SetBuilderTest()
        {
            _fixture = new();
        }

        [Fact]
        public void Build_WhenInvoked_BuildsTheObjectOfTypeT()
        {
            Person person = new SetBuilder<Person>().Build();

            Assert.IsType<Person>(person);
        }

        [Fact]
        public void WithProperty_WhenInvokedWithDelegate_AssignsValueToDefinedProperty()
        {
            decimal expectedValue = 120;

            Product product = new SetBuilder<Product>()
                .WithProperty(p => p.Value, _ => expectedValue)
                .Build();

            Assert.Equal(expectedValue, product.Value);
        }

        [Fact]
        public void WithProperty_WhenInvoked_AssignsValueToDefinedProperty()
        {
            decimal expectedValue = 120;

            Product product = new SetBuilder<Product>()
                .WithProperty(p => p.Value, expectedValue)
                .Build();

            Assert.Equal(expectedValue, product.Value);
        }

        [Fact]
        public void WithProperty_WhenInvokedUsingCollectionWithDelegate_AssignsValueToDefinedProperty()
        {
            Product productTest1 = _fixture.Build<Product>()
                .Without(p => p.DerivedProducts)
                .Create();

            Product productTest2 = _fixture.Build<Product>()
                .Without(p => p.DerivedProducts)
                .Create();

            List<Product> expectedDerivedProducts = [productTest1, productTest2];

            Product product = new SetBuilder<Product>()
                .WithProperty(p => p.DerivedProducts, dp =>
                {
                    dp.Add(_ => productTest1);
                    dp.Add(_ => productTest2);
                })
                .Build();

            Assert.Equal(expectedDerivedProducts, product.DerivedProducts);
        }

        [Fact]
        public void WithProperty_WhenInvokedUsingCollection_AssignsValueToDefinedProperty()
        {
            List<Product> expectedDerivedProducts = _fixture.Build<Product>()
                .Without(p => p.DerivedProducts)
                .CreateMany()
                .ToList();

            Product product = new SetBuilder<Product>()
                .WithProperty(p => p.DerivedProducts, expectedDerivedProducts)
                .Build();

            Assert.Equal(expectedDerivedProducts, product.DerivedProducts);
        }

        [Fact]
        public void WithProperty_WhenInvokedUsingNestedCollectionWithDelegate_AssignsValueToDefinedProperty()
        {
            Person personTest1 = _fixture.Create<Person>();
            Person personTest2 = _fixture.Create<Person>();

            Person personTest3 = _fixture.Create<Person>();
            Person personTest4 = _fixture.Create<Person>();

            List<Person> employeesIT = [personTest1, personTest2];
            List<Person> employeesADM = [personTest3, personTest4];

            List<ICollection<Person>> expectedEmployeesBySector =
            [
                employeesIT,
                employeesADM
            ];

            Producer producer = new SetBuilder<Producer>()
                .WithProperty(p => p.EmployeesBySector, ebs =>
                {
                    ebs.Add(e =>
                    {
                        e.Add(_ => personTest1);
                        e.Add(_ => personTest2);
                    });

                    ebs.Add(e =>
                    {
                        e.Add(_ => personTest3);
                        e.Add(_ => personTest4);
                    });
                })
                .Build();

            Assert.Equal(expectedEmployeesBySector, producer.EmployeesBySector);
        }

        [Fact]
        public void WithProperty_WhenInvokedUsingNestedCollection_AssignsValueToDefinedProperty()
        {
            List<ICollection<Person>> expectedEmployeesBySector = _fixture.Create<List<ICollection<Person>>>();

            Producer producer = new SetBuilder<Producer>()
                .WithProperty(p => p.EmployeesBySector, expectedEmployeesBySector)
                .Build();

            Assert.Equal(expectedEmployeesBySector, producer.EmployeesBySector);
        }
    }
}

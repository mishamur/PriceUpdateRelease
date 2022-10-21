namespace tests
{
    public class ProductTest
    {
        [Fact]
        public void TwoDifferentProductsShouldNotBeEquals()
        {
            var product1 = new Product("0", 0);
            var product2 = new Product("1", 0);
            Assert.False(product1.Equals(product2));
        }

        [Fact]
        public void TwoIdenticalProductsShouldBeEquals()
        {
            var product1 = new Product("0", 0);
            var product2 = new Product("0", 0);
            Assert.True(product1.Equals(product2));
        }
    }
}

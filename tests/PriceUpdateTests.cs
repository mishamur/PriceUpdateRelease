namespace tests
{
    public class PriceUpdateTests
    {
        [Fact]
        public void LengthExceptsTwoEqualsSetsShouldBeZero()
        {
            List<Product> newProducts = new List<Product> { new Product("молоко", 20), new Product("хлеб", 40) };
            List<Product> curProducts = new List<Product> { new Product("молоко", 20), new Product("хлеб", 40) };

            var difProducts = PriceUpdate.CompareProducts.GetDifferenceProductsPrice(newProducts, curProducts);
            
            Assert.Empty(difProducts);
        }
    }
}

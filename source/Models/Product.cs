namespace Models
{
    public class Product : IEquatable<Product>
    {
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Position { get; private set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal Price { get; private set; }

        public Product(string position, decimal price)
        {
            this.Position = position;
            this.Price = price;
        }

        public override string ToString()
        {
            return $"{Position} : {Price}";
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Product);  
          
        }

        public override int GetHashCode()
        {
            return 31 * Position.GetHashCode() + Price.GetHashCode();
        }

        public bool Equals(Product? obj)
        {
            if (obj == null) return false;
            else
                return obj.Position.Equals(this.Position) && obj.Price == this.Price;
        }
    }
}

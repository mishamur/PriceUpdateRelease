using Models;

namespace DbApi.api
{
    public class ProductsDb
    {
        private Action<string> logger;
        public ProductsDb(Action<string> logger = null)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Загружает список продуктов в базу 
        /// </summary>
        /// <param name="products">список продуктов</param>
        /// <param name="isDeleteDataFromProducts">удалить ли данные из базы?</param>
        public void LoadToProducts(IEnumerable<Product> products, bool isDeleteDataFromProducts = false)
        {
            using (ApplicationContext dbContext = new ApplicationContext())
            {
                if (products.Distinct().Count() > products.Count())
                {
                    logger?.Invoke("Переданные значения не уникальны, в базу пойдут только уникальные");
                }

                if (isDeleteDataFromProducts)
                    dbContext.Products.RemoveRange(dbContext.Products);

                //записываем только уникальные
                dbContext.Products.AddRange(products.Distinct());

                dbContext.SaveChanges();
                //логгировать
            }
        }

        /// <summary>
        /// Получить множество продуктов из базы данных
        /// </summary>
        /// <returns>множество продуктов</returns>
        public IEnumerable<Product> GetProducts()
        {
            using (ApplicationContext dbContext = new ApplicationContext())
            {
                return dbContext.Products.Select(x => x).ToList();
            }
        }
    }
}

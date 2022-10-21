using Models;

namespace PriceUpdate
{
    public class CompareProducts
    {
        /// <summary>
        /// Исключаем из нового множества продуктов, множество текущих продуктов 
        /// </summary>
        /// <param name="newProducts">Множество новых продуктов</param>
        /// <param name="curProducts">Множество текущих продуктов</param>
        /// <returns>Возвращаем новое множество</returns>
        public static List<Product> GetDifferenceProductsPrice(IEnumerable<Product> newProducts, IEnumerable<Product> curProducts)
        {
            return newProducts.Except(curProducts).ToList();
        }
    }
}

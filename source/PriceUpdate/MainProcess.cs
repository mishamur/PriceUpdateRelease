using Models;
using OfficeWrapper;
using DbApi.api;
using Interfaces;
using ConfigSettings;
using OfficeWrapper.Exceptions.ExcelExceptions;

namespace PriceUpdate
{
    public class MainProcess
    {
        /// <summary>
        /// Запустить основной процесс
        /// </summary>
        /// <param name="pathToExcelFile">Путь к excel файлу с новыми продуктами</param>
        /// <param name="logger">Логер</param>
        public void RunProcessing(ISettings settings, Action<string> logger = null)
        {
            //получаем путь до excel файла
            string pathToExcelFile = settings.GetValue("pathToExcelFile")?.ToString();
            if (string.IsNullOrWhiteSpace(pathToExcelFile))
            {
                logger?.Invoke("не задан путь входного excel-файла");
                return;
            }
                
            ProductsDb productsDb = new ProductsDb(logger);
            List<Product> excelProducts = new List<Product>();
            try
            {
                using (ExcelWrapper openRead = ExcelWrapper.OpenReadExcel(pathToExcelFile))
                {
                    excelProducts = openRead.ReadProductsFromABColumns().ToList();
                }
            }
            catch (ExcelExceptionBase ex)
            {
                logger?.Invoke(ex.Message);
                return;
            }
            
            //обрабатываем
            List<Product> dbProducts = productsDb.GetProducts().ToList();
            List<Product> differenceProducts = CompareProducts.GetDifferenceProductsPrice(excelProducts, dbProducts);
            
            string outputFolderPath = settings.GetValue("outputDirectory")?.ToString();
            if (string.IsNullOrWhiteSpace(outputFolderPath))
            {
                logger?.Invoke("не задан путь до выходной директории");
                return;
            }
            
            Directory.CreateDirectory(outputFolderPath);
            DirectoryInfo outputDirectory = new DirectoryInfo(outputFolderPath);
            //формируем имя файла
            string fileName = "список обновлённых продуктов" + DateTime.Now.ToString("g").GetHashCode();
            //задаётся путь к файлу
            string pathToOutputFile = Path.Combine(outputFolderPath, fileName);

            try
            {
                using (ExcelWrapper createFile = ExcelWrapper.CreateFileExcel(pathToOutputFile))
                {
                    createFile.SaveFileWithProducts(differenceProducts);
                }
            }
            catch (ExcelExceptionBase ex)
            {
                logger?.Invoke(ex.Message);
                return;
            }

            productsDb.LoadToProducts(excelProducts, true);
            logger?.Invoke($"процесс успешно отработал, выходной файл: {pathToOutputFile}");
        }
    }
}
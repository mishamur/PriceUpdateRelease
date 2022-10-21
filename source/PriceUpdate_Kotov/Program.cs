using OfficeWrapper;
using Models;
using PriceUpdate;
using Logger;
using Interfaces;
using DbApi;
using System.Globalization;
using ConfigSettings;
//этапы выполнения программы
/*приходит файл на выполнение
 * считываем с него данные +
 * обрабатываем данные из файла и из бд+
 * генерируем файл только с обновлёнными ценниками и новыми продуктами+
 * записываем новые данные в бд+
 */

public static class Program
{
    public static void Main(string[] args)
    {
        Action<string> logger;
        ILogger consoleLogger = new ConsoleLogger();
        logger = consoleLogger.Log;

        SettingsLoader settingsLoader = new SettingsLoader(new Settings());
        ISettings settings = settingsLoader.LoadSettings();

        MainProcess mainProcess = new();
        mainProcess.RunProcessing(settings, logger);
    }
}
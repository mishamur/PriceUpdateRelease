using Interfaces;

namespace ConfigSettings
{
    public class SettingsLoader
    {
        private ISettings settings;
        ILogger logger;
        const string TEMPLATE_SETTING = "-pathToExcelFile {path}";
        public readonly string applicationFolderName = "PriceConfig";

        public SettingsLoader(ISettings settings, ILogger logger = null)
        {
            this.settings = settings;
            SetDefaultValues();
        }
        /// <summary>
        /// загрузить настройки
        /// </summary>
        /// <returns>объект ISettings</returns>
        public ISettings LoadSettings()
        {
            SearchInSystemDirectory();
            SearchingHomeDirectory();
            GetFromParams();
            return settings;
        }

        /// <summary>
        /// установить дефолтные значения
        /// </summary>
        private void SetDefaultValues()
        {
            settings.SetDefaultOutputDirectory(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), applicationFolderName
                ));
        }
        
        /// <summary>
        /// поиск в системной директории
        /// </summary>
        private void SearchInSystemDirectory()
        {
            var pathToFolder = Path.Combine(Environment.GetFolderPath(
               Environment.SpecialFolder.CommonApplicationData), applicationFolderName);
            SearchInDirectory(pathToFolder);
        }

        /// <summary>
        /// Поиск в директории конкретного юзера
        /// </summary>
        private void SearchingHomeDirectory()
        {
            var pathToFolder = Path.Combine(Environment.GetFolderPath(
               Environment.SpecialFolder.ApplicationData), applicationFolderName);
            SearchInDirectory(pathToFolder);
        }

        /// <summary>
        /// поиск в директории
        /// </summary>
        /// <param name="pathToFolder">путь до папки</param>
        private void SearchInDirectory(string pathToFolder)
        {
            var pathToFile = Path.Combine(pathToFolder, "priceConfig.txt");
            Directory.CreateDirectory(pathToFolder);

            //наполнить данными
            if (!File.Exists(pathToFile))
            {
                //File.Create(pathToFile);
                CreateAndFillConfigFile(pathToFile);
            }
            else
            {
                //считать, распарсить
                string[] settingValue = this.ReadSettingsFile(pathToFile);
                //распарсить
                this.settings.ParseToSettigns(settingValue);
            }
        }

        /// <summary>
        /// получить значения из параметров запуска
        /// </summary>
        private void GetFromParams()
        {
            this.settings.ParseToSettigns(Environment.GetCommandLineArgs());
        }

        /// <summary>
        /// Считать файл настроек
        /// </summary>
        /// <param name="pathToFile">путь до файла</param>
        /// <returns>строки настроек</returns>
        private string[] ReadSettingsFile(string pathToFile)
        {
            List<string> result = new List<string>();

            if (File.Exists(pathToFile))
            {
                try
                {
                    using(var streamReader = File.OpenText(pathToFile))
                    {
                        string valueLine;
                        while((valueLine = streamReader.ReadLine()) != null)
                        {
                            result.Add(valueLine);
                        }
                        
                    }
                }
                catch(Exception ex)
                {
                    logger?.Log(ex.Message);
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Создать файл и заполнить его шаблоном настроек
        /// </summary>
        /// <param name="pathToFile">путь к файлу)</param>
        private void CreateAndFillConfigFile(string pathToFile)
        {
            try
            {
                if (!File.Exists(pathToFile))
                {
                    using (var text = File.CreateText(pathToFile))
                    {
                        text.WriteLine(TEMPLATE_SETTING);
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Log(ex.Message);
            }
        }
    }
}

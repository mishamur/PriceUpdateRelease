using Interfaces;

namespace ConfigSettings
{
    public class Settings : ISettings
    {
        /// <summary>
        /// настройки
        /// </summary>
        Dictionary<string, object> settings;
        public Settings()
        {
            settings = new Dictionary<string, object>();
        }

        /// <summary>
        /// Распарсить массив строк на настройки и записать их в переменную settings
        /// </summary>
        /// <param name="settingsValue"></param>
        public void ParseToSettigns(string[] settingsValue)
        {
            string[] settingArgs = string.Join(" ", settingsValue).Split('-');

            foreach(string setting in settingArgs)
            {
                string[] values = setting.Split(" ");
                if (values.Length == 2)
                {
                    string settingName = values[0];
                    string settingValue = values[1];

                    if (!string.IsNullOrEmpty(settingValue))
                    {
                        //проверить есть ли ключ,
                        if (!this.settings.ContainsKey(settingName))
                        {
                            this.settings.TryAdd(settingName, settingValue);
                        }
                        else
                        {
                            this.settings[settingName] = settingValue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Установить стандартную директорию для вывода файлов
        /// </summary>
        /// <param name="settingValue">значение настройки</param>
        public void SetDefaultOutputDirectory(string settingValue)
        {
            string settingName = "outputDirectory";
            if (!this.settings.ContainsKey(settingName))
            {
                this.settings.TryAdd(settingName, settingValue);
            }
        }

        /// <summary>
        /// получить значение по указанному ключу
        /// </summary>
        /// <param name="key">значение ключ</param>
        /// <returns>значение настройки</returns>
        public object? GetValue(string key)
        {
            return this.settings.GetValueOrDefault(key);
        }
    }
}

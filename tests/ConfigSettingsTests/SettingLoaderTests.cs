using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.ConfigSettingsTests
{
    public class SettingLoaderTests
    {
        [Fact]
        public void AllConfigShouldBeCreated()
        {
            var settingLoader = ReturnDefaultSettingLoader();
            settingLoader.LoadSettings();

            bool isSystemExist = Directory.Exists(PathToSystemDirectory(settingLoader.applicationFolderName));
            bool isHomeExist = Directory.Exists(PathToHomeDirectory(settingLoader.applicationFolderName));

            Assert.True(isSystemExist && isHomeExist);
        }

        private string PathToSystemDirectory(string appFolderName)
        {
            return Path.Combine(Environment.GetFolderPath(
               Environment.SpecialFolder.CommonApplicationData), appFolderName);
        }

        private string PathToHomeDirectory(string appFolderName)
        {
            return Path.Combine(Environment.GetFolderPath(
                 Environment.SpecialFolder.ApplicationData), appFolderName);
        }


        public SettingsLoader ReturnDefaultSettingLoader()
        {
            return new SettingsLoader(new Settings());
        }
    }
}

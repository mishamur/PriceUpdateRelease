using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.ConfigSettingsTests
{
    public class SettingsTests
    {
        
        [Theory]
        [InlineData("-pathToOutputFolder C:\\Users\\Public\\Documents", "pathToOutputFolder", "C:\\Users\\Public\\Documents")]
        [InlineData("-pathToFile C:\\Users\\Public\\Documents\\test.xlsx", "pathToFile", "C:\\Users\\Public\\Documents\\test.xlsx")]
        public void ParseShouldBeIsValid(string parseString, string key, object expectedValue)
        {
            var settings = ReturnDefaultSettings();
            settings.ParseToSettigns(new string[] { parseString });
            object actual = settings.GetValue(key);
            object expected = expectedValue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetNotExistingKeyShouldBeNull()
        {
            var settings = ReturnDefaultSettings();
           
            object actual = settings.GetValue("undefinedKey");
            Assert.Null(actual);
        }


        public Settings ReturnDefaultSettings()
        {
            return new Settings();
        }
            
    }
}

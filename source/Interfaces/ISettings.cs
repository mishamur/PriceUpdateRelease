using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ISettings
    {
        public void ParseToSettigns(string[] settingsValue);
        public void SetDefaultOutputDirectory(string settingValue);
        public object? GetValue(string key);
    }
}

using Cinema.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cinema.Configuration
{
    public class ApplicationConfig
    {
        public string SettingsPath { get; private set; }

        private IEnumerable<IApplicationSetting> _settings;
        public IEnumerable<IApplicationSetting> Settings 
        { 
            get
            {
                if(File.Exists(SettingsPath))
                {
                    string fileText = File.ReadAllText(SettingsPath);
                    _settings = JsonConvert.DeserializeObject<List<ApplicationSetting>>(fileText);
                }

                return _settings == null ? new List<IApplicationSetting>() : _settings;
            }
            set
            {
                _settings = value;
                File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(_settings));
            }
        }

        private static ApplicationConfig _instance;
        public static ApplicationConfig Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ApplicationConfig();
                }

                return _instance;
            }
        }

        private ApplicationConfig()
        {
            _settings = new List<IApplicationSetting>();
            SettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "settings.json");

            if(!File.Exists(SettingsPath))
            {
                File.Create(SettingsPath).Close();
            }
        }
    }
}
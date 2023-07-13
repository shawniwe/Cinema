using Cinema.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Configuration
{
    public class ApplicationSetting : IApplicationSetting
    { 
        public string Json { get; set; }
        public SettingType SettingType { get; set; }
    }
}

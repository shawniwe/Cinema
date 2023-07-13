using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Cinema.Abstract
{
    public interface IApplicationSetting
    {
        string Json { get; set; }
        SettingType SettingType { get; set; }
    }
}

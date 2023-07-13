using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNS.Abstract
{
    public interface IDefaultFactory
    {
        void AddTransient<T1>(string name);
        object GetObject(string name);
    }
}

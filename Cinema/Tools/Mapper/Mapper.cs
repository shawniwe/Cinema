using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using Cinema.Abstract;

namespace Cinema.Tools.Mapper
{
    public class Mapper
    {
        public List<IMap> Mappings { get; private set; }
        private static Mapper _instance;

        public static Mapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Mapper();
                }
                return _instance;
            }
        }

        private Mapper()
        {
            Mappings = new List<IMap>();
        }

        public Map<T1, T2> CreateMapping<T1, T2>() where T1 : class where T2 : class
        {
            var map = new Map<T1, T2>(typeof(T1), typeof(T2));
            Mappings.Add(map);
            return map;
        }
    }
}

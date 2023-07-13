using CinemaNS.Abstract;
using CinemaNS.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Factories
{
    public class EntityManagersFactory : IDefaultFactory
    {
        private static EntityManagersFactory _instance;
        public static EntityManagersFactory Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new EntityManagersFactory();
                }

                return _instance;
            }
        }

        private ApplicationDbContext _context;

        private Dictionary<string, Type> _transients;
        private EntityManagersFactory()
        {
            _transients = new Dictionary<string, Type>();
            _context = new ApplicationDbContext();
        }

        public void AddTransient<T>(string name)
        {
            if (_transients.Where(x => x.Key == name).Any())
            {
                throw new DuplicateNameException();
            }

            _transients.Add(name, typeof(T));
        }

        public object GetObject(string name)
        {
            var type = _transients.Where(x => x.Key == name).FirstOrDefault();
            var instance = Activator.CreateInstance(type.Value, _context);

            return instance;
        }
    }
}

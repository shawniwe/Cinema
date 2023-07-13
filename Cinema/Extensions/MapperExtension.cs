using Cinema.Tools.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Extensions
{
    public static class MapperExtension
    {
        public static T Map<T>(this object obj)
        {
            if (obj == null) throw new NullReferenceException();
            var instance = Activator.CreateInstance(typeof(T));
            var map = Mapper.Instance.Mappings
                .FirstOrDefault(x => x.SourceType == obj.GetType() && x.DestinationType == typeof(T));

            if (map == null)
            {
                map = Mapper.Instance.Mappings
                .FirstOrDefault(x => x.SourceType == typeof(T) && x.DestinationType == obj.GetType());

                if (map == null)
                {
                    throw new InvalidOperationException();
                }

                foreach (var pm in map.PropertyMappings)
                {
                    var property = instance.GetType().GetProperties().FirstOrDefault(x => x == pm.Key);
                    property.SetValue(instance, pm.Value.GetValue(obj));
                }
            }
            else
            {
                foreach (var pm in map.PropertyMappings)
                {
                    var property = instance.GetType().GetProperties().FirstOrDefault(x => x == pm.Value);
                    property.SetValue(instance, pm.Key.GetValue(obj));
                }
            }

            return (T)instance;
        }
    }
}

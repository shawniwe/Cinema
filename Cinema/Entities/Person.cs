using CinemaNS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNS.Entities
{
    public class Person : IEntity
    {
        public long? Id { get; private set; }
        public string Name { get; set; }
    }
}

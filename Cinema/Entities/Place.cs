using CinemaNS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNS.Entities
{
    public class Place : IEntity
    {
        public long? Id { get; private set; }
        public virtual Row? Row { get; set; }
        public int Number { get; set; }
    }
}

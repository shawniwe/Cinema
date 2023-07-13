using CinemaNS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNS.Entities
{
    public class MovieProducer : IEntity
    {
        public long? Id { get; set; }
        public virtual Person? Producer { get; set; }
        public virtual Movie? Movie { get; set; }
    }
}

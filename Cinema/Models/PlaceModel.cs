using CinemaNS.Abstract;
using CinemaNS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class PlaceModel
    {
        public long? Id { get; private set; }
        public virtual Row Row { get; set; }
        public int Number { get; set; }
    }
}

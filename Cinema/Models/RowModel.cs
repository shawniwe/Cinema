using CinemaNS.Abstract;
using CinemaNS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class RowModel
    {
        public long? Id { get; private set; }
        public virtual Hall Hall { get; set; }
        public int Number { get; set; }
        public virtual ICollection<Place> Places { get; set; }
        public int PlacesCount { get; set; }
    }
}

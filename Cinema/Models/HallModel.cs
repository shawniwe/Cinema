using CinemaNS.Abstract;
using CinemaNS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class HallModel
    {
        public long? Id { get; private set; }
        public virtual CinemaNS.Entities.Cinema Cinema { get; set; }
        public long? Number { get; set; }
        public virtual ICollection<Row> Rows { get; set; }
        public virtual ICollection<CinemaNS.Entities.Session> Sessions { get; set; }
    }
}

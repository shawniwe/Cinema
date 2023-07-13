using CinemaNS.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNS.Entities
{
    public class Session : IEntity
    {
        public long? Id { get; private set; }
        public virtual Movie? Movie { get; set; }
        public virtual Hall? Hall { get; set; }
        public DateTime StartDate { get; set; }

        [Column(TypeName = "Money")]
        public decimal Cost { get; set; }
    }
}

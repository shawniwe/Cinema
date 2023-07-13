using CinemaNS.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNS.Entities
{
    public class Row : IEntity
    {
        [Key]
        public long? Id { get; private set; }
        [ForeignKey("HallId")]
        public virtual Hall? Hall { get; set; }
        public int Number { get; set; }
        public virtual ICollection<Place>? Places { get; set; }
    }
}

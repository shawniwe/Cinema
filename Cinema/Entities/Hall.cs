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
    public class Hall : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long? Id { get; private set; }
        public long? Number { get; set; }
        public virtual Cinema? Cinema { get; set; }
        public virtual ICollection<Row>? Rows { get; set; }
        public virtual ICollection<Session>? Sessions { get; set; }
    }
}

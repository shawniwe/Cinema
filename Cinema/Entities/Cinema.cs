using Cinema.Abstract;
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
    public class Cinema : IEntity
    {
        [Key]
        public long? Id { get; set; }
        public string? Title { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District? District { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Hall>? Halls { get; set; }
    }
}

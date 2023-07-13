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
    public class District : IEntity
    {
        [Key]
        public long? Id { get; private set; }
        public string? Title { get; set; }
    }
}

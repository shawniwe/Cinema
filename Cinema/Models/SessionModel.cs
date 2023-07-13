using Cinema.Abstract;
using CinemaNS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class SessionModel
    {
        public long? Id { get; private set; }
        public virtual Movie? Movie { get; set; }
        public virtual Hall? Hall { get; set; }
        public DateTime StartDate { get; set; }

        [Column(TypeName = "Money")]
        public decimal Cost { get; set; }
    }
}

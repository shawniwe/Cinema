using Cinema.Abstract;
using CinemaNS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class CinemaModel
    {
        public long? Id { get; set; }
        public string? Title { get; set; }
        public virtual District District { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Hall>? Halls { get; set; }
    }
}

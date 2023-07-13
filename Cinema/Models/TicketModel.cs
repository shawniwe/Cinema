using CinemaNS.Abstract;
using CinemaNS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class TicketModel
    {
        [Key]
        public long? Id { get; set; }
        public Session? Session { get; set; }
        public Place? Place { get; set; }
    }
}

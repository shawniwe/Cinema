using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class RevenueModel
    {
        public CinemaNS.Entities.Cinema? Cinema { get; set; }
        public decimal Revenue { get; set; }
    }
}

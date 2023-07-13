using CinemaNS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNS.Entities
{
    public class Movie : IEntity
    {
        public long? Id { get; private set; }
        public string Title { get; set; }
        public virtual Genre? Genre { get; set; }
        public virtual Country? Country { get; set; }
        public int Minutes { get; set; }
        public string? Photo { get; set; }
        public virtual IEnumerable<Session>? Sessions { get; set; }
        public virtual ICollection<MovieActor>? MovieActors { get; set; }
        public virtual ICollection<MovieOperator>? MovieOperators { get; set; }
        public virtual ICollection<MovieProducer>? MovieProducers { get; set; }
    }
}

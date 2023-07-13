using CinemaNS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class MovieModel
    {
        public long? Id { get; private set; }
        public string Title { get; set; }
        public virtual Genre? Genre { get; set; }
        public virtual Country? Country { get; set; }
        public int Minutes { get; set; }
        public string? Photo { get; set; }
        public Uri DisplayPhoto { get; set; }
        public virtual IEnumerable<CinemaNS.Entities.Session>? Sessions { get; set; }
        public virtual ICollection<MovieActor>? MovieActors { get; set; }
        public virtual ICollection<MovieOperator>? MovieOperators { get; set; }
        public virtual ICollection<MovieProducer>? MovieProducers { get; set; }
        public string ActorsStr => string.Join(", ", MovieActors.Where(x => x.Actor != null).Take(3).Select(x => x.Actor.Name));
        public string OperatorsStr => string.Join(", ", MovieOperators.Where(x => x.Operator != null).Take(3).Select(x => x.Operator.Name));
        public string ProducersStr => string.Join(", ", MovieProducers.Where(x => x.Producer != null).Take(3).Select(x => x.Producer.Name));
    }
}

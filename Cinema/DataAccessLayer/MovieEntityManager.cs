using CinemaNS.DataAccessLayer;
using CinemaNS.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DataAccessLayer
{
    public class MovieEntityManager : EntityManager<Movie>
    {
        public MovieEntityManager(ApplicationDbContext context) : base(context) { }

        public override IEnumerable<Movie> ReadAll()
        {
            return _instance
                .Include(x => x.Genre)
                .Include(x => x.Country)
                .Include(x => x.MovieActors)
                .ThenInclude(x => x.Actor)
                .Include(x => x.MovieProducers)
                .ThenInclude(x => x.Producer)
                .Include(x => x.MovieOperators)
                .ThenInclude(x => x.Operator);
        }

        public override Movie Read(long Id)
        {
            return _instance
                .Include(x => x.Genre)
                .Include(x => x.Country)
                .Include(x => x.MovieActors)
                .ThenInclude(x => x.Actor)
                .Include(x => x.MovieProducers)
                .ThenInclude(x => x.Producer)
                .Include(x => x.MovieOperators)
                .ThenInclude(x => x.Operator)
                .FirstOrDefault(x => x.Id == Id);
        }

        public override IEnumerable<Movie> Read(Expression<Func<Movie, bool>> predicate)
        {
            return _instance
                .Where(predicate)
                .Include(x => x.Genre)
                .Include(x => x.Country)
                .Include(x => x.MovieActors)
                .ThenInclude(x => x.Actor)
                .Include(x => x.MovieProducers)
                .ThenInclude(x => x.Producer)
                .Include(x => x.MovieOperators)
                .ThenInclude(x => x.Operator);
        }

        public override void Update(Movie entity)
        {
            if(entity != null)
            {
                //Context.Attach(entity.Hall);
                var entry = _instance
                    .Include(x => x.Genre)
                    .Include(x => x.Country)
                    .Include(x => x.MovieActors)
                    .ThenInclude(x => x.Actor)
                    .Include(x => x.MovieProducers)
                    .ThenInclude(x => x.Producer)
                    .Include(x => x.MovieOperators)
                    .ThenInclude(x => x.Operator)
                    .FirstOrDefault(x => x.Id == entity.Id);
                if (entry != null)
                {
                    foreach (var prop in entity.GetType().GetProperties().Where(x => x.Name != "Id"))
                    {
                        var value = prop.GetValue(entity);
                        var entryProp = entry
                            .GetType()
                            .GetProperties()
                            .FirstOrDefault(x => x.Name == prop.Name);
                        if (entryProp != null)
                        {
                            entryProp.SetValue(entry, value);
                        }
                    }
                }
                Context.SaveChanges();
            }
        }

        public override void Create(Movie entity)
        {
            if (entity != null)
            {
                //entity.Places = null;
                //Context.Entry(entity.Hall).State = EntityState.Added;
                Context.Movies.Add(entity);
                Context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public void DeleteProducer(long? movieId, long? producerId)
        {
            if (movieId == null || producerId == null) throw new ArgumentNullException();
            var entry = _instance.Include(x => x.MovieProducers).FirstOrDefault(x => x.Id == movieId);
            if (entry == null) throw new NullReferenceException();
            if (entry.MovieProducers != null)
            {
                var producer = entry.MovieProducers.Where(x => x.Id == producerId).FirstOrDefault();
                entry.MovieProducers.Remove(producer);
                Context.SaveChanges();
            }
        }

        public void DeleteActor(long? movieId, long? actorId)
        {
            if (movieId == null || actorId == null) throw new ArgumentNullException();
            var entry = _instance.Include(x => x.MovieActors).FirstOrDefault(x => x.Id == movieId);
            if (entry == null) throw new NullReferenceException();
            if (entry.MovieActors != null)
            {
                var actor = entry.MovieActors.Where(x => x.Id == actorId).FirstOrDefault();
                entry.MovieActors.Remove(actor);
                Context.SaveChanges();
            }
        }

        public void DeleteOperator(long? movieId, long? operatorId)
        {
            if (movieId == null || operatorId == null) throw new ArgumentNullException();
            var entry = _instance.Include(x => x.MovieOperators).FirstOrDefault(x => x.Id == movieId);
            if (entry == null) throw new NullReferenceException();
            if (entry.MovieOperators != null)
            {
                var oprtr = entry.MovieOperators.Where(x => x.Id == operatorId).FirstOrDefault();
                entry.MovieOperators.Remove(oprtr);
                Context.SaveChanges();
            }
        }

        internal void AddActor(long? movieId, MovieActor model)
        {
            if (movieId == null || model == null) throw new ArgumentNullException();
            var entry = _instance.FirstOrDefault(x => x.Id == movieId);
            if (entry == null) throw new NullReferenceException();

            if(entry.MovieActors == null)
            {
                entry.MovieActors = new List<MovieActor>();
            }
            entry.MovieActors.Add(model);
            Context.SaveChanges();
        }

        internal void AddProducer(long? movieId, MovieProducer model)
        {
            if (movieId == null || model == null) throw new ArgumentNullException();
            var entry = _instance.FirstOrDefault(x => x.Id == movieId);
            if (entry == null) throw new NullReferenceException();

            if (entry.MovieProducers == null)
            {
                entry.MovieProducers = new List<MovieProducer>();
            }
            entry.MovieProducers.Add(model);
            Context.SaveChanges();
        }

        internal void AddOperator(long? movieId, MovieOperator model)
        {
            if (movieId == null || model == null) throw new ArgumentNullException();
            var entry = _instance.FirstOrDefault(x => x.Id == movieId);
            if (entry == null) throw new NullReferenceException();

            if (entry.MovieOperators == null)
            {
                entry.MovieOperators = new List<MovieOperator>();
            }
            entry.MovieOperators.Add(model);
            Context.SaveChanges();
        }
    }
}

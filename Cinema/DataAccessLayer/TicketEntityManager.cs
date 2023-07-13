using Cinema.Entities;
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
    public class TicketEntityManager : EntityManager<Ticket>
    {
        public TicketEntityManager(ApplicationDbContext context) : base(context) { }

        public override IEnumerable<Ticket> ReadAll()
        {
            var result = _instance
                .Include(x => x.Session)
                .ThenInclude(x => x.Movie)
                .Include(x => x.Session)
                .ThenInclude(x => x.Hall)
                .ThenInclude(x => x.Cinema)
                .Include(x => x.Place)
                .ThenInclude(x => x.Row)
                .ToList();
            return result;
        }

        public override Ticket Read(long Id)
        {
            return _instance
                .Include(x => x.Session)
                .ThenInclude(x => x.Movie)
                .Include(x => x.Session)
                .ThenInclude(x => x.Hall)
                .ThenInclude(x => x.Cinema)
                .Include(x => x.Place)
                .ThenInclude(x => x.Row).FirstOrDefault(x => x.Id == Id);
        }

        public override IEnumerable<Ticket> Read(Expression<Func<Ticket, bool>> predicate)
        {
            return _instance.Where(predicate)
                .Include(x => x.Session)
                .ThenInclude(x => x.Movie)
                .Include(x => x.Session)
                .ThenInclude(x => x.Hall)
                .ThenInclude(x => x.Cinema)
                .Include(x => x.Place)
                .ThenInclude(x => x.Row);
        }

        public override void Update(Ticket entity)
        {
            if(entity != null)
            {
                var entry = _instance
                .Include(x => x.Session)
                .ThenInclude(x => x.Movie)
                .Include(x => x.Session)
                .ThenInclude(x => x.Hall)
                .ThenInclude(x => x.Cinema)
                .Include(x => x.Place)
                .ThenInclude(x => x.Row).FirstOrDefault(x => x.Id == entity.Id);
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

        public override void Create(Ticket entity)
        {
            if (entity != null)
            {
                //Context.Attach(entity.District);
                _instance.Add(entity);
                Context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}

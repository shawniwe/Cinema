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
    public class PlaceEntityManager : EntityManager<Place>
    {
        public PlaceEntityManager(ApplicationDbContext context) : base(context) { }

        public override IEnumerable<Place> ReadAll()
        {
            return _instance.Include(x => x.Row);
        }

        public override Place Read(long Id)
        {
            return _instance.Include(x => x.Row).FirstOrDefault(x => x.Id == Id);
        }

        public override IEnumerable<Place> Read(Expression<Func<Place, bool>> predicate)
        {
            return _instance.Include(x => x.Row).Where(predicate);
        }

        public override void Update(Place entity)
        {
            if(entity != null)
            {
                //Context.Attach(entity.Row);
                var entry = _instance.Include(x => x.Row).Where(x => x.Id == entity.Id);
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

        public override void Create(Place entity)
        {
            if (entity != null)
            {
                //Context.Entry(entity).State = EntityState.Added;
                //Context.Attach(entity.Row.Hall);
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

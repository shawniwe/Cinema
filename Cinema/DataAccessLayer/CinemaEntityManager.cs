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
    public class CinemaEntityManager : EntityManager<CinemaNS.Entities.Cinema>
    {
        public CinemaEntityManager(ApplicationDbContext context) : base(context) { }

        public override IEnumerable<CinemaNS.Entities.Cinema> ReadAll()
        {
            var result = _instance.Include(x => x.District).Include(x => x.Halls).ToList();
            return result;
        }

        public override CinemaNS.Entities.Cinema Read(long Id)
        {
            return _instance.Include(x => x.District).Include(x => x.Halls).FirstOrDefault(x => x.Id == Id);
        }

        public override IEnumerable<CinemaNS.Entities.Cinema> Read(Expression<Func<CinemaNS.Entities.Cinema, bool>> predicate)
        {
            return _instance.Where(predicate).Include(x => x.District).Include(x => x.Halls);
        }

        public override void Update(CinemaNS.Entities.Cinema entity)
        {
            if(entity != null)
            {
                var entry = _instance.Include(x => x.District).FirstOrDefault(x => x.Id == entity.Id);
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

        public override void Create(CinemaNS.Entities.Cinema entity)
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

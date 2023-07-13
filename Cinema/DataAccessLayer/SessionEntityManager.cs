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
    public class SessionEntityManager : EntityManager<CinemaNS.Entities.Session>
    {
        public SessionEntityManager(ApplicationDbContext context) : base(context) { }

        public override IEnumerable<CinemaNS.Entities.Session> ReadAll()
        {
            var result = _instance
                .Include(x => x.Movie)
                .Include(x => x.Hall)
                .ThenInclude(x => x.Rows)
                .ThenInclude(x => x.Places)
                .Include(x => x.Hall)
                .ThenInclude(x => x.Cinema).ToList();
            return result;
        }

        public override CinemaNS.Entities.Session Read(long Id)
        {
            return _instance.Include(x => x.Movie)
                .Include(x => x.Hall)
                .ThenInclude(x => x.Rows)
                .ThenInclude(x => x.Places)
                .Include(x => x.Hall)
                .ThenInclude(x => x.Cinema).FirstOrDefault(x => x.Id == Id);
        }

        public override IEnumerable<CinemaNS.Entities.Session> Read(Expression<Func<CinemaNS.Entities.Session, bool>> predicate)
        {
            return _instance.Where(predicate).Include(x => x.Movie)
                .Include(x => x.Hall)
                .ThenInclude(x => x.Rows)
                .ThenInclude(x => x.Places)
                .Include(x => x.Hall)
                .ThenInclude(x => x.Cinema);
        }

        public override void Update(CinemaNS.Entities.Session entity)
        {
            if(entity != null)
            {
                var entry = _instance.Include(x => x.Movie)
                .Include(x => x.Hall)
                .ThenInclude(x => x.Rows)
                .ThenInclude(x => x.Places)
                .Include(x => x.Hall)
                .ThenInclude(x => x.Cinema).FirstOrDefault(x => x.Id == entity.Id);
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

        public override void Create(CinemaNS.Entities.Session entity)
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

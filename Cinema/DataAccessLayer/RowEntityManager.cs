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
    public class RowEntityManager : EntityManager<Row>
    {
        public RowEntityManager(ApplicationDbContext context) : base(context) { }

        public override IEnumerable<Row> ReadAll()
        {
            return _instance.Include(x => x.Hall).Include(x => x.Places);
        }

        public override Row Read(long Id)
        {
            return _instance.Include(x => x.Hall).Include(x => x.Places).FirstOrDefault(x => x.Id == Id);
        }

        public override IEnumerable<Row> Read(Expression<Func<Row, bool>> predicate)
        {
            return _instance.Where(predicate).Include(x => x.Hall).Include(x => x.Places);
        }

        public override void Update(Row entity)
        {
            if(entity != null)
            {
                //Context.Attach(entity.Hall);
                var entry = _instance.Include(x => x.Hall).Include(x => x.Places).FirstOrDefault(x => x.Id == entity.Id);
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

        public override void Create(Row entity)
        {
            if (entity != null)
            {
                //entity.Places = null;
                //Context.Entry(entity.Hall).State = EntityState.Added;
                Context.Rows.Add(entity);
                Context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException();
            }
        }
       
        //internal void AddPlace(long? id, Place newPlace)
        //{
        //    if (id == null || newPlace == null) throw new ArgumentNullException();

        //    var entry = _instance.Include(x => x.Places).Include(x => x.Hall).ThenInclude(x => x.Rows).FirstOrDefault(x => x.Id == id);
        //    if(entry != null)
        //    {
        //        if (entry.Places == null)
        //        {
        //            entry.Places = new List<Place>();
        //        }
        //        entry.Places.Add(newPlace);
        //        Context.SaveChanges();
        //    }
        //}

        internal int PlacesCount(long? id)
        {
            if(id != null)
            {
                var entry = _instance.FirstOrDefault(x => x.Id == id);
                if (entry.Places != null)
                {
                    return entry.Places.Count();
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}

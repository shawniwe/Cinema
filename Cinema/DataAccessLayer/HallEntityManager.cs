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
    public class HallEntityManager : EntityManager<Hall>
    {
        public HallEntityManager(ApplicationDbContext context) : base(context) { }

        public IEnumerable<Hall> ReadAllWithoutRelations()
        {
            return _instance.ToList();
        }

        public override IEnumerable<Hall> ReadAll()
        {
            var list = _instance.Include(x => x.Cinema).Include(x => x.Sessions).Include(x => x.Rows).ToList();
            return list;
        }


        public override Hall Read(long Id)
        {
            return _instance.Include(x => x.Cinema).Include(x => x.Sessions).Include(x => x.Rows).FirstOrDefault(x => x.Id == Id);
        }

        public override IEnumerable<Hall> Read(Expression<Func<Hall, bool>> predicate)
        {
            return _instance.Where(predicate).Include(x => x.Cinema).Include(x => x.Sessions).Include(x => x.Rows);
        }

        public override void Update(Hall entity)
        {
            if(entity != null)
            {
                //Context.Attach(entity.Cinema);
                var entry = _instance.Include(x => x.Cinema).Include(x => x.Sessions).Include(x => x.Rows).FirstOrDefault(x => x.Id == entity.Id);
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

        public override void Create(Hall entity)
        {
            if (entity != null)
            {
                //if(entity.Cinema != null)
                //{
                //   Context.Attach(entity.Cinema);
                //}
                //Context.Entry(entity).State = EntityState.Added;
                _instance.Add(entity);
                Context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        internal void AddRow(long? id, Row newRow)
        {
            if (id == null) throw new ArgumentNullException();

            var entry = _instance.Include(x => x.Rows).FirstOrDefault(x => x.Id == id);
            if (entry != null)
            {
                if (newRow == null) throw new ArgumentNullException();
                if (entry.Rows == null)
                {
                    entry.Rows = new List<Row>();
                }
                entry.Rows.Add(newRow);
                Context.SaveChanges();
            }
        }

        internal int RowsCount(long? id)
        {
            if(id != null)
            {
                var entry = _instance.FirstOrDefault(x => x.Id == id);
                if(entry.Rows != null)
                {
                    return entry.Rows.Count();
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }

        internal void AddPlace(long? rowId, Place newPlace)
        {
            if (rowId == null || newPlace == null) throw new ArgumentNullException();
            var entry = _instance.Include(x => x.Rows).FirstOrDefault(x => x.Rows.Select(x => x.Id).Contains(rowId));
            var row = entry.Rows.FirstOrDefault(x => x.Id == rowId);
            if(row != null)
            {
                if (row.Places == null)
                {
                    entry.Rows.FirstOrDefault(x => x.Id == rowId).Places = new List<Place>();
                }
                entry.Rows.FirstOrDefault(x => x.Id == rowId).Places.Add(newPlace);
                Context.SaveChanges();
            }
        }
    }
}

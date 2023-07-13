using CinemaNS.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNS.DataAccessLayer
{
    public class EntityManager<T> : DefaultEntityManager, IRepository<T> where T : class, IEntity
    {
        protected DbSet<T> _instance;
        public EntityManager(ApplicationDbContext context) : base(context)
        {
            var property = Context.GetType().GetProperties().Where(x => x.PropertyType == typeof(DbSet<T>)).FirstOrDefault();
            if (property == null) { throw new TypeLoadException(); }

            _instance = (DbSet<T>)property.GetValue(Context);
        }

        public virtual void Create(T entity)
        {
            if (entity != null)
            {
                _instance.Add(entity);
                Context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public virtual void Delete(long? Id)
        {
            if (Id == null) throw new ArgumentNullException();
            //Context.Entry(entity).State = EntityState.Deleted;
            var entry = _instance.Where(x => x.Id == Id).FirstOrDefault();
            if(entry != null)
            {
                _instance.Remove(entry);
                Context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public virtual T Read(long Id)
        {
            return _instance.Where(x => x.Id == Id).FirstOrDefault();
        }

        public virtual void Update(T entity)
        {
            var entry = _instance.FirstOrDefault(x => x.Id == entity.Id);
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

        public virtual IEnumerable<T> Read(Expression<Func<T, bool>> predicate)
        {
            return _instance.Where(predicate);
        }

        public virtual IEnumerable<T> ReadAll()
        {
            return _instance.ToList();
        }
    }
}

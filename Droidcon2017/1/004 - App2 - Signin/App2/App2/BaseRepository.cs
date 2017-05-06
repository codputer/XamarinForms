using System.Collections.Generic;
using System.Linq;

namespace App2
{
    public class BaseRepository<TEntity> where TEntity : Entity
    {
        public DbContext Db;

        public BaseRepository(DbContext db)
        {
            //db.ChangeTracker.QueryTrackingBehavior= QueryTrackingBehavior.NoTracking;
            this.Db = db;
        }
        
        public IQueryable<TEntity> Get()
        {
            var query = this.Db.Set<TEntity>().AsQueryable().AsNoTracking();
            return query;
        }
        public TEntity GetById(string id)
        {
            TEntity entity = this.Get().FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                this.Db.Entry(entity).State=EntityState.Detached;
            }
            return entity;
        }

        public virtual bool Exists(string id)
        {
            return this.Get().Any(x => x.Id == id);
        }

        public virtual TEntity Add(TEntity entity)
        {
            var dbSet = this.Db.Set<TEntity>();
            EntityEntry<TEntity> entry = dbSet.Add(entity);
            return entry.Entity;            
        }

        public virtual IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            this.Db.Set<TEntity>().AddRange(entities);
            return entities;
        }

        public virtual bool Delete(string id)
        {
            TEntity entity = this.GetById(id);
            if (entity != null)
            {
                this.Db.Set<TEntity>().Remove(entity);
            }
            return true;
        }

        public virtual bool Delete(TEntity entity)
        {
            var remove = this.Db.Set<TEntity>().Remove(entity);
            return true;
        }

        public virtual bool Edit(TEntity entity)
        {
            this.Db.Entry(entity).State = EntityState.Detached;
            var entityEntry = this.Db.Set<TEntity>().Update(entity);
            //Db.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public virtual bool Save()
        {
            var changes = this.Db.SaveChanges();
            return changes > 0;
        }
    }
}
using LoggingMvc.Business.Interfaces;
using LoggingMvc.Business.Models;
using LoggingMvc.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LoggingMvc.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly LogDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(LogDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Delete(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}

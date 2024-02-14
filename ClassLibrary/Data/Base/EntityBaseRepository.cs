using ClassLibrary.Context;
using ClassLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClassLibrary.Data.Base
{
    public class EntityBaseRepository<T>:IEntityBaseRepository<T> where T: class,BaseEntity, new()

    {
        private readonly CollegeDbContext _collegeDbContext;

        public EntityBaseRepository(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }

       public async Task<IEnumerable<T>> getAllAsync()
        {
            var result = await _collegeDbContext.Set<T>().ToListAsync();
            return result; 
        }

        public async Task<T> getByIdAsync(Guid id)
        {
            var result = await _collegeDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task addAsync(T entity)
        {
            await _collegeDbContext.Set<T>().AddAsync(entity);
            await _collegeDbContext.SaveChangesAsync();
        }

        public async Task updateAsync(T entity)
        {
            EntityEntry entityEntry = _collegeDbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _collegeDbContext.SaveChangesAsync();
        }

        public async Task deleteAsync(Guid id)
        {
            var entity = await _collegeDbContext.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _collegeDbContext.Entry<T>(entity!);
            entityEntry.State = EntityState.Deleted;
            await _collegeDbContext.SaveChangesAsync();
        }

    }

}


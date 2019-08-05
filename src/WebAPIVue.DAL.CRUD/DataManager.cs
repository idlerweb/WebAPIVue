using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAPIVue.DAL.CRUD
{
    public class DataManager<T, K> : IDataManager<T> where T : BaseEntity
                                                     where K : DbContext
                                                     
    {
        private readonly K _context;
        private readonly IQueryable<T> _collection;

        public DataManager(K context)
        {
            _context = context;
            
            PropertyInfo propertyInfo = _context.GetType().GetProperty($"{typeof(T).Name}s");
                
            _collection = propertyInfo?.GetValue(_context, null) as IQueryable<T>;
        }

        public async Task<List<T>> GetAllAsync() => await _collection.ToListAsync();
        public async Task<T> GetAsync(long id) => await _collection.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<T> CreateAsync(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entityDest = await _collection.FirstOrDefaultAsync(e => e.Id == entity.Id);
            var entityDestProperties = entityDest.GetType().GetProperties().Where(p => !string.Equals(p.Name, "Id"));
            foreach (PropertyInfo property in entityDestProperties)
            {
                var value = property.GetValue(entity, null);
                if (value == null)
                    continue;

                property.SetValue(entityDest, value, null);
            }
            await _context.SaveChangesAsync();
            return entityDest;
        }

        public async Task DeleteAsync(long id)
        {
            _context.Remove(await _collection.FirstAsync(e => e.Id == id));
            await _context.SaveChangesAsync();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPIVue.DAL.CRUD
{
    public interface IDataManager<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(long id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(long id);
    }
}

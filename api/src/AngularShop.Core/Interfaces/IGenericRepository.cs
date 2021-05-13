using System.Collections.Generic;
using System.Threading.Tasks;
using AngularShop.Core.Entities;
using AngularShop.Core.Specifications;

namespace AngularShop.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
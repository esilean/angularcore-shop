using System;
using System.Threading.Tasks;
using AngularShop.Core.Entities;

namespace AngularShop.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
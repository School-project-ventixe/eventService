using eventService.Data.Entities;
using System.Linq.Expressions;

namespace eventService.Repositories
{
    public interface IEventRepository
    {
        Task<EventEntity> AddAsync(EventEntity entity);
        Task<bool> AlreadyExistsAsync(Expression<Func<EventEntity, bool>> expression);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task<bool> DeleteAsync(Expression<Func<EventEntity, bool>> expression);
        Task<IEnumerable<EventEntity>> GetAllAsync();
        Task<EventEntity> GetAsync(Expression<Func<EventEntity, bool>> expression);
        Task RollbackTransactionAsync();
        Task<bool> SaveAsync();
        Task<bool> UpdateAsync(Expression<Func<EventEntity, bool>> expression, EventEntity entityToUpdate);
    }
}
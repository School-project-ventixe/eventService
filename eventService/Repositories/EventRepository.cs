using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using eventService.Data.Entities;
using eventService.Data.Context;

namespace eventService.Repositories;

public class EventRepository : IEventRepository
{
    private readonly DataContexts _context;
    private readonly DbSet<EventEntity> _dbSet;
    private IDbContextTransaction? _transaction;

    public EventRepository(DataContexts context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<EventEntity>();
    }

    #region Transaction Management

    public virtual async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }

    public virtual async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }
    }

    public virtual async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }
    }

    #endregion

    #region CRUD

    public virtual async Task<EventEntity> AddAsync(EventEntity entity)
    {
        if (entity == null)
            throw new Exception("Entity cannot be null");

        try
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating event entity :: {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<bool> SaveAsync()
    {
        try
        {
            var result = await _context.SaveChangesAsync();

            if (result == 0)
                throw new Exception("Failed saving to database");
            else
                return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving entity :: {ex.Message}");
            return false;
        }
    }

    public virtual async Task<IEnumerable<EventEntity>> GetAllAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving entities :: {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<EventEntity> GetAsync(Expression<Func<EventEntity, bool>> expression)
    {
        if (expression == null)
            throw new Exception("Expression cannot be null");

        try
        {
            return await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving event entity :: {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<bool> UpdateAsync(Expression<Func<EventEntity, bool>> expression, EventEntity entityToUpdate)
    {
        if (entityToUpdate == null)
            throw new Exception("Entity to update cannot be null");

        try
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? throw new Exception("Cannot find existing entity");
            _context.Entry(existingEntity).CurrentValues.SetValues(entityToUpdate);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving event entity :: {ex.Message}");
            return false!;
        }
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<EventEntity, bool>> expression)
    {
        if (expression == null)
            throw new Exception("Expression cannot be null");

        try
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? throw new Exception("Cannot find existing entity");
            _dbSet.Remove(existingEntity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting event entity :: {ex.Message}");
            return false;
        }
    }

    public virtual async Task<bool> AlreadyExistsAsync(Expression<Func<EventEntity, bool>> expression)
    {
        if (expression == null)
            throw new Exception("Expression cannot be null");

        try
        {
            return await _dbSet.AnyAsync(expression);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error checking if event exists :: {ex.Message}");
            return false;
        }
    }

    #endregion
}

using Employee.Business.Interfaces;
using Employee.Common.Common;
using Employee.Data.Context;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Data.RepoUnitOfWork;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
  private readonly ApplicationDbContext _dbContext;

  public GenericRepository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }


  public IQueryable<T> Entities => _dbContext.Set<T>();

  /// <summary>
  /// Gets all asynchronous.
  /// </summary>
  /// <returns></returns>
  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await _dbContext
    .Set<T>()
    .ToListAsync();
  }

  /// <summary>
  /// Gets the by identifier asynchronous.
  /// </summary>
  /// <param name="id">The identifier.</param>
  /// <returns></returns>
  public async Task<T> GetByIdAsync(Guid id)
  {
    return await _dbContext.Set<T>().FindAsync(id)
        ?? throw new FieldAccessException($"Field with id {id} cannot be found");
  }

  /// <summary>
  /// Adds the asynchronous.
  /// </summary>
  /// <param name="entity">The entity.</param>
  /// <returns></returns>
  public async Task<T> AddAsync(T entity)
  {
    await _dbContext.Set<T>().AddAsync(entity);
    return entity;
  }

  /// <summary>
  /// Deletes the asynchronous.
  /// </summary>
  /// <param name="entity">The entity.</param>
  /// <returns></returns>
  public Task DeleteAsync(T entity)
  {
    _dbContext.Set<T>().Remove(entity);
    return Task.CompletedTask;
  }

  /// <summary>
  /// Updates the asynchronous.
  /// </summary>
  /// <param name="entity">The entity.</param>
  public async Task UpdateAsync(T entity)
  {
    _dbContext.Entry(entity).State = EntityState.Modified;
    //_dbContext.Entry(entity).CurrentValues.SetValues(entity);
    await Task.CompletedTask;
  }

  public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
  {
    return await _dbContext
      .Set<T>()
      .Where(predicate)
      .ToListAsync();
  }
  public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object?>> includeProperties)
  {
    IQueryable<T> query = _dbContext.Set<T>().Where(predicate);

    query = includeProperties(query);

    return await query.ToListAsync();
  }


}

using Employee.Common.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Interfaces;

public interface IGenericRepository<T> where T : class, IEntity
{
  IQueryable<T> Entities { get; }

  Task<IEnumerable<T>> GetAllAsync();

  Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);

  Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object?>> includeProperties);

  Task<T> GetByIdAsync(Guid id);

  Task<T> AddAsync(T entity);

  Task UpdateAsync(T entity);

  Task DeleteAsync(T entity);

}

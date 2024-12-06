using Employee.Business.Interfaces;
using Employee.Common.Common;
using Employee.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Data.RepoUnitOfWork
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly ApplicationDbContext _dbContext;
    private Hashtable _repositories;
    private bool disposed;
    private readonly ILogger<UnitOfWork> _logger;


    public UnitOfWork(ApplicationDbContext dbContext,ILogger<UnitOfWork> logger)
    {
      _dbContext = dbContext
          ?? throw new ArgumentNullException(nameof(dbContext));
      _logger = logger;
    }

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {

      _repositories ??= new Hashtable();

      var type = typeof(T).Name;
      if (!_repositories.ContainsKey(type))
      {

        var repositoryType = typeof(GenericRepository<>);
        try
        {
          var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);
          _repositories.Add(type, repositoryInstance);
        }
        catch (Exception ex)
        {
          _logger.LogError($"UnitOfWork, Error:{ex.Message} stackTrace:{ex.StackTrace}");
          throw new Exception("We have an Issue creating the Repository");

        }

      }

      return (IGenericRepository<T>)_repositories[type]
          ?? throw new Exception($"Repository {type} cannot be found.");
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposed)
      {
        if (disposing)
        {
          _dbContext.Dispose();
        }
      }
      disposed = true;
    }

    public Task Rollback()
    {
      _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
      return Task.CompletedTask;
    }

    public async Task<int> Save(CancellationToken cancellationToken)
    {
      try
      {
        return await _dbContext.SaveChangesAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        _logger.LogError($"UnitOfWork, Error:{ex.Message} stackTrace:{ex.StackTrace}");
        throw new Exception("We have an Issue Saving the changes");

      }


    }

    public async Task<int> Save<T>(CancellationToken cancellationToken)
    {
      try
      {
        var modelState = _dbContext.ChangeTracker.Entries().FirstOrDefault();
        var isUpdated = modelState.State == EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result;

      }
      catch (Exception ex)
      {
        _logger.LogError($"UnitOfWork, Error:{ex.Message} stackTrace:{ex.StackTrace}");
        throw new Exception("We have an Issue Saving the changes");

      }

    }
  }
}

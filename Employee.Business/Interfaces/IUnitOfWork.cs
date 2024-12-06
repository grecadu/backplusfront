using Employee.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Interfaces
{
  public interface IUnitOfWork
  {
    IGenericRepository<T> Repository<T>() where T : BaseEntity;
    Task<int> Save<T>(CancellationToken cancellationToken);
    Task<int> Save(CancellationToken cancellationToken);
    Task Rollback();
  }
}

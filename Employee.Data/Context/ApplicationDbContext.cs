using Employee.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data.Context;

public class ApplicationDbContext : DbContext
{

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  public DbSet<EmployeeEntity> Employees => Set<EmployeeEntity>();
  public DbSet<DepartmentEntity> Deparements => Set<DepartmentEntity>();


}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Models;

public class EmployeesModel: BaseModel
{
  public required string FirstName { get; set; }
  public required string LastName { get; set; }

  public DateTime? HireDate { get; set; }

  public required string Phone { get; set; }

  public required string Address { get; set; }

  public Guid DepartmentId { get; set; }
    public DepartmentModel? Department { get; set; }
}

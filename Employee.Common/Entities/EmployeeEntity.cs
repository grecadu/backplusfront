using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.Common.Common;

namespace Employee.Common.Entities;

[Table("Employee")]
public class EmployeeEntity : BaseEntity
{


  [StringLength(50)]
  [Column(TypeName = "varchar")]
  public required string FirstName { get; set; }

  [StringLength(50)]
  [Column(TypeName = "varchar")]
  public required string LastName { get; set; }


  [Column(TypeName = "datetime")]
  public DateTime? HireDate { get; set; }

  [StringLength(50)]
  [Column(TypeName = "varchar")]
  public required string Phone { get; set; }


  [StringLength(50)]
  [Column(TypeName = "varchar")]
  public required string Address { get; set; }

  [ForeignKey("Id")]
  public Guid DepartmentId { get; set; }
  public virtual DepartmentEntity Department { get; set; }
}

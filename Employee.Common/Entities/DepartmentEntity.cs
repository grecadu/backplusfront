using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.Common.Common;

namespace Employee.Common.Entities;

[Table("Department")]
public class DepartmentEntity : BaseEntity
{

  [StringLength(50)]
  [Column(TypeName = "varchar")]
  public required string Description { get; set; }
}

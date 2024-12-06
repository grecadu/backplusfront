using Employee.Common.Common.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Common.Common;

public class BaseEntity : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DefaultValue("newsequentialid()")]
    [Column(Order = 0)]
    public virtual Guid Id { get; set; }
}

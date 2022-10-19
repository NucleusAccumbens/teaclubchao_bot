using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Honey")]
public class Honey : Product
{
    public HoneyWeight HoneyWeight { get; set; }
}


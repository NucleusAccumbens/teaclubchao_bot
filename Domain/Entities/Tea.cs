using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Teas")]
public class Tea : Product
{
    public TeaWeight? TeaWeight { get; set; }
    public TeaForms? TeaForm { get; set; }
    public TeaTypes? TeaType { get; set; }
}


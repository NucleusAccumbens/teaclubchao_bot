using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{

    [Table("Herbs")]
    public class Herb : Product
    {
        public HerbsRegion Region { get; set; }
        public HerbsWeight Weight { get; set; }
    }
}

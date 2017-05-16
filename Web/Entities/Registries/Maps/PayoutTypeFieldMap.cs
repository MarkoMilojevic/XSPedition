using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Entities.Registries.Maps
{
    public class PayoutTypeFieldMap
    {
        [Key]
        [Column(Order = 0)]
        public int PayoutTypeRegistryId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int FieldRegistryId { get; set; }

        [ForeignKey(nameof(PayoutTypeFieldMap.PayoutTypeRegistryId))]
        public virtual PayoutTypeRegistry PayoutType { get; set; }

        [ForeignKey(nameof(PayoutTypeFieldMap.FieldRegistryId))]
        public virtual FieldRegistry Field { get; set; }
    }
}
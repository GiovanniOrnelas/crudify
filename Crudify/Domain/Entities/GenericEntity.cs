using Crudify.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crudify.Domain.Entities
{
    public class GenericEntity : IEntity
    {
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        [Column("tenantId")]
        public long TenantId { get; set; }


        public virtual Tenant Tenant { get; set; }
    }
}

using Crudify.Domain.Entities;

namespace Crudify.Dto
{
    public class TenantResult
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public static explicit operator TenantResult(Tenant entity)
        {
            return new TenantResult
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}

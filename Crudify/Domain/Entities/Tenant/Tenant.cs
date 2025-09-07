using Crudify.Domain.Interfaces;

namespace Crudify.Domain.Entities
{
    public class Tenant : IEntity
    {
        protected Tenant() {}

        public Tenant(string name)
        {
            Name = name;
            CreatedAt = DateTime.UtcNow;
            Active = true;
        }

        public long Id { get; set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool Active { get; private set; }

        public void Update(string name, bool active)
        {
            Name = name;
            UpdatedAt = DateTime.UtcNow;
            Active = active;
        }

        public void Desactivate() => Active = false;

        public void Activate() => Active = true;
    }
}
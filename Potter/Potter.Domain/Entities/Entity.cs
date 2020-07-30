using System;

namespace Potter.Domain.Entities
{
    public abstract class Entity
    {
        public string Id { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString().Replace("-", "");
        }

    }
}

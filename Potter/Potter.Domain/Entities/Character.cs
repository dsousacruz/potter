
namespace Potter.Domain.Entities
{
    public class Character : Entity
    {
        public string Name { get; private set; }

        public string Role { get; private set; }

        public string School { get; private set; }

        public string House { get; private set; }

        public string Patronus { get; private set; }

        public Character(string name, string role, string school, string house, string patronus)
        {
            Name = name;
            Role = role;
            School = school;
            House = house;
            Patronus = patronus;
        }

        public void Update(string name, string role, string school, string house, string patronus)
        {
            Name = name;
            Role = role;
            School = school;
            House = house;
            Patronus = patronus;
        }
    }
}

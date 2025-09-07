using Crudify.Commons.Enums;

namespace Crudify.Domain.Entities
{
    public class User : GenericEntity
    {
        protected User() { }

        public User(string name, string password, UserType userType, string email, string document, DateTime birthDate, Gender gender)
        {
            Name = name;
            Password = password;
            Active = true;
            Type = userType;
            Email = email;
            Document = document;
            Birthdate = birthDate;
            Gender = gender;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public string Name { get; private set; }

        public string Password { get; set; }

        public bool Active { get; private set; }

        public UserType Type { get; private set; }

        public string Email { get; private set; }

        public string Document { get; private set; }

        public DateTime Birthdate { get; private set; }

        public Gender Gender { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }
    }
}
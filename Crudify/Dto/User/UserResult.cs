using Crudify.Domain.Entities;

namespace Crudify.Dto
{
    public class UserResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }


        public static explicit operator UserResult(User entity)
        {
            return new UserResult
            {
                Id = entity.Id,
                Name = entity.Name,
                Document = entity.Document,
                Email = entity.Email,
            };
        }
    }
}

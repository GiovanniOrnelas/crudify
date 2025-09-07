using Crudify.Commons.Enums;
using Crudify.Domain.Entities;
using Crudify.Infrastructure.JWT;
using System.ComponentModel.DataAnnotations;

namespace Crudify.Dto.JWT
{
    public class AuthResponse
    {
        public AuthResponse()
        {

        }

        public AuthResponse(User user, long tenantId)
        {
            Id = user.Id;
            Email = user.Email;
            Name = user.Name;
            Token = JwtAuthentication.Create(user, tenantId);
            UserType = user.Type;
            TenantId = tenantId;
            //Phone = user.Phones.Number;
        }


        public long Id { get; set; }

        [Required(ErrorMessage = "EmailRequired")]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public JwtModel Token { get; set; }

        public long TenantId { get; set; }
        public UserType UserType { get; }
    }
}
using Crudify.Commons.Enums;
using Crudify.Domain.Entities;
using Crudify.Infrastructure.JWT;
using System.ComponentModel.DataAnnotations;

namespace Crudify.Dto.Result
{
    public class AuthenticationResult
    {
        public AuthenticationResult()
        {

        }

        public AuthenticationResult(User user, long tenantId)
        {
            Id = user.Id;
            Email = user.Email;
            Name = user.Name;
            Token = JwtAuthentication.Create(user, tenantId);
            UserType = user.Type;
            TenantId = tenantId;
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
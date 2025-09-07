namespace Crudify.Dto.JWT
{
    public class JwtModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? Expires { get; internal set; }
    }
}

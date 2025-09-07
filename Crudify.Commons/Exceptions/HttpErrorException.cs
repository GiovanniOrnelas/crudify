using System.Net;

namespace Crudify.Commons.Exceptions
{
    public class HttpErrorException : Exception
    {
        private readonly List<string> _messages = new List<string>();
        public HttpStatusCode HttpStatusCode { get; }

        public HttpErrorException(string message, HttpStatusCode code = HttpStatusCode.InternalServerError) : base(message)
        {
            HttpStatusCode = code;
            _messages.Add(message);
        }

        public static HttpErrorException BadRequest(string message)
        {
            return new HttpErrorException(message, HttpStatusCode.BadRequest);
        }
    }
}
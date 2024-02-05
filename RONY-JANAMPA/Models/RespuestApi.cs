using System.Net;

namespace RONY_JANAMPA.Models
{
    public class RespuestApi
    {
        public RespuestApi() 
        { 
           ErrorMessage = new List<string>();   
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool isSuccess { get; set; } = true;
        public List<string> ErrorMessage { get; set;}
        public object Result { get; set; }
    }
}

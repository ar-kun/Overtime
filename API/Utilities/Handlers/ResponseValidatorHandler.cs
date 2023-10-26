using System.Net;

namespace API.Utilities.Handlers
{
    public class ResponseValidatorHandler
    {
        // Declares all public property for ResponseValidatorHandler class
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }

        public ResponseValidatorHandler(object error)
        {
            // Assigns the value of every ResponseValidatorHandler property.
            Code = StatusCodes.Status400BadRequest;
            Status = HttpStatusCode.BadRequest.ToString();
            Message = "Validation Error";
            Error = error;
        }
    }
}

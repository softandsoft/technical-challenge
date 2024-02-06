namespace TektonLabs.Domain
{
    public class WebApiResponse
    {
        public bool Success { get; set; }
        public Response Response { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Response
    {
        public object Data { get; set; }
    }

    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    /*
    {
      "success": false,
      "response": {
        "data": {objeto}
      },
      "errors": [
        {
          "code": 500,
          "message": "server 500 Error"
        }
      ]
    } 
     */
}

using MarsRoverAPI.Model;

namespace MarsRoverAPI.Responses
{
    public class ApiResponse<T> where T : IApiModel
    {
        public ApiResponse()
        {
            IsSuccess = true;
        }

        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
namespace Micro.AuthAPI.Models
{
    public class ResponseDTO
    {
        public DateTime TokenExpires { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string DisplayMessage { get; set; } = string.Empty;
        public object Result { get; set; }

    }
}

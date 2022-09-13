namespace TweetApp.Models.Requests
{
    public class ResetPasswordRequest
    {
        public string EmailId { get; set; }

        public string ContactNumber { get; set; }

        public string NewPassword { get; set; }
    }
}

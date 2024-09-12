namespace AccountManagement.Models
{
    public class FeedbackViewModel
    {
        public int FeedbackId { get; set; }
        public string CustomerName { get; set; }
        public string EmailAddress { get; set; }
        public string FeedbackType { get; set; }
        public string FeedbackMessage { get; set; }
        public string AppVersion { get; set; }
    }
}

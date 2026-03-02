namespace Entities.API.Entities.RequestModels
{
    public class CreateSubscriptionRequest
    {
        public string Type { get; set; }

        public string Status { get; set; }

        public string ExpiredAt { get; set; }

        public CreateSubscriptionRequest(string type, string status, string expiredAt)
        {
            Type = type;
            Status = status;
            ExpiredAt = expiredAt;
        }
    }
}
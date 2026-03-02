using Entities.API.Entities.RequestModels;
using Entities.API.Entities.ResponseModels;
using Entities.API.Entities.ResponseModels.Authentication;

namespace API.Interactions
{
    public class SubscriptionsApiClient : BaseApiClient
    {
        private const string BasicPath = "subscriptions/";

        public CreateSubscriptionResponse CreateSubscription(CreateSubscriptionRequest createSubscriptionRequest)
        {
            var response = Post<CreateSubscriptionResponse>(BasicPath, createSubscriptionRequest);
            return response;
        }
    }
}
using Entities.Common.Entities;

namespace Entities.API.Entities.RequestModels
{
    public class SignUpRequest : SignInRequest
    {
        public string Name { get; set; }

        public SignUpRequest(User user) : base(user)
        {
            Name = user.Name;
        }
    }
}
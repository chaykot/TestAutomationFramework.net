using Entities.Common.Entities;

namespace Entities.API.Entities.RequestModels
{
    public class SignInRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public SignInRequest(User user)
        {
            Email = user.Email;
            Password = user.Password;
        }
    }
}
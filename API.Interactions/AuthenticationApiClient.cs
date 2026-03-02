using System.Net;
using Entities.API.Entities.RequestModels;
using Entities.API.Entities.ResponseModels;
using Entities.API.Entities.ResponseModels.Authentication;
using Entities.Common.Entities;

namespace API.Interactions
{
    public class AuthenticationApiClient : BaseApiClient
    {
        private const string BasicPath = "auth/";

        public AuthenticationResponse SignIn(User user)
        {
            var apiAuthenticationResponse = SignIn<AuthenticationResponse>(user);
            AccessTokens.Value = apiAuthenticationResponse.AccessToken;
            return apiAuthenticationResponse;
        }

        public T SignIn<T>(User user, HttpStatusCode statusCode = HttpStatusCode.Created)
        {
            return Post<T>($"{BasicPath}signin", new SignInRequest(user), statusCode);
        }

        public AuthenticationResponse SignUp(User user)
        {
            var apiAuthenticationResponse = SignUp<AuthenticationResponse>(user);
            AccessTokens.Value = apiAuthenticationResponse.AccessToken;
            return apiAuthenticationResponse;
        }

        public T SignUp<T>(User user, HttpStatusCode statusCode = HttpStatusCode.Created)
        {
            return Post<T>($"{BasicPath}signup", new SignUpRequest(user), statusCode);
        }

        public ProfileResponse GetProfile(string token = null)
        {
            AccessTokens.Value = token ?? AccessTokens.Value;
            return Get<ProfileResponse>($"{BasicPath}profile");
        }
    }
}
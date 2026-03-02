namespace Entities.API.Entities.ResponseModels.Authentication
{
    public class AuthenticationResponse : AuthenticationData
    {
        public ProfileResponse User { get; set; }
    }
}
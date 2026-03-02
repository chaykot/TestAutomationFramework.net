using System;
using System.Collections.Generic;
using System.Net;
using API.Interactions;
using API.Tests.Constants;
using Entities.API.Entities.RequestModels;
using Entities.API.Entities.ResponseModels;
using Entities.Common.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace API.Tests
{
    public class DemoApiTests : BaseApiTest
    {
        private AuthenticationApiClient AuthenticationApiClient => new();
        private SubscriptionsApiClient SubscriptionsApiClient => new();
        private User _user;

        [SetUp]
        public void BeforeTest()
        {
            _user = User.GetRandomUser();
        }

        [TearDown]
        public void AfterTest()
        {
        }

        [Test]
        public void SignUpTest()
        {
            var response = AuthenticationApiClient.SignUp(_user);
            using (new AssertionScope())
            {
                response.AccessToken.Should().NotBeNullOrEmpty();
                response.User.Email.Should().Be(_user.Email);
                response.User.Name.Should().Be(_user.Name);
                Guid.TryParse(response.User.Id, out _).Should().BeTrue();
            }
        }

        [TestCase("ttt")]
        public void SignUpTest_InvalidEmail(string invalidEmail)
        {
            _user.Email = invalidEmail;
            var response = AuthenticationApiClient.SignUp<ErrorResponse<List<string>>>(_user, HttpStatusCode.BadRequest);
            using (new AssertionScope())
            {
                response.Error.Should().Be(HttpStatusCode.BadRequest);
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.Message.Should().BeEquivalentTo(new List<string> { ApiErrors.InvalidEmail });
            }
        }

        [Test]
        public void SignInTest()
        {
            AuthenticationApiClient.SignUp(_user);
            var response = AuthenticationApiClient.SignIn(_user);
            using (new AssertionScope())
            {
                response.AccessToken.Should().NotBeNullOrEmpty();
                response.User.Email.Should().Be(_user.Email);
                response.User.Name.Should().Be(_user.Name);
                Guid.TryParse(response.User.Id, out _).Should().BeTrue();
            }
        }

        [Test]
        public void GetProfileTest()
        {
            AuthenticationApiClient.SignUp(_user);
            AuthenticationApiClient.SignIn(_user);
            var response = AuthenticationApiClient.GetProfile();
            using (new AssertionScope())
            {
                response.Email.Should().Be(_user.Email);
                response.Name.Should().Be(_user.Name);
                Guid.TryParse(response.Id, out _).Should().BeTrue();
            }
        }

        [Test]
        public void CreateSubscriptionTest()
        {
            var subscription = new CreateSubscriptionRequest("web", "active", "2026-12-31T23:59:59.000Z");
            AuthenticationApiClient.SignUp(_user);
            AuthenticationApiClient.SignIn(_user);
            var response = SubscriptionsApiClient.CreateSubscription(subscription);
            using (new AssertionScope())
            {
                //response.Type.Should().Be(subscription.Type);
            }
        }
    }
}
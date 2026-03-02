using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using FluentAssertions;
using Infrastructure.Configuration;
using Infrastructure.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace API.Interactions
{
    public abstract class BaseApiClient
    {
        protected static readonly LogHelper Log = LogHelper.Instance;

        protected static readonly ThreadLocal<string> AccessTokens = new();

        protected RestClient RestClient { get; set; }

        protected JsonSerializerSettings JsonSerializerSettings { get; set; }

        protected BaseApiClient(string baseUrl = null)
        {
            RestClient = new RestClient(baseUrl ?? Configuration.Api.BaseUrl);
            JsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        protected virtual T Post<T>(string path, object body = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return SendRequestAndValidateStatusCode<T>(Method.POST, Request(path, body), statusCode);
        }

        //protected virtual void Post(string path, object body = null)
        //{
        //    SendRequestAndValidateSuccess(Method.POST, Request(path, body));
        //}

        //protected virtual T Put<T>(string path, object body = null)
        //{
        //    return SendRequestAndValidateSuccess<T>(Method.PUT, Request(path, body));
        //}

        //protected virtual void Put(string path, object body)
        //{
        //    SendRequestAndValidateSuccess(Method.PUT, Request(path, body));
        //}

        //protected virtual T Delete<T>(string path)
        //{
        //    return SendRequestAndValidateSuccess<T>(Method.DELETE, Request(path));
        //}

        //protected virtual void Delete(string path)
        //{
        //    SendRequestAndValidateSuccess(Method.DELETE, Request(path));
        //}

        protected virtual T Get<T>(string path, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return SendRequestAndValidateStatusCode<T>(Method.GET, Request(path), statusCode);
        }

        protected static RestRequest Request(string path) => new(path);

        protected IRestRequest Request(string path, object body)
        {
            if (body == null) return Request(path);
            var json = JsonConvert.SerializeObject(body, JsonSerializerSettings);
            return Request(path).AddParameter("application/json", json, ParameterType.RequestBody);
        }

        protected IRestResponse<T> SendRequest<T>(Method method, IRestRequest request, Dictionary<string, string> parameters = null)
        {
            AddParameters(request, parameters);
            LogRequest(method.ToString(), request);
            AddAccessToken(request);
            var response = RestClient.Execute<T>(request, method);
            LogResponse(response);
            return response;
        }

        protected virtual void AddAccessToken(IRestRequest request)
        {
            if (AccessTokens.Value != null)
            {
                request.AddHeader("Authorization", $"Bearer {AccessTokens.Value}");
            }
        }

        protected T SendRequestAndValidateStatusCode<T>(
            Method method, 
            IRestRequest request,
            HttpStatusCode statusCode, 
            Dictionary<string, string> parameters = null)
        {
            var response = SendRequest<T>(method, request, parameters);
            ValidateStatusCode(response, statusCode);
            return response.Data;
        }

        //private T SendRequestAndValidateSuccess<T>(Method method, IRestRequest request)
        //{
        //    LogRequest(method.ToString(), request);
        //    AddAccessToken(request);
        //    var response = RestClient.Execute<T>(request, method);
        //    LogResponse(response);
        //    ValidateRequest(method, response);
        //    return response.Data;
        //}

        //private void SendRequestAndValidateSuccess(Method method, IRestRequest request)
        //{
        //    var response = SendRequest(method, request);
        //    ValidateRequest(method, response);
        //}

        private static void ValidateRequest(Method method, IRestResponse response)
        {
            if (!response.IsSuccessful)
            {
                throw new ArgumentException(
                    $"Unsuccessful {method} '{response.ResponseUri}' " +
                    $"{(int)response.StatusCode} ({response.StatusDescription}): {response.Content}. " +
                    $"{response.Request.Body?.Value}");
            }
        }

        private static void ValidateStatusCode(IRestResponse response, HttpStatusCode statusCode)
        {
            response.StatusCode.Should().Be(statusCode,
                $"HTTP Status Code for '{response.Request.Method}' request to '{response.ResponseUri}' should be correct");
        }

        private static void AddParameters(IRestRequest request, Dictionary<string, string> parameters)
        {
            if (parameters != null)
            {
                foreach (var (paramName, paramValue) in parameters)
                {
                    request.AddOrUpdateParameter(paramName, paramValue);
                }
            }
        }

        private void LogRequest(string requestType, IRestRequest request)
        {
            Log.Api($"{requestType} request to {RestClient.BaseUrl}{request.Resource}");
            Log.Api($"Params: {JsonConvert.SerializeObject(request.Parameters)}");
        }

        private static void LogResponse(IRestResponse response)
        {
            Log.Api($"Response: {(int)response.StatusCode} {response.StatusDescription}");
            Log.Api($"Response body: {response.Content}");
        }
    }
}
using BMS.PublicTransport.Domain.Contract;
using BMS.PublicTransport.Domain.Contract.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BMS.PublicTransport.Domain
{
    public class VasttrafikService : IVasttrafikService
    {
        private readonly HttpClient _httpClient;

        public VasttrafikService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<VasttrafikResponse> Token()
        {
            var apiKey = Environment.GetEnvironmentVariable("VasttrafikKey");
            var deviceId = Environment.GetEnvironmentVariable("VasttrafikDeviceId");

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(deviceId))
            {
                return new VasttrafikResponse
                {
                    Data = new VasttrafikErrorResponse(),
                    Message = "API key or Device id not found"
                };
            }

            try
            {
                var uri = new Uri("https://api.vasttrafik.se/token");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", apiKey);
                var response = await _httpClient.PostAsync(uri, new StringContent($"grant_type=client_credentials&scope={deviceId}", Encoding.UTF8, "application/x-www-form-urlencoded"));

                var responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<VasttrafikTokenResponse>(responseBody);

                return new VasttrafikResponse
                {
                    Data = data,
                    AccessToken = data.AccessToken,
                    Success = response.StatusCode == HttpStatusCode.OK,
                    Code = (int)response.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new VasttrafikResponse
                {
                    Data = new VasttrafikErrorResponse(),
                    Message = ex.Message
                };
            }
        }

        public async Task<VasttrafikResponse> Revoke(string accessToken)
        {
            var apiKey = Environment.GetEnvironmentVariable("VasttrafikKey");

            if (string.IsNullOrEmpty(apiKey))
            {
                return new VasttrafikResponse
                {
                    Data = new VasttrafikErrorResponse(),
                    Message = "API key is null or empty"
                };
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                return new VasttrafikResponse
                {
                    Data = new VasttrafikErrorResponse(),
                    Message = "Access token is null or empty"
                };
            }

            try
            {
                var uri = new Uri("https://api.vasttrafik.se/revoke?format=json");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", apiKey);
                var response = await _httpClient.PostAsync(uri, new StringContent($"token={accessToken}", Encoding.UTF8, "application/x-www-form-urlencoded"));

                var responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<VasttrafikRevokeResponse>(responseBody);

                return new VasttrafikResponse
                {
                    Data = data,
                    Success = response.StatusCode == HttpStatusCode.OK,
                    Code = (int)response.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new VasttrafikResponse
                {
                    Data = new VasttrafikErrorResponse(),
                    Message = ex.Message
                };
            }
        }

        public async Task<VasttrafikResponse> DepartureBoard(string accessToken, string busStopId, DateTime departure)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return new VasttrafikResponse
                {
                    Data = new VasttrafikErrorResponse(),
                    Message = "Access token is null or empty"
                };
            }

            if (string.IsNullOrEmpty(busStopId))
            {
                return new VasttrafikResponse
                {
                    Data = new VasttrafikErrorResponse(),
                    Message = "Bus stop is null or empty"
                };
            }

            try
            {
                var date = departure.ToString("yyyy-MM-dd");
                var time = departure.ToString("HH:mm");
                var uri = new Uri($"https://api.vasttrafik.se/bin/rest.exe/v2/departureBoard?id={busStopId}&date={date}&time={time}&format=json");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await _httpClient.GetAsync(uri);
                var responseBody = await response.Content.ReadAsStringAsync();

                var departureBoard = JObject.Parse(responseBody).SelectToken("DepartureBoard").ToString();
                var data = JsonConvert.DeserializeObject<VasttrafikDepartureBoardResponse>(departureBoard);

                return new VasttrafikResponse
                {
                    AccessToken = accessToken,
                    Data = data,
                    Success = response.StatusCode == HttpStatusCode.OK,
                    Code = (int)response.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new VasttrafikResponse
                {
                    Data = new VasttrafikErrorResponse(),
                    Message = ex.Message
                };
            }
        }
    }
}

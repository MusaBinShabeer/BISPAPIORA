using System.Net.Http.Headers;
using System.Text;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.VerificationResponseDTO;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BISPAPIORA.Repositories.InnerServicesRepo
{
    public class InnerServices : IInnerServices
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _managementBaseUrl;
        public InnerServices(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _managementBaseUrl = configuration.GetSection("BISPAPI:BaseUrl").Value ?? "";
            
        }
        public async Task<ResponseModel<SurvayResponseDTO>> VerifyCitzen(string cnic)
        {
            try
            {
                var api = _managementBaseUrl + _configuration.GetSection("BISPAPI:VerifyCnic").Value;
                var requestUri = $"{api}?cnic={cnic}";
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
                var jwtToken = await GetToken();
                var handler = new HttpClientHandler();

                // Set the custom certificate validation callback to accept any certificate
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                var client = new HttpClient(handler);
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.data);
                }
                var httpResponseMessage = client.SendAsync(httpRequestMessage).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var response = await httpResponseMessage.Content.ReadAsStringAsync();
                    var ResponseModel = JsonConvert.DeserializeObject<ResponseDTO>(response);
                    if (ResponseModel != null)
                    {
                        if (ResponseModel.status == "ELIGIBLE")
                        {
                            return new ResponseModel<SurvayResponseDTO>() { data = ResponseModel.surveyDetails, success= true};
                        }
                        else
                            return new ResponseModel<SurvayResponseDTO>() { data = ResponseModel.surveyDetails!=null? ResponseModel.surveyDetails:null,success = false };
                    }
                    else
                        return new ResponseModel<SurvayResponseDTO>() { success = false };
                }
                else
                {
                    return new ResponseModel<SurvayResponseDTO> { success = false };
                }
            }
            catch (Exception)
            {
                return new ResponseModel<SurvayResponseDTO> { success = false };
            }
        }

        public async Task<ResponseModel> SendEmail(string to, string subject, string body)
        {
            try
            {
                var api = _managementBaseUrl + _configuration.GetSection("BISPAPI:SendOtp").Value;
                var requestUri = $"{api}?email={Uri.EscapeDataString(to)}&subject={Uri.EscapeDataString(subject)}&message={Uri.EscapeDataString(body)}";

                //var parameters = new Dictionary<string, string>
                //{
                //    { "email", to },
                //    { "subject", subject },
                //    { "message", body }
                //};

                //// Convert parameters to a JSON string
                //var jsonPayload = JsonConvert.SerializeObject(parameters);

                //// Set the content type to application/json
                //var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                //var content = new FormUrlEncodedContent(parameters);

                var jwtToken = await GetToken();
                var handler = new HttpClientHandler();

                // Set the custom certificate validation callback to accept any certificate
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                var client = new HttpClient(handler);
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.data);
                }
                var httpResponseMessage = await client.PostAsync(requestUri, null); 
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var response = await httpResponseMessage.Content.ReadAsStringAsync();
                    var ResponseModel = JsonConvert.DeserializeObject<bool>(response);
                    if (ResponseModel != null)
                    {
                        if (ResponseModel)
                        {
                            return new ResponseModel() { success = true };
                        }
                        else
                            return new ResponseModel() { success = false };
                    }
                    else
                        return new ResponseModel() { success = false };
                }
                else
                {
                    return new ResponseModel() { success = false };
                }
            }
            catch (Exception)
            {
                return new ResponseModel() { success = false };
            }
        }
        public async Task<ResponseModel<string>> GetToken()
        {
            try
            {
                var user = "bispsaving";
                var password = "HSPS@BISP@51Saving3";
                var api = _managementBaseUrl + _configuration.GetSection("BISPAPI:GetToken").Value;
                var requestUri = $"{api}?user={user}&password={password}";
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
                var handler = new HttpClientHandler();

                // Set the custom certificate validation callback to accept any certificate
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                var client = new HttpClient(handler);
               
                var httpResponseMessage = client.SendAsync(httpRequestMessage).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var response = await httpResponseMessage.Content.ReadAsStringAsync();
                    var ResponseModel = response/*JsonConvert.DeserializeObject<string>(response.ToString())*/;
                    if (ResponseModel != null)
                    {
                        
                            return new ResponseModel<string>() { data = ResponseModel, success = true };
                    
                    }
                    else
                        return new ResponseModel<string>() { success = false };
                }
                else
                {
                    return new ResponseModel<string> { success = false };
                }
            }
            catch (Exception)
            {
                return new ResponseModel<string> { success = false };
            }
        }
        //public async Task<ResponseModel> SendEmail(string to, string subject, string body)
        //{
        //    try
        //    {
        //        var api = _managementBaseUrl + _configuration.GetSection("BISPAPI:SendOtp").Value;
        //        var requestUri = api; // No need to append parameters to the URL

        //        var jwtToken = await GetToken();
        //        var handler = new HttpClientHandler();

        //        // Set the custom certificate validation callback to accept any certificate
        //        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

        //        using (var client = new HttpClient(handler))
        //        {
        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.data);

        //            // Create a dictionary for the parameters to be sent in the request body
        //            var parameters = new Dictionary<string, string>
        //    {
        //        { "to", to },
        //        { "subject", subject },
        //        { "body", body }
        //    };

        //            // Convert parameters to a JSON string
        //            var jsonPayload = JsonConvert.SerializeObject(parameters);

        //            // Set the content type to application/json
        //            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        //            // Make the POST request with the content in the request body
        //            var httpResponseMessage = await client.PostAsync(requestUri, content);

        //            if (httpResponseMessage.IsSuccessStatusCode)
        //            {
        //                var response = await httpResponseMessage.Content.ReadAsStringAsync();
        //                var ResponseModel = JsonConvert.DeserializeObject<ResponseDTO>(response);

        //                if (ResponseModel != null && ResponseModel.status == "ELIGIBLE")
        //                {
        //                    return new ResponseModel() { success = true };
        //                }
        //                else
        //                {
        //                    return new ResponseModel() { success = false };
        //                }
        //            }
        //            else
        //            {
        //                return new ResponseModel() { success = false };
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new ResponseModel() { success = false };
        //    }
        //}


    }
}

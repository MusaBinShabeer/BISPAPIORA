
using System.Net.Http.Headers;
using System.Text;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.VerificationResponseDTO;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using BISPAPIORA.Models.ENUMS;
using BISPAPIORA.Models.DTOS.InnerServicesDTO;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using System.Collections.Generic;

namespace BISPAPIORA.Repositories.InnerServicesRepo
{
    public class InnerServices : IInnerServices
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _managementBaseUrl;
        private readonly Dbcontext db;
        public InnerServices(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, Dbcontext db)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _managementBaseUrl = configuration.GetSection("BISPAPI:BaseUrl").Value ?? "";
            this.db = db;
            
        }

        // Verifies a citizen's eligibility by calling an external API with the provided CNIC (Computerized National Identity Card) number
        // Returns a response model containing survey details if the citizen is eligible; otherwise, returns a failure response
        public async Task<ResponseModel<SurvayResponseDTO>> VerifyCitzen(string cnic)
        {
            try
            {
                // Construct the API endpoint using the management base URL and the specified API endpoint for CNIC verification
                var api = _managementBaseUrl + _configuration.GetSection("BISPAPI:VerifyCnic").Value;
                var requestUri = $"{api}?cnic={cnic}";

                // Create a new HttpRequestMessage for a GET request to the constructed API endpoint
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

                // Obtain a JWT token for authentication
                var jwtToken = await GetToken();

                // Create a new HttpClientHandler
                var handler = new HttpClientHandler();

                // Set the custom certificate validation callback to accept any certificate
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                // Create a new HttpClient with the specified handler
                var client = new HttpClient(handler);

                // Set the Authorization header with the obtained JWT token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.data);

                // Send the HTTP request and await the response
                var httpResponseMessage = client.SendAsync(httpRequestMessage).Result;

                // Check if the HTTP response is successful
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var response = await httpResponseMessage.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a ResponseDTO object
                    var ResponseModel = JsonConvert.DeserializeObject<ResponseDTO>(response);

                    // Check if the ResponseDTO is not null
                    if (ResponseModel != null)
                    {
                        // Check the eligibility status in the ResponseDTO
                        if (ResponseModel.status == "ELIGIBLE")
                        {
                            // Return a success response model with survey details if the citizen is eligible
                            return new ResponseModel<SurvayResponseDTO>() { data = ResponseModel.surveyDetails, success = true };
                        }
                        else
                        {
                            // Return a failure response model if the citizen is not eligible
                            return new ResponseModel<SurvayResponseDTO>() { data = ResponseModel.surveyDetails != null ? ResponseModel.surveyDetails : null, success = false };
                        }
                    }
                    else
                    {
                        // Return a failure response model if the ResponseDTO is null
                        return new ResponseModel<SurvayResponseDTO>() { success = false };
                    }
                }
                else
                {
                    // Return a failure response model if the HTTP response is not successful
                    return new ResponseModel<SurvayResponseDTO> { success = false };
                }
            }
            catch (Exception)
            {
                // Return a failure response model if an exception occurs during the verification process
                return new ResponseModel<SurvayResponseDTO> { success = false };
            }
        }


        // Sends an email using an external API with the specified recipient, subject, and body
        public async Task<ResponseModel> SendEmail(string to, string subject, string body)
        {
            try
            {
                // Construct the API endpoint using the management base URL and the specified API endpoint for sending OTP emails
                var api = _managementBaseUrl + _configuration.GetSection("BISPAPI:SendOtp").Value;
                var requestUri = $"{api}?email={Uri.EscapeDataString(to)}&subject={Uri.EscapeDataString(subject)}&message={Uri.EscapeDataString(body)}";

                // Obtain a JWT token for authentication
                var jwtToken = await GetToken();

                // Create a new HttpClientHandler
                var handler = new HttpClientHandler();

                // Set the custom certificate validation callback to accept any certificate
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                // Create a new HttpClient with the specified handler
                var client = new HttpClient(handler);

                // Set the Authorization header with the obtained JWT token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.data);

                // Send a POST request to the API endpoint with the specified parameters
                var httpResponseMessage = await client.PostAsync(requestUri, null);

                // Check if the HTTP response is successful
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var response = await httpResponseMessage.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a boolean indicating the success of sending the email
                    var ResponseModel = JsonConvert.DeserializeObject<bool>(response);

                    // Check if the boolean response is not null
                    if (ResponseModel != null)
                    {
                        // Return a success response model if the email was sent successfully
                        if (ResponseModel)
                        {
                            return new ResponseModel() { success = true };
                        }
                        else
                        {
                            // Return a failure response model if the email sending process failed
                            return new ResponseModel() { success = false };
                        }
                    }
                    else
                    {
                        // Return a failure response model if the boolean response is null
                        return new ResponseModel() { success = false };
                    }
                }
                else
                {
                    // Return a failure response model if the HTTP response is not successful
                    return new ResponseModel() { success = false };
                }
            }
            catch (Exception)
            {
                // Return a failure response model if an exception occurs during the email sending process
                return new ResponseModel() { success = false };
            }
        }

        // Retrieves an authentication token from an external API using a specified username and password
        public async Task<ResponseModel<string>> GetToken()
        {
            try
            {
                // Specify the API credentials (username and password)
                var user = "bispsaving";
                var password = "HSPS@BISP@51Saving3";

                // Construct the API endpoint using the management base URL and the specified API endpoint for obtaining a token
                var api = _managementBaseUrl + _configuration.GetSection("BISPAPI:GetToken").Value;
                var requestUri = $"{api}?user={user}&password={password}";

                // Create a new HttpRequestMessage for making a GET request to the API endpoint
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

                // Create a new HttpClientHandler
                var handler = new HttpClientHandler();

                // Set the custom certificate validation callback to accept any certificate
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                // Create a new HttpClient with the specified handler
                var client = new HttpClient(handler);

                // Send the GET request to the API endpoint and await the response
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);

                // Check if the HTTP response is successful
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var response = await httpResponseMessage.Content.ReadAsStringAsync();

                    // Assign the response directly to the ResponseModel's data property (string)
                    var ResponseModel = response;

                    // Check if the response data is not null
                    if (ResponseModel != null)
                    {
                        // Return a success response model with the retrieved token
                        return new ResponseModel<string>() { data = ResponseModel, success = true };
                    }
                    else
                    {
                        // Return a failure response model if the response data is null
                        return new ResponseModel<string>() { success = false };
                    }
                }
                else
                {
                    // Return a failure response model if the HTTP response is not successful
                    return new ResponseModel<string> { success = false };
                }
            }
            catch (Exception)
            {
                // Return a failure response model if an exception occurs during the token retrieval process
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
        public List<int> GetQuarterCodesBetween(int startingQuarterCode, int currentQuarterCode) 
        {
            List<int> quarterCodes = new List<int>();

            for (int code = startingQuarterCode; code <= currentQuarterCode; code++)
            {
                quarterCodes.Add(code);
            }
            return quarterCodes;
        }
        public async Task<double> GetTotalExpectedSavingAmount(List<int> quarterCodes, Guid fk_citizen,double expectedSavingAmountPerQuarter) 
        {
           
            double totalActualExpected = 0;
            foreach(int code in quarterCodes)
            {
                totalActualExpected = await GetExpectedSavingAmount(code, fk_citizen, expectedSavingAmountPerQuarter, totalActualExpected);
            }
            return totalActualExpected;

            
        }
        public async Task<double> GetTotalActualDueAmount(List<int> quarterCodes, Guid fk_citizen)
        {
            double total = 0;
            foreach (int code in quarterCodes)
            {
                var amountDue = await GetActualDueAmount(code, fk_citizen);

                total = total + amountDue;
            }
            return total;


        }
        public async Task<double> GetExpectedSavingAmount(int quarterCode, Guid fk_citizen, double expectedSavingAmountPerQuarter, double totalActualExpected) 
        { 
            var compliance= await db.tbl_citizen_compliances.Where(x=>x.fk_citizen==fk_citizen && x.citizen_compliance_quarter_code== quarterCode).Include(x=>x.tbl_transactions).FirstOrDefaultAsync();
            if(compliance!=null) 
            {
                if (compliance.tbl_transactions.Count() > 0)
                {
                    var amountSavedDecimal = compliance.tbl_transactions.Sum(transaction =>
                    {
                        if (Enum.TryParse(transaction.transaction_type, out TransactionTypeEnum transactionType))
                        {
                            if (double.TryParse(transaction.transaction_amount.ToString(), out double transactionAmount))
                            {
                                return transactionType == TransactionTypeEnum.Debit ? -transactionAmount : +transactionAmount;
                            }
                            else
                            {
                                // Handle parsing error for transaction amount
                                Console.WriteLine("Invalid transaction amount: " + transaction.transaction_amount);
                                return 0; // or any default value
                            }
                        }
                        else
                        {
                            // Handle parsing error for transaction type
                            Console.WriteLine("Invalid transaction type: " + transaction.transaction_type);
                            return 0; // or any default value
                        }
                    });
                    var amountSaved = double.Parse(amountSavedDecimal.ToString());
                    if (amountSaved == (totalActualExpected+expectedSavingAmountPerQuarter) && amountSaved> totalActualExpected + expectedSavingAmountPerQuarter)
                    {
                        return 0;
                    }
                    else
                    {
                        var difference = (totalActualExpected + expectedSavingAmountPerQuarter) - amountSaved;
                        return difference > 0 ?  difference : 0;
                    }
                }
                else
                {
                    return (totalActualExpected + expectedSavingAmountPerQuarter);
                }
            }
            else 
            {
                return  (totalActualExpected + expectedSavingAmountPerQuarter);
            }
        }
        public async Task<double> GetActualDueAmount(int quarterCode, Guid fk_citizen)
        {
            var payment = await db.tbl_payments.Where(x => x.fk_citizen == fk_citizen && x.payment_quarter_code == quarterCode).FirstOrDefaultAsync();
            if (payment != null)
            {
                
                var actualDue=double.Parse(( payment.quarterly_due_amount- payment.paid_amount).ToString());
                return actualDue;
                
            }
            else
            {
                return 0;
            }
        }
        public List<QuarterCodesReponseDTO> GetAllQuarterCodes(int startingQuarterCode)
        {
            List<QuarterCodesReponseDTO> quarterCodes = new List<QuarterCodesReponseDTO>();

            for (int x = 1; x <= 8; x++)
            {
                quarterCodes.Add(new QuarterCodesReponseDTO() 
                {
                    quarterCode=startingQuarterCode,
                    quarterCodeName=$"Q{x}",
                });
                startingQuarterCode++;
            }
            return quarterCodes;
        }
        public QuarterEnum GetQuarter(int month)
        {
            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month), "Month should be between 1 and 12.");

            if (month >= 1 && month <= 3)
                return QuarterEnum.Q1;
            else if (month >= 4 && month <= 6)
                return QuarterEnum.Q2;
            else if (month >= 7 && month <= 9)
                return QuarterEnum.Q3;
            else
                return QuarterEnum.Q4;
        }
        public async Task<Boolean> CheckCompliance(List<int> quarterCodes,Guid citizenId)
        {
            var month=DateTime.Now.Month;
            var year=DateTime.Now.Year;
            var currentQuarter= GetQuarter(month);
            var currentQuarterIndex = (int)Enum.Parse<QuarterIndexEnum>(currentQuarter.ToString());
            var currentQuarterCode= (year*4) + currentQuarterIndex;
            if (quarterCodes.Contains(currentQuarterCode))
            {
                var compliance= await db.tbl_citizen_compliances
                    .Where(x=>x.citizen_compliance_quarter_code.Equals(currentQuarterCode) && x.fk_citizen== citizenId)
                    .FirstOrDefaultAsync();
                return compliance!=null? compliance.is_compliant.Value : false;
            }
            else
            {
                var compliance = await db.tbl_citizen_compliances
                   .Where(x => x.citizen_compliance_quarter_code.Equals(quarterCodes.LastOrDefault()) && x.fk_citizen == citizenId)
                   .FirstOrDefaultAsync();
                return compliance != null ? compliance.is_compliant.Value : false;
            }
        }
        public async Task<int> CheckCompliance(List<DashboardCitizenLocationModel> citizens)
        {
            var count = 0;
            foreach (var citizen in citizens)
            {
                var quarterCodesDto = GetAllQuarterCodes(citizen.tbl_citizen_scheme.citizen_scheme_quarter_code.Value);
                var quarterCodes =quarterCodesDto.Select(x=>x.quarterCode).ToList();
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var currentQuarter = GetQuarter(month);
                var currentQuarterIndex = (int)Enum.Parse<QuarterIndexEnum>(currentQuarter.ToString());
                var currentQuarterCode = (year * 4) + currentQuarterIndex;
                if (quarterCodes.Contains(currentQuarterCode))
                {
                    var compliance = await db.tbl_citizen_compliances
                        .Where(x => x.citizen_compliance_quarter_code.Equals(currentQuarterCode) && x.fk_citizen == citizen.citizen_id)
                        .FirstOrDefaultAsync();
                    if (compliance != null)
                    {
                        if(compliance.is_compliant == true)
                        {
                            count++;
                        };
                    }
                }
                else
                {
                    var compliance = await db.tbl_citizen_compliances
                       .Where(x => x.citizen_compliance_quarter_code.Equals(quarterCodes.LastOrDefault()) && x.fk_citizen == citizen.citizen_id)
                       .FirstOrDefaultAsync();
                    if (compliance != null)
                    {
                        if (compliance.is_compliant == true)
                        {
                            count++;
                        };
                    }
                }
            }
            return count;
        }
        public async Task<List<DashboardCitizenLocationModel>> GetComplaintCitizen(List<DashboardCitizenLocationModel> citizens)
        {
            var count = 0;
            List<DashboardCitizenLocationModel> compliiantCitizen= new List<DashboardCitizenLocationModel>();
            foreach (var citizen in citizens)
            {
                var quarterCodesDto = GetAllQuarterCodes(citizen.tbl_citizen_scheme.citizen_scheme_quarter_code.Value);
                var quarterCodes = quarterCodesDto.Select(x => x.quarterCode).ToList();
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var currentQuarter = GetQuarter(month);
                var currentQuarterIndex = (int)Enum.Parse<QuarterIndexEnum>(currentQuarter.ToString());
                var currentQuarterCode = (year * 4) + currentQuarterIndex;
                if (quarterCodes.Contains(currentQuarterCode))
                {
                    var compliance = await db.tbl_citizen_compliances
                        .Where(x => x.citizen_compliance_quarter_code.Equals(currentQuarterCode) && x.fk_citizen == citizen.citizen_id)
                        .FirstOrDefaultAsync();
                    if (compliance != null)
                    {
                        if (compliance.is_compliant == true)
                        {
                            compliiantCitizen.Add(citizen);
                        };
                    }
                }
                else
                {
                    var compliance = await db.tbl_citizen_compliances
                       .Where(x => x.citizen_compliance_quarter_code.Equals(quarterCodes.LastOrDefault()) && x.fk_citizen == citizen.citizen_id)
                       .FirstOrDefaultAsync();
                    if (compliance != null)
                    {
                        if (compliance.is_compliant == true)
                        {
                            compliiantCitizen.Add(citizen);
                        };
                    }
                }
            }
            return compliiantCitizen;
        }

    }
}

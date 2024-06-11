using System.Text;
using System.Security.Cryptography;
using BISPAPIORA.Models.ENUMS;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using BISPAPIORA.Models.DTOS.InnerServicesDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Extensions
{
    public class OtherServices
    {
        public bool Check(object model)
        {
            try
            {
                if (model.GetType() == typeof(byte[]))
                {
                    byte[] newLogo = Convert.FromBase64String(model.ToString());
                    if (newLogo.Length > 0 && newLogo != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (!string.IsNullOrEmpty(model.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string encodePassword(string password)
        {
            string currentPassword = "";
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] hashValue =
                mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
                currentPassword = Convert.ToBase64String(hashValue);
            }
            return currentPassword;
        }
        public string GenerateOTP(int length)
        {
            // Use a random number generator to create a random OTP
            Random random = new Random();
            string otp = string.Empty;

            for (int i = 0; i < length; i++)
            {
                otp += random.Next(0, 9).ToString();
            }

            return otp;
        }
        public double getTotalSavings(CitizenBaseModel citizen)
        {
            try
            {
                return citizen.tbl_transactions.Count() > 0 ? double.Parse(( citizen.tbl_transactions.Sum(transaction =>
                {
                    if (Enum.TryParse(transaction.transaction_type, out TransactionTypeEnum transactionType))
                    {

                        return transactionType == TransactionTypeEnum.Debit ? -transaction.transaction_amount : +transaction.transaction_amount;

                    }
                    else
                    {
                        // Handle parsing error for transaction type
                        Console.WriteLine("Invalid transaction type: " + transaction.transaction_type);
                        return 0; // or any default value
                    }
                })+ (citizen.tbl_citizen_compliances.OrderBy(x=>x.citizen_compliance_quarter_code).FirstOrDefault().starting_balance_on_quarterly_bank_statement.Value)).ToString()):0;
            }
            catch(Exception e) { return 0; }
        }
        public double getTotalSavings(tbl_citizen citizen)
        {
            try
            {
                return citizen.tbl_transactions.Count()>0?double.Parse((citizen.tbl_transactions.Sum(transaction =>
                {
                    if (Enum.TryParse(transaction.transaction_type, out TransactionTypeEnum transactionType))
                    {

                        return transactionType == TransactionTypeEnum.Debit ? -transaction.transaction_amount : +transaction.transaction_amount;

                    }
                    else
                    {
                        // Handle parsing error for transaction type
                        Console.WriteLine("Invalid transaction type: " + transaction.transaction_type);
                        return 0; // or any default value
                    }
                })+ (citizen.tbl_citizen_compliances.OrderBy(x => x.citizen_compliance_quarter_code).FirstOrDefault().starting_balance_on_quarterly_bank_statement.Value)).ToString()):0;
            }
            catch (Exception e) { return 0; }
        }
        public List<QuarterCodesReponseDTO> GetAllQuarterCodes(int startingQuarterCode)
        {
            List<QuarterCodesReponseDTO> quarterCodes = new List<QuarterCodesReponseDTO>();

            for (int x = 1; x <= 8; x++)
            {
                quarterCodes.Add(new QuarterCodesReponseDTO()
                {
                    quarterCode = startingQuarterCode,
                    quarterCodeName = $"Q{x}",
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
        public Boolean CheckCompliance(Guid citizenId, CitizenBaseModel citizen)
        {
            if (citizen.tbl_enrollment != null && citizen.tbl_citizen_scheme != null)
            {
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var currentQuarter = GetQuarter(month);
                var currentQuarterIndex = (int)Enum.Parse<QuarterIndexEnum>(currentQuarter.ToString());
                var currentQuarterCode = (year * 4) + currentQuarterIndex;
                var quarterCodesDTO = GetAllQuarterCodes(citizen.tbl_citizen_scheme.citizen_scheme_quarter_code.Value);
                var quarterCodes = quarterCodesDTO.Select(x => x.quarterCode).ToList();
                if (quarterCodes.Contains(currentQuarterCode))
                {
                    var compliance = citizen.tbl_citizen_compliances
                        .Where(x => x.citizen_compliance_quarter_code.Equals(currentQuarterCode) && x.fk_citizen == citizenId)
                        .FirstOrDefault();
                    return compliance != null ? compliance.is_compliant.Value : false;
                }
                else
                {
                    var compliance = citizen.tbl_citizen_compliances
                                       .Where(x => x.fk_citizen == citizen.citizen_id)
                                       .OrderBy(x => x.citizen_compliance_quarter_code)
                                       .LastOrDefault();
                    return compliance != null ? compliance.is_compliant.Value : false;

                }
            }
            else
            {
                return false;
            }
        }

    }
}

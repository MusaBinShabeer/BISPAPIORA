using System.Text;
using System.Security.Cryptography;
using BISPAPIORA.Models.ENUMS;

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
      
    }
}

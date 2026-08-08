using Application.Abstractions.Security;
using System.Security.Cryptography;

namespace Infrastructure.Security
{
    internal class OTPGenerator : IOTPGenerator
    {
        public string GenerateOTP()
        {
            //Generate a random 6-digit OTP
            int OtpCode = RandomNumberGenerator.GetInt32(0, 1000000);

            // Format as 6 digits with leading zeros if necessary
            string OtpCodeString = OtpCode.ToString("D6"); 

            return OtpCodeString;
        }
    }
}

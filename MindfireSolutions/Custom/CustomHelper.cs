using System.Security.Cryptography;
using System.Text;

namespace MindfireSolutions.Custom
{
    /// <summary>
    /// Returns a 32 bit hash.
    /// </summary>
    public class CustomHelper : ICustomHelper
    {
        public string HashValue(string value)
        {
            byte[] byteCode = ASCIIEncoding.ASCII.GetBytes(value);
            byte[] byteHash = new MD5CryptoServiceProvider().ComputeHash(byteCode);
            return (ByteArrayToString(byteHash));
        }
        private string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}
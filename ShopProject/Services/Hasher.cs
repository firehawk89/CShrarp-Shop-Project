using System.Text;
using System.Security.Cryptography;

namespace ShopProject.Services
{
    public static class Hasher
    {
        public static string Hash(string password)
        {
            var source = ASCIIEncoding.ASCII.GetBytes(password);
            var hash = new MD5CryptoServiceProvider().ComputeHash(source);

            return ByteArrayToString(hash);
        }
        public static bool CompareHashValues(string userPassword, string password)
        {
            var source = ASCIIEncoding.ASCII.GetBytes(userPassword);
            var hash = new MD5CryptoServiceProvider().ComputeHash(source);

            source = ASCIIEncoding.ASCII.GetBytes(password);
            var newHash = new MD5CryptoServiceProvider().ComputeHash(source);

            bool bEqual = false;
            if (newHash.Length == hash.Length)
            {
                int i = 0;
                while ((i < newHash.Length) && (newHash[i] == hash[i]))
                {
                    i += 1;
                }
                if (i == newHash.Length)
                {
                    bEqual = true;
                }
            }
            return bEqual;
        }
        private static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}

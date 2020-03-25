using System;
using System.Security.Cryptography;
using System.Text;

namespace GPD.Commom.Utils
{
    public static class Cryptography
    {
        private const string securityKey = "gpD@7![23.Le";

        public static string Encrypt(this string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Senha não definida.");
            }

            using var provider = new TripleDESCryptoServiceProvider();
            using var md5 = new MD5CryptoServiceProvider();
            var bytes = Encoding.ASCII.GetBytes(securityKey);
            provider.Key = md5.ComputeHash(bytes);
            provider.Mode = CipherMode.ECB;
            var encryptor = provider.CreateEncryptor();
            var buffer = Encoding.ASCII.GetBytes(password);
            var encryptorBytes = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
            string result = Convert.ToBase64String(encryptorBytes);
            return result;
        }

        public static string Decrypt(this string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Senha não definida.");
            }

            using var provider = new TripleDESCryptoServiceProvider();
            using var md5 = new MD5CryptoServiceProvider();
            var bytes = Encoding.ASCII.GetBytes(securityKey);
            provider.Key = md5.ComputeHash(bytes);
            provider.Mode = CipherMode.ECB;
            var decryptor = provider.CreateDecryptor();
            var buffer = Convert.FromBase64String(password);
            var decriptorBytes = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
            string result = Encoding.ASCII.GetString(decriptorBytes);
            return result;
        }
    }
}

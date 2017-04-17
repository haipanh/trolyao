using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Application.Service
{
    public class SecurityService
    {
        public static string GetMD5Hash(string input)
        {
            byte[] originalBytes = Encoding.UTF8.GetBytes(input);

            MD5 encryptType = new MD5CryptoServiceProvider();

            byte[] encodedBytes = encryptType.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }

        public static string GenerateRandomString(int length, CharSets charSet)
        {
            switch (charSet)
            {
                case CharSets.Letters:
                    return GenerateRandomString(length, "abcdefghijklmnopqrstvwxyz");

                case CharSets.Numerics:
                    return GenerateRandomString(length, "0123456789");

                case CharSets.Alphanumeric:
                    return GenerateRandomString(length, "abcdefghijklmnopqrstvwxyz0123456789");

                default:
                    break;
            }

            return String.Empty;
        }

        protected static string GenerateRandomString(int length, string charSet)
        {
            return GenerateRandomString(length, charSet.ToCharArray());
        }

        protected static string GenerateRandomString(int length, params char[] charSet)
        {
            Random random = new Random();
            string randomString = string.Empty;
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(charSet.Length - 1);

                randomString += charSet[index];
            }

            return randomString;
        }
    }

    public enum CharSets
    {
        Letters,
        Numerics,
        Alphanumeric
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Nep.Project.Common
{
    public static class PasswordEncrypter
    {
        private static readonly Char[] VALID_RANDOM_CHARS = { '$', '%', '&', '+', ',', '-', '.', '1', '2', '3', '4', '5', '6', '7', '8', '9', '=', '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', ']', '^', '_', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', };
        private const Int32 ITERATE_COUNT = 100;
        public const Int32 SALT_LENGTH = 10;
        public const Int32 ACTIVATION_CODE_LENGTH = 8;
        public const Int32 RANDOM_PASSWORD_LENGTH = 8;
        public static readonly System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("TIS-620");
        public static String GenerateSalt()
        {
            var salt = String.Empty;
            using (var random = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                var bytes = new Byte[SALT_LENGTH];
                random.GetBytes(bytes);
                for (int i = 0; i < SALT_LENGTH; i++)
                {
                    if (bytes[i] < 32)
                    {
                        bytes[i] += 32;
                    }
                }

                salt = encoding.GetString(bytes);
            }

            return salt;
        }

        public static String GeneratePassword()
        {
            Random r = new Random();
            StringBuilder builder = new StringBuilder();
            Int32 maxIndex = VALID_RANDOM_CHARS.Length - 1;
            for (int i = 0; i < RANDOM_PASSWORD_LENGTH; i++)
            {
                builder.Append(VALID_RANDOM_CHARS[r.Next(0, maxIndex)]);
            }

            return builder.ToString();
        }

        public static String GenerateActivationCode()
        {
            Random r = new Random();
            StringBuilder builder = new StringBuilder();
            Int32 maxIndex = VALID_RANDOM_CHARS.Length - 1;
            for (int i = 0; i < ACTIVATION_CODE_LENGTH; i++)
            {
                builder.Append(VALID_RANDOM_CHARS[r.Next(0, maxIndex)]);
            }

            return builder.ToString();
        }

        public static Byte[] Encrypt(String password, String salt)
        {
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            var saltBytes = System.Text.Encoding.UTF8.GetBytes(salt);
            using (var hash = CreateHashAlgo())
            {
                var encryptedBytes = hash.ComputeHash(passwordBytes);
                int hashLength = hash.HashSize / 8;
                int saltLength = saltBytes.Length;
                int passwordLength = passwordBytes.Length;

                Byte[] data1 = new Byte[hashLength + saltLength];
                saltBytes.CopyTo(data1, hashLength);
                Byte[] data2 = new Byte[hashLength + passwordLength];
                passwordBytes.CopyTo(data2, hashLength);

                for (int i = 1; i <= ITERATE_COUNT; i++)
                {
                    if (i % 2 == 0)
                    {
                        encryptedBytes.CopyTo(data1, 0);
                        encryptedBytes = hash.ComputeHash(data1);
                    }
                    else
                    {
                        encryptedBytes.CopyTo(data2, 0);
                        encryptedBytes = hash.ComputeHash(data2);
                    }
                }
                return encryptedBytes;
            }
        }

        private static System.Security.Cryptography.HashAlgorithm CreateHashAlgo()
        {
            return System.Security.Cryptography.SHA512.Create();
        }
    }
}

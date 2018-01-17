using System;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace MIAAPI.Utilities
{
    public class AuthTokenOption
    {
        public static string Audience { get; } = "Member";
        public static string Issuer { get; } = "MCIA";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(40);
        public static string TokenType { get; } = "Bearer";
    }

    public class RSAKeyHelper
    {
        public static RSAParameters GenerateKey()
        {
            using (var key = new RSACryptoServiceProvider(2048))
            {
                return key.ExportParameters(true);
            }
        }
    }
}

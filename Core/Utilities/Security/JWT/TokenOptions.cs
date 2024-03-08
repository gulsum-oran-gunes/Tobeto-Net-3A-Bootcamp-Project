using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class TokenOptions
    {
        public string Audience { get; set; } //kimlik doğrulama, tokenin hedef kitlesini temsil eden özellik
        public string Issuer { get; set; } // bir kimlik doğrulama işleminde tokeni veren kim
        public int AccessTokenExpiration { get; set; } // geçerlilik süresi
        public string SecurityKey { get; set; } // kimlik doğrulama için kullanılan güvenlik anahtarı
    }
}

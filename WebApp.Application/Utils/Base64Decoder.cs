using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApp.Application.Utils
{
    public class Base64Decoder
    {
        public byte[] Decode(string base64String, out string type)
        {
            var match = Regex.Match(base64String, @"^data:image\/([a-zA-Z]+);base64,");

            if (match.Success)
            {
                type = match.Groups[1].Value;
            }
            else
            {
                type = null;
            }
            var base64Data = Regex.Replace(base64String, @"^data:image\/[a-zA-Z]+;base64,", "");

            return Convert.FromBase64String(base64Data);
        }

        public string Encode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}

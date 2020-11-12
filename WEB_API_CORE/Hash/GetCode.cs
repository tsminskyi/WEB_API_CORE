using System;
using System.Security.Cryptography;
using System.Text;

namespace WEB_API_CORE.Hash
{
    public class GetCode
    {

        public static string Hash(string password)
        {

            byte[] bytes = Encoding.Unicode.GetBytes(password);

           
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hashed = string.Empty;

            
            foreach (byte b in byteHash)
                hashed += string.Format("{0:x2}", b);



            return hashed;
        }
       
    }
}

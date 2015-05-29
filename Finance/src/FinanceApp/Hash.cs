using System.Security.Cryptography;
using System.Text;
using System;

namespace App
{
    public static class Hash
    {
        public static string GetHash(string word){
            var sha1 = new SHA1Managed();
         
            return Convert.ToBase64String(sha1.ComputeHash(Encoding.Default.GetBytes(word)));   
        }
    }
}
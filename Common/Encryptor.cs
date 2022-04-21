using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ZealEducationManager.Common
{
    public static class Encryptor
    {
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < strBuilder.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2")); //formatted in Hexadecimal (thap luc phan)
            }
            return strBuilder.ToString();
        }
    }
}
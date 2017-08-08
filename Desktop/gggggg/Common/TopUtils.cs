using System;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DXzonghejiaofei.Common
{
    public class TopUtils
    {
        public static string GetRegexString(string input,string pattern)
        {
            var match = Regex.Match(input,pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        public static string GetTimeStamp()
        {
            Thread.Sleep(1);
            var dt = new DateTime(1970,1,1,0,0,0,0);
            return Math.Round((DateTime.Now-dt).TotalMilliseconds).ToString(CultureInfo.CurrentCulture);
        }

        public static string Md5Encrypt(string sourceString,Encoding enc = null)
        {
            if(enc==null) enc=Encoding.UTF8;
            var buffer = MD5.Create().ComputeHash(enc.GetBytes(sourceString));
            var builder = new StringBuilder();
            foreach(var t in buffer)
            {
                builder.Append(t.ToString("x").PadLeft(2,'0'));
            }
            return builder.ToString();
        }
        public static string EncryptAES(string toEncrypt, string key)
        {

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        //解密
        public static string Decrypt(string toDecrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public static string PostHtml(string url,string postData,Encoding enc = null,WebProxy proxy=null)
        {
            var data = Encoding.UTF8.GetBytes(postData);
            if(enc==null) enc=Encoding.UTF8;
            var webClient = new WebClient() { Encoding=enc,Proxy =proxy };
            webClient.Headers.Add(HttpRequestHeader.KeepAlive,"False");
            webClient.Headers.Add("Content-Type","application/x-www-form-urlencoded");
            var response = webClient.UploadData(url,"POST",data);//得到返回字符流  
            return enc.GetString(response);//解码  
        }

        public static WebProxy GetProxy(string url,string port,string usr,string pwd)
        {
            try
            {
                var client=new WebClient {Encoding = Encoding.UTF8};
                var ip = client.DownloadString(url).Trim();
                var proxy = new WebProxy($"{ip}:{port}")
                {
                    Credentials=new NetworkCredential(usr,pwd)
                };
                return proxy;
            }
            catch
            {
                return null;
            }
        }

    }
}
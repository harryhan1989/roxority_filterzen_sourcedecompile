using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Common
{
    public class Security
    {
        /// <summary>
        /// 解密KEY, 客户端那边不能使用此值。 只能运用于服务端。
        /// </summary>
        public static readonly String Key = "kXwL7X2+fgM=";

        /// <summary>
        ///  加密EncryptKey
        /// </summary>
        public static readonly String EncryptKey = "FwGQWRRgKCI=";
        //默认密钥向量
        private static readonly byte[] Keys = { 0x12, 0x24, 0x52, 0x48, 0x92, 0x27, 0x72, 0x69 };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns>加密成功返回加密后的字符串, 有异常返回 NULL </returns>
        public static String Encrypt(String encryptString)
        {
            try
            {
                byte[] rgbKey = Convert.FromBase64String(EncryptKey);
                byte[] rgbIv = Convert.FromBase64String(Key);
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString.Trim());
                using (var dCsp = new DESCryptoServiceProvider())
                {
                    using (var mStream = new MemoryStream())
                    {
                        using (var cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write))
                        {
                            cStream.Write(inputByteArray, 0, inputByteArray.Length);
                            cStream.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(mStream.ToArray());
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串</returns>
        /// <param name="keys"></param>
        public static String Decrypt(String decryptString, String keys)
        {
            try
            {
                byte[] rgbKey = Convert.FromBase64String(EncryptKey);
                byte[] rgbIv = Convert.FromBase64String(keys);
                byte[] inputByteArray = Convert.FromBase64String(decryptString.Trim());
                using (var dcsp = new DESCryptoServiceProvider())
                {
                    using (var mStream = new MemoryStream())
                    {
                        using (var cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write))
                        {
                            cStream.Write(inputByteArray, 0, inputByteArray.Length);
                            cStream.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
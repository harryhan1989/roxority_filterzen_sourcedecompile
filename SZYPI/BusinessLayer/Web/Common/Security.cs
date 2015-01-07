using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace BusinessLayer.Web.Common
{
    public class Security
    {
        const string _QueryStringKey = "ESICESIC"; //����Key
       
        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="QueryString"></param>
        /// <returns></returns>
        public static string EncryptQueryString(string QueryString)
        {
            return new Security().Encrypt(QueryString, _QueryStringKey);
        }
          
        /// <summary>
        /// �����ַ���  
        /// </summary>
        /// <param name="QueryString"></param>
        /// <returns></returns>
        public static string DecryptQueryString(string QueryString)
        {
            return new Security().Decrypt(QueryString, _QueryStringKey);
        }

        /// <summary>
        /// DEC ���ܹ��� 
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        private string Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();  //���ַ����ŵ�byte������ 
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            //byte[] inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt); 
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);                   //�������ܶ������Կ��ƫ���� 
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);                    //ԭ��ʹ��ASCIIEncoding.ASCII������GetBytes���� 
            MemoryStream ms = new MemoryStream();                           //ʹ�����������������Ӣ���ı� 
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// DEC ���ܹ���
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        private string Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);               //�������ܶ������Կ��ƫ��������ֵ��Ҫ�������޸� 
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();                    //����StringBuild����CreateDecryptʹ�õ��������󣬱���ѽ��ܺ���ı���������� 
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
    }
}


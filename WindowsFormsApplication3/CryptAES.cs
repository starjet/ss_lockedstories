using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DerestageClasses
{
	public class CryptAES
	{
        public static string encrypt(string src)
        {
            return CryptAES.EncryptRJ256(src);
        }

		public static string decrypt(string src)
		{
			return CryptAES.DecryptRJ256(src);
		}

        public static string EncryptRJ256(string prm_text_to_encrypt)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Padding = PaddingMode.Zeros;
            rijndaelManaged.Mode = CipherMode.CBC;
            rijndaelManaged.KeySize = 256;
            rijndaelManaged.BlockSize = 256;
            byte[] array = new byte[0];
            byte[] rgbIV = new byte[0];
            string s = Cryptographer.generateKeyString();
            string s2 = Certification.Udid.Replace("-", string.Empty);
            array = Encoding.UTF8.GetBytes(s);
            rgbIV = Encoding.UTF8.GetBytes(s2);
            ICryptoTransform transform = rijndaelManaged.CreateEncryptor(array, rgbIV);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
            byte[] bytes = Encoding.UTF8.GetBytes(prm_text_to_encrypt);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] array2 = memoryStream.ToArray();
            byte[] array3 = new byte[array2.Length + array.Length];
            Array.Copy(array2, 0, array3, 0, array2.Length);
            Array.Copy(array, 0, array3, array2.Length, array.Length);
            return Convert.ToBase64String(array3);
        }

		public static string DecryptRJ256(string prm_text_to_decrypt)
		{
			byte[] array = Convert.FromBase64String(prm_text_to_decrypt);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.Padding = PaddingMode.Zeros;
			rijndaelManaged.Mode = CipherMode.CBC;
			rijndaelManaged.KeySize = 256;
			rijndaelManaged.BlockSize = 256;
			byte[] array2 = new byte[32];
			byte[] rgbIV = new byte[32];
			byte[] array3 = new byte[array.Length - array2.Length];
			Array.Copy(array, 0, array3, 0, array3.Length);
			Array.Copy(array, array.Length - array2.Length, array2, 0, array2.Length);
			rgbIV = Encoding.UTF8.GetBytes(Certification.Udid.Replace("-", string.Empty));
			ICryptoTransform transform = rijndaelManaged.CreateDecryptor(array2, rgbIV);
			byte[] array4 = new byte[array3.Length];
			MemoryStream stream = new MemoryStream(array3);
			CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read);
			cryptoStream.Read(array4, 0, array4.Length);
			return Encoding.UTF8.GetString(array4).TrimEnd(new char[1]);
		}

        public static byte[] EncryptRJ256(byte[] binData)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Padding = PaddingMode.PKCS7;
            rijndaelManaged.Mode = CipherMode.CBC;
            rijndaelManaged.KeySize = 256;
            rijndaelManaged.BlockSize = 256;
            byte[] array = new byte[0];
            byte[] rgbIV = new byte[0];
            string s = Cryptographer.generateKeyString();
            string s2 = Certification.Udid.Replace("-", string.Empty);
            array = Encoding.UTF8.GetBytes(s);
            rgbIV = Encoding.UTF8.GetBytes(s2);
            ICryptoTransform transform = rijndaelManaged.CreateEncryptor(array, rgbIV);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
            byte[] bytes = BitConverter.GetBytes(binData.Length);
            byte[] array2 = new byte[4 + binData.Length];
            Array.Copy(bytes, 0, array2, 0, 4);
            Array.Copy(binData, 0, array2, 4, binData.Length);
            cryptoStream.Write(array2, 0, array2.Length);
            cryptoStream.FlushFinalBlock();
            byte[] array3 = memoryStream.ToArray();
            byte[] array4 = new byte[array.Length + array3.Length];
            Array.Copy(array3, 0, array4, 0, array3.Length);
            Array.Copy(array, 0, array4, array3.Length, array.Length);
            return array4;
        }

        public static byte[] DecryptRJ256(byte[] binData)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Padding = PaddingMode.PKCS7;
            rijndaelManaged.Mode = CipherMode.CBC;
            rijndaelManaged.KeySize = 256;
            rijndaelManaged.BlockSize = 256;
            byte[] array = new byte[32];
            byte[] rgbIV = new byte[32];
            byte[] array2 = new byte[binData.Length - array.Length];
            Array.Copy(binData, 0, array2, 0, array2.Length);
            Array.Copy(binData, binData.Length - array.Length, array, 0, array.Length);
            rgbIV = Encoding.UTF8.GetBytes(Certification.Udid.Replace("-", string.Empty));
            ICryptoTransform transform = rijndaelManaged.CreateDecryptor(array, rgbIV);
            byte[] array3 = new byte[array2.Length];
            MemoryStream stream = new MemoryStream(array2);
            CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            cryptoStream.Read(array3, 0, array3.Length);
            byte[] array4 = new byte[4];
            Array.Copy(array3, 0, array4, 0, 4);
            int num = BitConverter.ToInt32(array4, 0);
            byte[] array5 = new byte[num];
            Array.Copy(array3, 4, array5, 0, array5.Length);
            return array5;
        }
	}
}

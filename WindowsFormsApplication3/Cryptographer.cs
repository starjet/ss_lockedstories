using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace DerestageClasses
{
	public class Cryptographer
	{
		public const int FBENCRYPT_BLOCK_SIZE = 32;

		private static string encode_buf;

		private static Random cRandom = new Random();

		private static int random()
		{
			return Cryptographer.cRandom.Next(1, 9);
		}

		public static string generateIvString()
		{
			string text = string.Empty;
			for (int i = 0; i < 32; i++)
			{
				text += string.Format("{0}", Cryptographer.random());
			}
			return text;
		}

		public static string generateKeyString()
		{
			string text = string.Empty;
			for (int i = 0; i < 32; i++)
			{
				text += string.Format("{0:x}", Cryptographer.cRandom.Next(0, 65535));
			}
			string text2 = Convert.ToBase64String(Encoding.ASCII.GetBytes(text.ToString()));
			return text2.Substring(0, 32);
		}

		public static string encode(string dat)
		{
			int length = dat.Length;
			Cryptographer.encode_buf = string.Format("{0:x4}", length);
			for (int i = 0; i < dat.Length; i++)
			{
				char value = dat[i];
				Cryptographer.encode_buf += string.Format("{0,1:x}", Cryptographer.random());
				Cryptographer.encode_buf += string.Format("{0,1:x}", Cryptographer.random());
				Cryptographer.encode_buf += ((char)(Convert.ToInt32(value) + 10)).ToString();
				Cryptographer.encode_buf += string.Format("{0,1:x}", Cryptographer.random());
			}
			Cryptographer.encode_buf += Cryptographer.generateIvString();
			return Cryptographer.encode_buf;
		}

		public static string decode(string dat)
		{
			if (dat == null || dat.Length < 4)
			{
				return dat;
			}
			string s = dat.Substring(0, 4);
			int num = int.Parse(s, NumberStyles.AllowHexSpecifier);
			string text = string.Empty;
			int num2 = 2;
			string text2 = dat.Substring(4, dat.Length - 4);
			for (int i = 0; i < text2.Length; i++)
			{
				char value = text2[i];
				if (num2 % 4 == 0)
				{
					text += ((char)(Convert.ToInt32(value) - 10)).ToString();
				}
				num2++;
				if (text.Length >= num)
				{
					break;
				}
			}
			return text;
		}

		public static string ComputeHash(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				return null;
			}
			SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			byte[] bytes = Encoding.UTF8.GetBytes(data);
			byte[] array = sHA1CryptoServiceProvider.ComputeHash(bytes);
			string text = string.Empty;
			byte[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				byte b = array2[i];
				text += string.Format("{0:x2}", b);
			}
			return text;
		}

		public static string MakeMd5(string input)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.UTF8.GetBytes(input + "r!I@nt8e5i=");
			byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
			string text = string.Empty;
			byte[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				byte b = array2[i];
				text += b.ToString("x2");
			}
			return text;
		}
	}
}

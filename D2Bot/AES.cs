using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace D2Bot;

public class AES
{
	private static readonly int iterations = 1000;

	public static string Encrypt(string input, string password)
	{
		byte[] salt = GetSalt();
		byte[] key = CreateKey(password, salt);
		byte[] iV;
		byte[] array;
		using (Aes aes = Aes.Create())
		{
			aes.Key = key;
			aes.Padding = PaddingMode.PKCS7;
			aes.Mode = CipherMode.CBC;
			aes.GenerateIV();
			iV = aes.IV;
			ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
			using MemoryStream memoryStream = new MemoryStream();
			using CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				streamWriter.Write(input);
			}
			array = memoryStream.ToArray();
		}
		byte[] array2 = new byte[salt.Length + iV.Length + array.Length];
		Array.Copy(salt, 0, array2, 0, salt.Length);
		Array.Copy(iV, 0, array2, salt.Length, iV.Length);
		Array.Copy(array, 0, array2, salt.Length + iV.Length, array.Length);
		return Convert.ToBase64String(array2.ToArray());
	}

	public static string Decrypt(string input, string password)
	{
		string result = null;
		try
		{
			byte[] array = Convert.FromBase64String(input);
			byte[] array2 = new byte[32];
			byte[] array3 = new byte[16];
			byte[] array4 = new byte[array.Length - array2.Length - array3.Length];
			Array.Copy(array, 0, array2, 0, array2.Length);
			Array.Copy(array, array2.Length, array3, 0, array3.Length);
			Array.Copy(array, array2.Length + array3.Length, array4, 0, array4.Length);
			byte[] key = CreateKey(password, array2);
			using (Aes aes = Aes.Create())
			{
				aes.Key = key;
				aes.IV = array3;
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
				using MemoryStream stream = new MemoryStream(array4);
				using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
				using StreamReader streamReader = new StreamReader(stream2);
				result = streamReader.ReadToEnd();
			}
			return result;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public static byte[] CreateKey(string password, byte[] salt)
	{
		using Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, iterations);
		return rfc2898DeriveBytes.GetBytes(32);
	}

	private static byte[] GetSalt()
	{
		byte[] array = new byte[32];
		using RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
		rNGCryptoServiceProvider.GetNonZeroBytes(array);
		return array;
	}

	public static string GetUniqueKey(int maxSize)
	{
		char[] array = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
		byte[] array2 = new byte[1];
		using (RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
		{
			rNGCryptoServiceProvider.GetNonZeroBytes(array2);
			array2 = new byte[maxSize];
			rNGCryptoServiceProvider.GetNonZeroBytes(array2);
		}
		StringBuilder stringBuilder = new StringBuilder(maxSize);
		byte[] array3 = array2;
		foreach (byte b in array3)
		{
			stringBuilder.Append(array[b % array.Length]);
		}
		return stringBuilder.ToString();
	}
}

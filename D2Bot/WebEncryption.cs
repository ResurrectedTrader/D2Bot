using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using SslCertBinding.Net;

namespace D2Bot;

public class WebEncryption
{
	public const string DEFAULT_CA = "d2botsharp CA";

	public const string DEFAULT_SUBJECT = "d2botsharp";

	public X509Certificate2 certificate;

	public ICertificateBindingConfiguration binding;

	public static string AssemblyGuid
	{
		get
		{
			object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(GuidAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return string.Empty;
			}
			return ((GuidAttribute)customAttributes[0]).Value.ToUpper();
		}
	}

	public static X509Certificate2 CreateSelfSignedCertificate(string subjectName, string issuerName, AsymmetricKeyParameter issuerPrivKey, string ip, string host)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Expected O, but got Unknown
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		SecureRandom val = new SecureRandom((IRandomGenerator)new CryptoApiRandomGenerator());
		ISignatureFactory val2 = (ISignatureFactory)new Asn1SignatureFactory("SHA512WITHRSA", issuerPrivKey, val);
		X509V3CertificateGenerator val3 = new X509V3CertificateGenerator();
		val3.AddExtension(X509Extensions.ExtendedKeyUsage.Id, true, (Asn1Encodable)new ExtendedKeyUsage((KeyPurposeID[])(object)new KeyPurposeID[1] { KeyPurposeID.IdKPServerAuth }));
		BigInteger serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(long.MaxValue), val);
		val3.SetSerialNumber(serialNumber);
		X509Name subjectDN = new X509Name("CN=" + subjectName);
		X509Name issuerDN = new X509Name("CN=" + issuerName);
		val3.SetIssuerDN(issuerDN);
		val3.SetSubjectDN(subjectDN);
		DateTime date = DateTime.UtcNow.Date;
		DateTime notAfter = date.AddYears(2);
		val3.SetNotBefore(date);
		val3.SetNotAfter(notAfter);
		KeyGenerationParameters val4 = new KeyGenerationParameters(val, 2048);
		RsaKeyPairGenerator val5 = new RsaKeyPairGenerator();
		val5.Init(val4);
		AsymmetricCipherKeyPair val6 = val5.GenerateKeyPair();
		val3.SetPublicKey(val6.Public);
		GeneralName[] array = (GeneralName[])(object)new GeneralName[2]
		{
			new GeneralName(7, ip),
			new GeneralName(2, host)
		};
		val3.AddExtension(X509Extensions.SubjectAlternativeName, false, (Asn1Encodable)new GeneralNames(array));
		Org.BouncyCastle.X509.X509Certificate obj = val3.Generate(val2);
		AsymmetricAlgorithm privateKey = ToDotNetKey((RsaPrivateCrtKeyParameters)val6.Private);
		return new X509Certificate2(DotNetUtilities.ToX509Certificate(obj))
		{
			PrivateKey = privateKey,
			FriendlyName = subjectName
		};
	}

	public static X509Certificate2 CreateCACertificate(string subjectName, ref AsymmetricKeyParameter CaPrivateKey)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Expected O, but got Unknown
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		SecureRandom val = new SecureRandom((IRandomGenerator)new CryptoApiRandomGenerator());
		X509V3CertificateGenerator val2 = new X509V3CertificateGenerator();
		BigInteger serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(long.MaxValue), val);
		val2.SetSerialNumber(serialNumber);
		X509Name val3 = new X509Name("CN=" + subjectName);
		X509Name issuerDN = val3;
		val2.SetIssuerDN(issuerDN);
		val2.SetSubjectDN(val3);
		DateTime date = DateTime.UtcNow.Date;
		DateTime notAfter = date.AddYears(2);
		val2.SetNotBefore(date);
		val2.SetNotAfter(notAfter);
		KeyGenerationParameters val4 = new KeyGenerationParameters(val, 2048);
		RsaKeyPairGenerator val5 = new RsaKeyPairGenerator();
		val5.Init(val4);
		AsymmetricCipherKeyPair val6 = val5.GenerateKeyPair();
		val2.SetPublicKey(val6.Public);
		AsymmetricCipherKeyPair val7 = val6;
		ISignatureFactory val8 = (ISignatureFactory)new Asn1SignatureFactory("SHA512WITHRSA", val7.Private, val);
		X509Certificate2 result = new X509Certificate2(val2.Generate(val8).GetEncoded())
		{
			FriendlyName = subjectName
		};
		CaPrivateKey = val7.Private;
		return result;
	}

	public static AsymmetricAlgorithm ToDotNetKey(RsaPrivateCrtKeyParameters privateKey)
	{
		RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
		{
			KeyContainerName = Guid.NewGuid().ToString(),
			KeyNumber = 1,
			Flags = CspProviderFlags.UseMachineKeyStore
		});
		RSAParameters parameters = new RSAParameters
		{
			Modulus = ((RsaKeyParameters)privateKey).Modulus.ToByteArrayUnsigned(),
			P = privateKey.P.ToByteArrayUnsigned(),
			Q = privateKey.Q.ToByteArrayUnsigned(),
			DP = privateKey.DP.ToByteArrayUnsigned(),
			DQ = privateKey.DQ.ToByteArrayUnsigned(),
			InverseQ = privateKey.QInv.ToByteArrayUnsigned(),
			D = ((RsaKeyParameters)privateKey).Exponent.ToByteArrayUnsigned(),
			Exponent = privateKey.PublicExponent.ToByteArrayUnsigned()
		};
		rSACryptoServiceProvider.ImportParameters(parameters);
		return rSACryptoServiceProvider;
	}

	public bool removeStoreByName(string subjectName, StoreName st = StoreName.My, StoreLocation sl = StoreLocation.LocalMachine)
	{
		X509Store x509Store = new X509Store(st, sl);
		try
		{
			x509Store.Open(OpenFlags.ReadWrite);
			X509Certificate2Collection certificates = x509Store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, validOnly: false);
			x509Store.RemoveRange(certificates);
		}
		finally
		{
			x509Store.Close();
		}
		return true;
	}

	public bool readStoreByHash(string hash, StoreName st = StoreName.My, StoreLocation sl = StoreLocation.LocalMachine)
	{
		hash = Regex.Replace(hash, "[^\\da-fA-F]", string.Empty).ToUpper();
		X509Store x509Store = new X509Store(st, sl);
		try
		{
			x509Store.Open(OpenFlags.ReadOnly);
			X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, hash, validOnly: false);
			if (x509Certificate2Collection.Count == 0)
			{
				throw new FileNotFoundException($"Cert with thumbprint: '{hash}' not found in local machine cert store.");
			}
			certificate = x509Certificate2Collection[0];
		}
		finally
		{
			x509Store.Close();
		}
		return true;
	}

	public static bool addCertToStore(X509Certificate2 cert, StoreName st = StoreName.My, StoreLocation sl = StoreLocation.LocalMachine)
	{
		X509Store x509Store = new X509Store(st, sl);
		try
		{
			x509Store.Open(OpenFlags.ReadWrite);
			x509Store.Add(cert);
		}
		finally
		{
			x509Store.Close();
		}
		return true;
	}

	public bool fetchSelfSignedCertificate(StoreName st = StoreName.My, StoreLocation sl = StoreLocation.LocalMachine)
	{
		bool result = false;
		X509Store x509Store = new X509Store(st, sl);
		try
		{
			x509Store.Open(OpenFlags.ReadOnly);
			_ = x509Store.Certificates;
			certificate = x509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault((X509Certificate2 cert) => cert.SubjectName.Name.Contains("d2botsharp"));
			if (certificate != null && certificate.NotAfter > DateTime.UtcNow.Date.AddMonths(6))
			{
				result = true;
			}
			else
			{
				certificate = null;
			}
		}
		finally
		{
			x509Store.Close();
		}
		return result;
	}

	public bool createSelfSignedCertificate(string subjectName, string ip, string host)
	{
		AsymmetricKeyParameter CaPrivateKey = null;
		removeStoreByName("d2botsharp CA", StoreName.Root);
		addCertToStore(CreateCACertificate("d2botsharp CA", ref CaPrivateKey), StoreName.Root);
		removeStoreByName(subjectName);
		certificate = CreateSelfSignedCertificate(subjectName, "d2botsharp CA", CaPrivateKey, ip, host);
		addCertToStore(certificate);
		return true;
	}

	public bool bindCertificateToEndpoint(IPEndPoint address, StoreName st = StoreName.My)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		binding = (ICertificateBindingConfiguration)new CertificateBindingConfiguration();
		Guid guid = Guid.Parse(AssemblyGuid);
		CertificateBinding val = null;
		try
		{
			val = binding.Query(address)[0];
		}
		catch
		{
		}
		if (val != null && val.AppId != guid)
		{
			throw new Exception("The selected endpoint is already in use!");
		}
		removeActiveBindingFromEndpoint(address);
		val = new CertificateBinding(certificate.Thumbprint, st, address, guid, (BindingOptions)null);
		val.Options.DoNotVerifyCertificateRevocation = true;
		val.Options.UseDsMappers = true;
		binding.Bind(val);
		return true;
	}

	public bool removeActiveBindingFromEndpoint(IPEndPoint address)
	{
		try
		{
			binding.Delete(address);
		}
		catch
		{
		}
		return true;
	}
}

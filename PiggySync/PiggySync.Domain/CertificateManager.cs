using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace PiggySync.Domain
{
    public class CertificateManager
    {
        private const string serverCertFileName="serverCert.pfx";
        
        public static X509Certificate2 GenerateCertificate(string certName)
        {
            var keypairgen = new RsaKeyPairGenerator();
            keypairgen.Init(new KeyGenerationParameters(new SecureRandom(new CryptoApiRandomGenerator()), 1024));

            var keypair = keypairgen.GenerateKeyPair();

            var gen = new X509V3CertificateGenerator();

            var CN = new X509Name("CN=" + certName);
            var SN = BigInteger.ProbablePrime(120, new Random());

            gen.SetSerialNumber(SN);
            gen.SetSubjectDN(CN);
            gen.SetIssuerDN(CN);
            gen.SetNotAfter(DateTime.MaxValue);
            gen.SetNotBefore(DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0)));
            gen.SetSignatureAlgorithm("MD5WithRSA");
            gen.SetPublicKey(keypair.Public);

            var newCert = gen.Generate(keypair.Private);
            return new X509Certificate2(DotNetUtilities.ToX509Certificate(newCert));
        }

        public static X509Certificate2 ServerCert
        {
            get;
            set;
        }

        public static X509Certificate2 ClientCert
        {
            get;
            set;
        }

        public static void Initialize()
        {
            if (!File.Exists(serverCertFileName))
            {
                ServerCert = GenerateCertificate("server");

				var certData = ServerCert.Export(X509ContentType.Cert);
                File.WriteAllBytes(serverCertFileName, certData);
            }
            else
            {
                try
                {
                    var certData = File.ReadAllBytes(serverCertFileName);
                    ServerCert = new X509Certificate2();
                    ServerCert.Import(certData);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + e);
                }
            }
        }
    }
}

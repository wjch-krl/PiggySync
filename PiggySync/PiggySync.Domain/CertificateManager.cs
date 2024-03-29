﻿using System;
using System.Diagnostics;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using PiggySync.Domain.Concrete;
using PiggySync.Common;

namespace PiggySync.Domain
{
    public class CertificateManager
    {
        private const string serverCertFileName = "serverCert.pfx";

        static CertificateManager()
        {
			if (TypeResolver.DirectoryHelper.FileExists (serverCertFileName))
            {
                ServerCert = GenerateCertificate("server");
                var certData = ServerCert.GetEncoded();
				using (var fileWriter = new BinaryWriter(TypeResolver.DirectoryHelper.OperFileWrite(serverCertFileName)))
                {
                    fileWriter.Write(certData);
                }
            }
            else
            {
                try
                {
					using (var filestream = TypeResolver.DirectoryHelper.OpenFileRead (serverCertFileName))
                    {
                        var parser = new X509CertificateParser();
                        ServerCert = parser.ReadCertificate(filestream);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Exception: " + e);
                }
            }
        }

        public static X509Certificate ServerCert { get; set; }

        public static X509Certificate ClientCert { get; set; }

        public static X509Certificate GenerateCertificate(string certName)
        {
            var keypairgen = new RsaKeyPairGenerator();
            keypairgen.Init(new KeyGenerationParameters(new SecureRandom(new VmpcRandomGenerator()), 1024));

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
            return newCert;
        }
    }
}
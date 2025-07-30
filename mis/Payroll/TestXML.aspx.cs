using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

//using System.Security.Cryptography.Xml.SignedXml;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

public partial class mis_Payroll_TestXML : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnkbtngenerate_Click(object sender, EventArgs e)
    {
        CspParameters cspParams = new CspParameters()
        {
            KeyContainerName = "XML_DSIG_RSA_KEY"
        };

        // Create a new RSA signing key and save it in the container.
        // System.Security.Cryptography.RSACryptoServiceProvider.UseMachineKeyStore = true;
        // var provider = new System.Security.Cryptography.RSACryptoServiceProvider();
        RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);

        // Create a new XML document.
        XmlDocument xmlDoc = new XmlDocument()
        {
            // Load an XML file into the XmlDocument object.
            PreserveWhitespace = true
        };

        string paths = Server.MapPath("../Payroll/UploadSalaryXML/MPMFF_000001_1000000001_170123_Test.xml");
        string paths1 = Server.MapPath("../Payroll/UploadSalaryXML/MPMFF_000001_1000000001_170123_Test1.xml");
        //ViewState["vs"] = paths.ToString();
        xmlDoc.Load(paths);


        // Open the X.509 "Current User" store in read only mode.
        X509Store store = new X509Store(StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly);

        // Place all certificates in an X509Certificate2Collection object.
        X509Certificate2Collection certCollection = store.Certificates;


        X509Certificate2 cert = null;


        // Loop through each certificate and find the certificate
        // with the appropriate name.
        foreach (X509Certificate2 c in certCollection)
        {

            if (c.Subject != "CN=localhost")
            {
                cert = c;
                break;
            }
        }

        if (cert == null)
        {
            // throw new CryptographicException("The X.509 certificate could not be found.");
           
        }
        else
        {
            SignXml(xmlDoc, rsaKey, cert);

            Console.WriteLine("XML file signed.");
            // Save the document.
            xmlDoc.Save(paths1);
        }
    }

    public static void SignXml(XmlDocument xmlDoc, RSA rsaKey, X509Certificate2 Cert)
    {
        // Check arguments.
        //if (xmlDoc == null)
        //     throw new ArgumentException(null, nameof(xmlDoc));
        //if (rsaKey == null)
        //    throw new ArgumentException(null, nameof(rsaKey));

        // Create a SignedXml object.
        System.Security.Cryptography.Xml.SignedXml signedXml = new System.Security.Cryptography.Xml.SignedXml(xmlDoc)
        {

            // Add the key to the SignedXml document.
            SigningKey = rsaKey
        };

        // Create a reference to be signed.
        Reference reference = new Reference()
        {
            Uri = ""
        };

        Signature XMLSignature = signedXml.Signature;


        // Add an enveloped transformation to the reference.
        XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
        reference.AddTransform(env);

        // Add the reference to the SignedXml object.
        signedXml.AddReference(reference);

        // Add an RSAKeyValue KeyInfo (optional; helps recipient find key to validate).
        KeyInfo keyInfo = new KeyInfo();
        keyInfo.AddClause(new KeyInfoX509Data(Cert));
        keyInfo.AddClause(new RSAKeyValue((RSA)rsaKey));


        // Add the KeyInfo object to the Reference object.
        XMLSignature.KeyInfo = keyInfo;

        // Compute the signature.
        signedXml.ComputeSignature();

        // Get the XML representation of the signature and save
        // it to an XmlElement object.
        XmlElement xmlDigitalSignature = signedXml.GetXml();

        // Append the element to the XML document.
        xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
    }
}
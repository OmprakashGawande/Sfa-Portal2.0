using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class mis_SetSHA512pwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          //      string MPCDF = SHA512_HASH("MPCDF@1234");  // PWD CHANGED
           //   string BDS = SHA512_HASH("BDS@1234");     // PWD CHANGED
            //  string GDS = SHA512_HASH("GDS@1234"); // PWD CHANGED
            // string IDS = SHA512_HASH("IDS@1234");  // PWD CHANGED
            //  string JDS = SHA512_HASH("JDS@1234"); // PWD CHANGED
            //string UDS = SHA512_HASH("UDS@1234"); // PWD CHANGED
          //  string BKDS = SHA512_HASH("BKDS@1234"); // PWD CHANGED

            string APEX = SHA512_HASH("mfp@123"); // E0001
        }

       

    }

    public string SHA512_HASH(string rawData)
    {
        //Create a SHA512   
        using (SHA512 sha512Hash = SHA512.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

//using System.Security.Cryptography.Xml.SignedXml;
using System.IO;
using System.Security.Cryptography.X509Certificates;

public partial class mis_Payroll_PayrollGenerateXML : System.Web.UI.Page
{
    DataSet ds1, ds2, ds3 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    static DataTable dt = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
                DivDetail.Visible = false;
                FillYear();
                FillOffice();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void FillYear()
    {
        try
        {
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            ds1 = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlYear.DataSource = ds1.Tables[0];
                        ddlYear.DataTextField = "Year";
                        ddlYear.DataValueField = "Year";
                        ddlYear.DataBind();
                        ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void FillOffice()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ds2 = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds2 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {

                        ddlOfficeName.DataSource = ds2.Tables[0];
                        ddlOfficeName.DataTextField = "Office_Name";
                        ddlOfficeName.DataValueField = "Office_ID";
                        ddlOfficeName.DataBind();
                        ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
            }
            ddlOfficeName.SelectedValue = objdb.Office_ID();
            if (ddlOfficeName.SelectedValue.ToString() != "1")
            {
                ddlOfficeName.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            DivDetail.Visible = false;
            lblrowcount.Text = "";
            string Office = "0";
            lblTab.Text = "";
            if (Convert.ToString(objdb.Office_ID()) != "1")
            {
                Office = Convert.ToString(objdb.Office_ID());
                ddlOfficeName.SelectedValue = objdb.Office_ID();
            }
            else
            {
                Office = ddlOfficeName.SelectedValue.ToString();
            }

            ds3 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Year", "MonthNo", "Office_ID", "SalaryType" }, new string[] { "24", ddlYear.SelectedValue, ddlMonth.SelectedValue, Office, rbnlist.SelectedValue }, "dataset");
            if (ds3 != null)
            {
                if (ds3.Tables.Count > 0)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds3.Tables[0];
                        GridView1.DataBind();
                        lblrowcount.Text = "Employee Count : " + (ds3.Tables[0].Rows.Count);
                        DivDetail.Visible = true;
                        int j = 0, K = 0;
                        for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                        {
                            if (ds3.Tables[0].Rows[i]["SalaryFinalStatus"].ToString() != "1")
                            {
                                j++;
                            }
                            if (ds3.Tables[0].Rows[i]["SalaryFinalStatus"].ToString() == "1" && ds3.Tables[0].Rows[i]["Xml_Status"].ToString() != "1")
                            {
                                K++;
                            }
                        }
                        if (j > 0 && K == 0)
                        {
                            btnFinal.Visible = true;
                            btnDownload.Visible = false;

                        }
                        else if (j == 0 && K > 0)
                        {
                            btnFinal.Visible = false;
                            btnDownload.Visible = true;
                        }
                        else if (j > 0 && K > 0)
                        {
                            btnFinal.Visible = true;
                            btnDownload.Visible = true;
                        }
                        else if (j == 0 && K == 0)
                        {
                            btnFinal.Visible = false;
                            btnDownload.Visible = false;
                        }
                    }
                }
            }
            else
            {
                lblMsg.Text = "Salary Details Not Found For This Month.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Page.Validate("a");

            if (!Page.IsValid)
            {
                return;
            }
            else
            {
                try
                {
                    lblMsg.Text = "";
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    DivDetail.Visible = false;
                    if (ddlYear.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
                    {
                        FillGrid();
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 5 : ", ex.Message.ToString());
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



    protected void btnFinal_Click(object sender, EventArgs e)
    {
        try
        {
            // string flag = "0";
            lblMsg.Text = "";
            string msg = "";
            if (ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Office Name.\\n";
            }
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year.\\n";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month.\\n";
            }
            if (msg == "")
            {
                //string Year = ddlYear.SelectedValue.ToString();
                //string MonthNo = ddlMonth.SelectedValue.ToString();
                //string LoginUserID = ViewState["Emp_ID"].ToString();
                //string Office_ID = ddlOfficeName.SelectedValue.ToString();
                string Salary_IDS = "";
                foreach (GridViewRow gr in GridView1.Rows)
                {

                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    HiddenField gvhfsalaryid = (HiddenField)gr.FindControl("gvhfsalaryid");
                    Label lblSalaryFinalStatus = (Label)gr.FindControl("lblSalaryFinalStatus");
                    if (chkSelect.Checked == true && lblSalaryFinalStatus.Text == "Not Finaly Genarated")
                    {
                        if (Salary_IDS.ToString() == "")
                        {
                            Salary_IDS = gvhfsalaryid.Value;
                        }
                        else
                        {
                            Salary_IDS = Salary_IDS.ToString() + "," + gvhfsalaryid.Value;
                        }
                    }

                }
                if (Salary_IDS.ToString() != "")
                {


                    ds1 = objdb.ByProcedure("SpPayrollSalaryDetail",
                      new string[] { "flag", "Salary_IDs", "Salary_UpdatedBy", "Salary_UpdatedByIP" },
                      new string[] { "26", Salary_IDS.ToString(), Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");

                    // flag = "1";

                }
                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                    {

                        FillGrid();

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Salary Finaly Genarated");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + ds1.Tables[0].Rows[0]["Msg"].ToString() + "');", true);
                    }
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {

            //string Year = ddlYear.SelectedValue.ToString();
            //string MonthNo = ddlMonth.SelectedValue.ToString();
            //string LoginUserID = ViewState["Emp_ID"].ToString();
            //string Office_ID = ddlOfficeName.SelectedValue.ToString();
            lblMsg.Text = "";
            string Salary_IDS = "";
            foreach (GridViewRow gr in GridView1.Rows)
            {

                CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                HiddenField gvhfsalaryid = (HiddenField)gr.FindControl("gvhfsalaryid");
                Label lblSalaryFinalStatus = (Label)gr.FindControl("lblSalaryFinalStatus");
                Label lblXml_Status = (Label)gr.FindControl("lblXml_Status");
                if (chkSelect.Checked == true && lblSalaryFinalStatus.Text == "Finaly Genarated" && lblXml_Status.Text == "XML Not Genarated")
                {
                    if (Salary_IDS.ToString() == "")
                    {
                        Salary_IDS = gvhfsalaryid.Value;
                    }
                    else
                    {
                        Salary_IDS = Salary_IDS.ToString() + "," + gvhfsalaryid.Value;
                    }
                }

            }
            if (Salary_IDS.ToString() != "")
            {


                createXML(Salary_IDS.ToString());
                //FillGrid();

                // flag = "1";

            }



        }
        catch (Exception ex)
        {
            //string msg = ex.Message.ToString() + ViewState["ms"].ToString();
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry 2!", ex.Message.ToString());
        }
    }
    public void createXML(string Salary_IDS)
    {
       // ViewState["vs"] = "";
        try
        {
            
            lblMsg.Text = "";
            dt.Clear();
            string strfilename = "";
            DataSet dsxmldata = new DataSet();
            dsxmldata = objdb.ByProcedure("SpPayrollSalaryDetailXMLData",
                        new string[] { "Salary_IDS", "Office_ID" },
                        new string[] { Salary_IDS.ToString(), Session["Office_ID"].ToString() }, "dataset");
            if (dsxmldata.Tables.Count > 1 && dsxmldata.Tables[0].Rows.Count > 0 && dsxmldata.Tables[1].Rows.Count > 0)
            {
                strfilename = dsxmldata.Tables[1].Rows[0]["entity"].ToString() + "_123456_" + dsxmldata.Tables[1].Rows[0]["Mainreference_no"].ToString() + "_" + System.DateTime.Now.ToString("ddMMyy");
                XmlTextWriter writer = new XmlTextWriter(Server.MapPath("../Payroll/UploadSalaryXML/" + strfilename + ".xml"), System.Text.Encoding.UTF8);

                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                int reference_no1 = int.Parse(dsxmldata.Tables[1].Rows[0]["reference_no"].ToString());

                string Office_ID = Session["Office_ID"].ToString();
                string reference_no = "";
                writer.WriteStartElement("FTO");
                for (int j = 0; j < dsxmldata.Tables[0].Rows.Count; j++)
                {
                    // start code for reference_no
                    if ((reference_no1.ToString().Length) == 1)
                    {

                        reference_no = Office_ID + "0000000000" + reference_no1.ToString();
                    }
                    else if ((reference_no1.ToString().Length) == 2)
                    {

                        reference_no = Office_ID + "000000000" + reference_no1.ToString();
                    }
                    else if ((reference_no1.ToString().Length) == 3)
                    {

                        reference_no = Office_ID + "00000000" + reference_no1.ToString();
                    }
                    else if ((reference_no1.ToString().Length) == 4)
                    {

                        reference_no = Office_ID + "0000000" + reference_no1.ToString();
                    }
                    else if ((reference_no1.ToString().Length) == 5)
                    {

                        reference_no = Office_ID + "000000" + reference_no1.ToString();
                    }
                    else if ((reference_no1.ToString().Length) == 6)
                    {

                        reference_no = Office_ID + "00000" + reference_no1.ToString();
                    }
                    else if ((reference_no1.ToString().Length) == 7)
                    {

                        reference_no = Office_ID + "0000" + reference_no1.ToString();
                    }
                    else if ((reference_no1.ToString().Length) == 8)
                    {

                        reference_no = Office_ID + "000" + reference_no1.ToString();
                    }
                    else if ((reference_no1.ToString().Length) == 9)
                    {

                        reference_no = Office_ID + "00" + reference_no1.ToString();
                    }
                    else if ((reference_no1.ToString().Length) == 10)
                    {

                        reference_no = Office_ID + "0" + reference_no1.ToString();
                    }
                    else if ((reference_no1.ToString().Length) > 10)
                    {

                        reference_no = Office_ID + reference_no1.ToString();
                    }
                    //End code for reference_no

                    writer.WriteStartElement("account");

                    writer.WriteStartElement("entity");
                    writer.WriteString(dsxmldata.Tables[1].Rows[0]["entity"].ToString());
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("account_debit");
                    writer.WriteString(dsxmldata.Tables[1].Rows[0]["account_debit"].ToString());
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("ifsc_code_debit");
                    writer.WriteString(dsxmldata.Tables[1].Rows[0]["ifsc_code_debit"].ToString());
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("trans_type");
                    writer.WriteString("ACH");
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("tran_date");
                    writer.WriteString(dsxmldata.Tables[0].Rows[0]["tran_date"].ToString());
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("reference_no");
                    writer.WriteString(reference_no.ToString());
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("bank_name");
                    writer.WriteString("PUNB");
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("branch_code");
                    writer.WriteString("");
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("branch_name");
                    writer.WriteString("");
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("ifsc_code_credit");
                    writer.WriteString(dsxmldata.Tables[0].Rows[j]["ifsc_code_credit"].ToString());
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("account_credit");
                    writer.WriteString(dsxmldata.Tables[0].Rows[j]["ifsc_code_credit"].ToString());
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("name");
                    writer.WriteString(dsxmldata.Tables[0].Rows[j]["Emp_Name"].ToString());
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("address");
                    writer.WriteString("");
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("currency");
                    writer.WriteString("INR");
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("d_c");
                    writer.WriteString("D");
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("narration");
                    writer.WriteString("Payment for Salary");
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("amount");
                    writer.WriteString(dsxmldata.Tables[0].Rows[j]["Salary_NetSalary"].ToString());
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("bo_agency_cod");
                    writer.WriteString("");
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("district_id");
                    writer.WriteString(dsxmldata.Tables[0].Rows[j]["district_id"].ToString());
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("proposal_id");
                    writer.WriteString("1");
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("payment_type");
                    writer.WriteString("");
                    writer.WriteFullEndElement();

                    //Book Price
                    writer.WriteStartElement("AID");
                    writer.WriteString("");
                    writer.WriteFullEndElement();



                    //End Author2Address
                    writer.WriteEndElement();
                    reference_no1++;

                    if (dt.Columns.Count == 0)
                    {
                        //dt.Columns.Add("SNo");

                        dt.Columns.Add("Salary_ID", typeof(int));
                        dt.Columns.Add("Emp_ID", typeof(int));
                        dt.Columns.Add("reference_no", typeof(string));
                        dt.Columns.Add("Salary_NetSalary", typeof(Decimal));
                        dt.Columns.Add("tran_date", typeof(DateTime));
                        dt.Columns.Add("ifsc_code_credit", typeof(string));
                        dt.Columns.Add("account_credit", typeof(string));

                    }
                    //string tran_date = Convert.ToDateTime(dsxmldata.Tables[0].Rows[j]["tran_date1"].ToString().Trim(), cult).ToString("yyyy/MM/dd");
                    DateTime ToDate = Convert.ToDateTime(dsxmldata.Tables[0].Rows[j]["tran_date1"].ToString(), culture);
                    string tran_date = ToDate.ToString("yyyy/MM/dd");


                    dt.Rows.Add(dsxmldata.Tables[0].Rows[j]["Salary_ID"].ToString(), dsxmldata.Tables[0].Rows[j]["Emp_ID"].ToString(), reference_no.ToString()
                        , dsxmldata.Tables[0].Rows[j]["Salary_NetSalary"].ToString(), tran_date.ToString()
                        , dsxmldata.Tables[0].Rows[j]["ifsc_code_credit"].ToString(), dsxmldata.Tables[0].Rows[j]["account_credit"].ToString()
                        );
                    ViewState["dt"] = dt;


                }
                //End Author2
                writer.WriteEndElement();
                //createNode(writer);


                writer.Close();


                //start digital sinature
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

                string paths = Server.MapPath("../Payroll/UploadSalaryXML/" + strfilename + ".xml");
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
                    Console.WriteLine("The X.509 certificate could not be found");
                }
                else
                {
                    SignXml(xmlDoc, rsaKey, cert);

                    Console.WriteLine("XML file signed.");
                    // Save the document.
                    xmlDoc.Save(paths);
                }
                //  xmlDoc.Load(path);
                // Sign the XML document.
                //end digital sinature
                //string XML_File_Path = "../UploadSalaryXML/" + strfilename + ".xml";
                string XML_File_Path = strfilename + ".xml";
                DataSet ds = objdb.ByProcedure("SpPayrollSalaryDetailXMLData_Insert",
                                             new string[] { "Office_ID", "Mainreference_no", "bank_name", "ifsc_code_debit", "account_debit", "Xml_CreatedBy", "Xml_CreatedByIP", "XML_File_Path", "Salary_Year", "Salary_MonthNo" },
                                    new string[] { Office_ID.ToString(), dsxmldata.Tables[1].Rows[0]["Mainreference_no"].ToString(),"PUNB", dsxmldata.Tables[1].Rows[0]["ifsc_code_debit"].ToString(),
                               dsxmldata.Tables[1].Rows[0]["account_debit"].ToString(), Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress(), XML_File_Path.ToString(),ddlYear.SelectedValue,ddlMonth.SelectedValue },
                                     new string[] { "Type_PayrollSalaryDetailXML_Child" },
                                                        new DataTable[] { dt },
                                               "dataset");
                //}

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                {
                    FillGrid();
                    string filePath = strfilename + ".xml";
                    // Response.ContentType = ".xml";
                    // Response.ContentType = ".xml";
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                    Response.TransmitFile(Server.MapPath("../Payroll/UploadSalaryXML/" + filePath));
                    // Response.TransmitFile(filePath);
                    //Response.WriteFile(filePath);
                    Response.End();

                    lblMsg.Text = "XML File Generated Successfully!";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "XML File Generated Successfully!");
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Sorry 1! ", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-check", "alert-info", "alert!", "Please Select Atlease 1 Record!");
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert( Please Select Atlease 1 Record);", true);
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry 3!", ex.Message.ToString());
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

    public IFormatProvider culture { get; set; }
    public IFormatProvider cult { get; set; }
}
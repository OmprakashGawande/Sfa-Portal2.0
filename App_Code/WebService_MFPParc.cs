using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Text;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    string securityKey = "SFA_MFPPARK";
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    StringBuilder sb = new StringBuilder();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    // For PCS & phad Login
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void PCS_OR_Phad_Login(string Key, string UserName, string Password)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (UserName.ToString() != "" && Password.ToString() != "")
                {
                    switch (UserName.Substring(0, 1))
                    {
                        case "E": //for Employees Login offices like HO,CCF,DFO 


                        case "P": //for or pcs
                            ds = obj.ByProcedure("USP_login_RO_PCS_Phad",
                                    new string[] { "flag", "UserName" },
                                    new string[] { "1", UserName.ToString() }, "dataset");
                            break;

                        case "F": //for or Phad Officer
                            ds = obj.ByProcedure("USP_login_RO_PCS_Phad",
                                    new string[] { "flag", "UserName" },
                                    new string[] { "3", UserName.ToString() }, "dataset");
                            break;
                        default:
                            if (ds != null)
                            {
                                ds.Clear();
                            }
                            break;
                    }

                    if (ds.Tables[0].Rows.Count > 0 && ds != null)
                    {
                        StringBuilder randomText = new StringBuilder();
                        string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz!@#$%&*~";
                        Random r = new Random();
                        for (int j = 0; j <= 10; j++)
                        { randomText.Append(alphabets[r.Next(alphabets.Length)]); }
                        string RandomText = obj.Encrypt(randomText.ToString());
                        string Passwordnew = obj.SHA512_HASH(String.Concat(obj.SHA512_HASH(Password.ToString()), RandomText.ToString()));

                        if (CompaireHashCode(ds.Tables[0].Rows[0]["Password"].ToString(), Passwordnew.ToString(), RandomText.ToString()))
                        {
                            dt = ds.Tables[0];
                            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                            Dictionary<string, object> row = null;
                            foreach (DataRow rs in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {
                                    row.Add(col.ColumnName, rs[col]);
                                }
                                rows.Add(row);
                            }

                            this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                        }
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials" }));
                        }
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials" }));
                    }

                }
                else
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials" }));
                }

                
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }
    private bool CompaireHashCode(string DataBasePassword, string ClientPasswordWithHashing, string RandomText1)
    {
        bool i;
        if (obj.SHA512_HASH(String.Concat(DataBasePassword, RandomText1.ToString())).Equals(ClientPasswordWithHashing))
        { i = true; }
        else { i = false; }
        return i;
    }

    //Change Password
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ChangePassword(string Key, string UserName, string NewPassword, string OldPassword)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("sp_Login",
                         new string[] { "flag", "UserName" },
                         new string[] { "0", UserName.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Credentials." }));
                }
                else
                {
                    string saltkey = obj.GenerateSaltKey();
                    if (obj.CompaireHashCode(ds1.Tables[0].Rows[0]["Password"].ToString(), OldPassword, saltkey))
                    {
                        //Update New Password by UserName prefix
                        ds = obj.ByProcedure("sp_Login",
                                 new string[] { "flag", "UserName", "Password" },
                                 new string[] { "3", UserName, obj.SHA512_HASH(NewPassword) }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                        }
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                        }
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Old Password Not Matched." }));
                    }
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    #region=======================user Registration========================
    //// Office_Type for Trade DFO Login
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    // public void Get_OfficeType_ForTrade_ForDFO(string Key)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    APIProcedure apiprocedure = new APIProcedure();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (Key == securityKey)
    //    {
    //        try
    //        {
    //            string saltkey = apiprocedure.GenerateSaltKey();
    //            DataSet ds1 = new DataSet();

                 

    //            ds1 = obj.ByProcedure("USP_tblAdminOfficeType_GetTypeForTrade",
    //                      new string[] {},
    //                      new string[] {}, "dataset");

    //            if (ds1.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
    //            }
    //            else
    //            {

    //                dt = ds1.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;
    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }
    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}


    //Get Office_Type for Trade for PCS Login
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_OfficeType_ForTrade_ForPCS(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();

                //if (OfficeType_ID.ToString() == "3")
                //{
                //    ds1 = obj.ByProcedure("USP_tblAdminOfficeType_GetTypeForTrade",
                //             new string[] { },
                //             new string[] { }, "dataset");
                //}
                //else if (OfficeType_ID.ToString() == "4")
                //{
                    ds1 = obj.ByProcedure("USP_tblAdminOfficeType_GetTypeForTrade",
                              new string[] { "OfficeType_ID" },
                              new string[] { "5" }, "dataset");
               // }

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //Get Degignation for Trade by OfficeType_ID
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_Degignation_ForTrade_by_OfficeType_ID(string Key, string OfficeType_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();

                
                ds1 = obj.ByProcedure("USP_Trade_GetDesignation_By_ID", new string[] { "OfficeType_ID" }, new string[] { OfficeType_ID.ToString() }, "dataset");
               

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    //Get Bank List
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_AllBank(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Mst_Bank_GetData", new string[] { }, new string[] { }, "dataset");
               

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //Insert User Information
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void User_Reg_Insert(string Key ,string UserType_Id,string Designation_ID ,string Name_Hi ,string Name_Eng ,string Mobile_No ,string U_Image ,string U_Document ,string U_Address ,string Bank_ID ,string BankAccount_No ,string IFSC_Code ,string Acount_Holder_Name ,string Office_ID ,string CreatedBy ,string CreatedByIP ,string Aadhar_No ,string Created_OfficeType_ID ,string PCS_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_UserRegistration_Insert", new string[] {"UserType_Id" ,"Designation_ID", "Name_Hi", "Name_Eng", "Mobile_No", "U_Image", "U_Document", "U_Address", "Bank_ID", "BankAccount_No", "IFSC_Code", "Acount_Holder_Name", "Office_ID", "CreatedBy", "CreatedByIP", "Aadhar_No", "Created_OfficeType_ID", "PCS_ID" },
                                                                              new string[] { UserType_Id.ToString(), Designation_ID.ToString(), Name_Hi.ToString(), Name_Eng.ToString(), Mobile_No.ToString(), U_Image.ToString(), U_Document.ToString(), U_Address.ToString(), Bank_ID.ToString(), BankAccount_No.ToString(), IFSC_Code.ToString(), Acount_Holder_Name.ToString(), Office_ID.ToString(), CreatedBy.ToString(), CreatedByIP.ToString(), Aadhar_No.ToString(), Created_OfficeType_ID.ToString(), PCS_ID.ToString() }, "dataset");


                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    //Update User Information
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void User_Reg_Update(string Key, string Name_Hi, string Name_Eng, string Mobile_No, string U_Image, string U_Document, string U_Address, string Bank_ID, string BankAccount_No, string IFSC_Code, string Acount_Holder_Name, string LastUpdatedBy, string LastUpdatedByIP, string UserID, string Aadhar_No)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();


                ds = obj.ByProcedure("USP_Trade_UserRegistration_Update", new string[] { "Name_Hi", "Name_Eng", "Mobile_No", "U_Image", "U_Document", "U_Address", "Bank_ID", "BankAccount_No", "IFSC_Code", "Acount_Holder_Name", "LastUpdatedBy", "LastUpdatedByIP", "UserID", "Aadhar_No" },
                                                                                  new string[] { Name_Hi.ToString(), Name_Eng.ToString(), Mobile_No.ToString(), U_Image.ToString(), U_Document.ToString(), U_Address.ToString(), Bank_ID.ToString(), BankAccount_No.ToString(), IFSC_Code.ToString(), Acount_Holder_Name.ToString(), LastUpdatedBy.ToString(), LastUpdatedByIP.ToString(), UserID.ToString(), Aadhar_No.ToString() }, "dataset");


                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //Get All User Data For PCS
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_All_UserData_For_PCS(string Key, string Office_ID, string PCS_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();


                ds = obj.ByProcedure("USP_Trade_UserRegistration_GetData", new string[] { "Office_ID", "PCS_ID" }, new string[] { Office_ID.ToString(), PCS_ID.ToString() }, "dataset");


                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //Get All User Data BY User_ID 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_All_UserData_BY_User_ID(string Key, string User_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();


                ds = obj.ByProcedure("USP_Trade_UserRegistration_GetDataById", new string[] { "User_ID" }, new string[] { User_ID.ToString() }, "dataset");


                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Activate/Deactivate User by User_ID(0 for Deactivate /1 for  Activate) 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ActivateorDeactivate_User_BY_User_ID(string Key, string User_ID, string IsActive, string LastIsActiveBy, string LastIsActiveByIP)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();


                ds = obj.ByProcedure("USP_Trade_UserRegistration_UpdateIsActive", new string[] { "UserID", "IsActive", "LastIsActiveBy", "LastIsActiveByIP" },
                                                                          new string[] { User_ID.ToString(), IsActive.ToString(), LastIsActiveBy.ToString(), LastIsActiveByIP.ToString() }, "dataset");


                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    #endregion================================================================


    #region=======================TenduPatta Collection========================
    
    //To Get offices for Office Dropdown at PCS Login
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_offices_forPCS_Login(string Key, string UserID,  string PCS_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 =  obj.ByProcedure("USP_GetOffice_PCS_Phad_ByUserID", new string[] { "flag", "UserID", "OfficeType_Id", "PCS_ID" }
                    , new string[] { "2", UserID.ToString(), "4", PCS_ID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }
                
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            
            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get offices for Office Dropdown at Phad Login
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_offices_forPhad_Login(string Key, string UserID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get Plucker List Officewise
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_Plucker_List_Officewise(string Key, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_Plucker_GetById", new string[] { "Office_ID" }
                    , new string[] { Office_ID.ToString() }, "dataset");
               if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }
                
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            
            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get Family Member of Plucker 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_Family_Member_ofPlucker(string Key, string PLKR_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_PluckerChild_GetByPluckeID"
                    , new string[] { "PLKR_ID" }, new string[] { PLKR_ID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get Itemtype ForCollection 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_Itemtype_ForCollection(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Itemtype_GetForCollection"
                    , new string[] { }, new string[] { }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get Items by CollectionDate 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_Items_by_CollectionDate(string Key,string ItemType_id,string Collection_Date)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();

                string Collection_Date1 = (Convert.ToDateTime(Collection_Date.ToString(), culture).ToString("yyyy/MM/dd"));
                ds1 = obj.ByProcedure("USP_Trade_Get_Item", new string[] { "flag", "ItemType_id", "Collection_Date" }
                    , new string[] { "1", ItemType_id.ToString(), Collection_Date1.ToString() }, "dataset");
              
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get Item Rate by ItemID
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_Items_Rate_by_ItemID(string Key, string Date, string Item_Id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();

                string Date1 = (Convert.ToDateTime(Date.ToString(), culture).ToString("yyyy/MM/dd"));
                ds1 = obj.ByProcedure("USP_TradeOrMSP_Mst_PurchaseRate_GetbyId", new string[] { "dt", "Item_Id" }, new string[] { Date1.ToString(), Item_Id.ToString() }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Insert Item Collection
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Insert_Item_Collection(string Key, string PCS_Id, string Phad_Id, string PLKR_ID, string PLKR_Child_ID, string ItemType_Id, string Item_Id, string Item_PurchaseRate, string Quantity_PerUnit, string Total_Amount, string Collection_Date, string Office_ID, string CreatedBy, string CreatedByIP, string OfficeType_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();

                string Collection_Date1 = (Convert.ToDateTime(Collection_Date.ToString(), culture).ToString("yyyy/MM/dd"));
                ds1 = obj.ByProcedure("USP_TradeOrMSP_Trn_ItemCollection_Insert", new string[] { "PCS_Id", "Phad_Id", "PLKR_ID", "PLKR_Child_ID", "ItemType_Id", "Item_Id", "Item_PurchaseRate", "Quantity_PerUnit", "Total_Amount", "Collection_Date", "Office_ID", "CreatedBy", "CreatedByIP", "OfficeType_ID" },
                                        new string[] { PCS_Id.ToString(), Phad_Id.ToString(), PLKR_ID.ToString(), PLKR_Child_ID.ToString(), ItemType_Id.ToString(), Item_Id.ToString(), Item_PurchaseRate.ToString(), Quantity_PerUnit.ToString(), Total_Amount.ToString(), Collection_Date1.ToString(), Office_ID.ToString(), CreatedBy.ToString(),  CreatedByIP.ToString(), OfficeType_ID.ToString() }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Update Item Collection
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Update_Item_Collection(string Key, string PLKR_Child_ID, string Quantity_PerUnit, string Total_Amount, string Office_ID, string LastUpdatedBy, string LastUpdatedByIP, string Collection_Date, string Collection_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();

                string Collection_Date1 = (Convert.ToDateTime(Collection_Date.ToString(), culture).ToString("yyyy/MM/dd"));
                ds1 = obj.ByProcedure("USP_TradeOrMSP_Trn_ItemCollection_Update",
                        new string[] { "PLKR_Child_ID", "Quantity_PerUnit", "Total_Amount", "Office_ID", "LastUpdatedBy", "LastUpdatedByIP", "Collection_Date", "Collection_ID" },
                        new string[] { PLKR_Child_ID.ToString(), Quantity_PerUnit.ToString(), Total_Amount.ToString(), Office_ID.ToString(), LastUpdatedBy.ToString(), LastUpdatedByIP.ToString(), Collection_Date1.ToString(), Collection_ID.ToString() }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }



    #endregion================================================================

    #region=======================TP1 Mukhya========================

    //To Get offices Details of Phud Munshi to Fill Phad Dropdown/Phud_Munsi_ID means User_ID
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_offices_Details_of_Phud_Munshi(string Key, string OfficeType_ID, string Office_ID,string Phud_Munsi_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_TraderQC_Getofficedetails",
                 new string[] { "OfficeType_ID", "Office_ID", "Phud_Munsi_ID" },
                   new string[] { OfficeType_ID.ToString(), Office_ID.ToString(), Phud_Munsi_ID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get Phad Munshi name of selected Phad by Phad ID
    public void Get_Phad_Munshi_name_By_Phad_ID(string Key, string Phad_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_TraderQC_GetPhud_Munsi_Name",
                   new string[] { "Phad_ID" },
                     new string[] { Phad_ID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get Collection Of TenduPatta Phadwise
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetCollection_Of_TenduPatta_Phadwise(string Key, string OfficeType_ID, string Phad_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_TraderQC_GetCollection",
                   new string[] { "OfficeType_ID", "Phad_ID", "Item_Id" },
                     new string[] { OfficeType_ID.ToString(), Phad_ID.ToString(), "4" }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get Transact Pass details PCS/Phadwise
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_Transact_Pass_details_PCSorPhadwise(string Key, string OfficeType_ID, string Lot_Number)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_Trader_QC_Get_PCSPhadwise",
                   new string[] { "OfficeType_ID", "Item_Id", "Lot_Number" },
                   new string[] { OfficeType_ID.ToString(), "4", Lot_Number.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    //To Transact Pass1 Insert
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Trade_Transact_Pass1_Insert(string Key, string PCS_ID, string TraderRepresentativeName, string Trader_QC_Date, string Total_Bundle, string Total_Accepted_Bundle, string Lot_Number, string CreatedBy, string CreatedByIP, string OfficeType_ID, string Phad_Id, string Remark)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();

                string Trader_QC_Date1 = (Convert.ToDateTime(Trader_QC_Date.ToString(), culture).ToString("yyyy/MM/dd"));
                ds1 = obj.ByProcedure("USP_Trade_Trader_QC_Insert",
                                new string[] { "PCS_ID", "TraderRepresentativeName", "Trader_QC_Date", "Total_Bundle", "Total_Accepted_Bundle", "Lot_Number", "IsActive", "CreatedBy", "CreatedByIP", "Item_Id", "OfficeType_ID", "Phad_Id","Remark" },
                                new string[] { PCS_ID.ToString(), TraderRepresentativeName.ToString(), Trader_QC_Date1.ToString(), Total_Bundle.ToString(), Total_Accepted_Bundle.ToString(), Lot_Number.ToString(), "1", CreatedBy.ToString(), CreatedByIP.ToString(), "4", OfficeType_ID.ToString(), Phad_Id.ToString(), Remark.ToString()},
                                 new string[] { "Type_Trade_Trader_QC_Child" },
                                           new DataTable[] { dt },
                                           "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Transact Pass1 Update
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Trade_Transact_Pass1_Update(string Key, string Trader_QC_ID, string TraderRepresentativeName, string Trader_QC_Date, string Total_Bundle, string Total_Accepted_Bundle, string CreatedBy, string CreatedByIP, string Remark)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();

                string Trader_QC_Date1 = (Convert.ToDateTime(Trader_QC_Date.ToString(), culture).ToString("yyyy/MM/dd"));
                ds1 = obj.ByProcedure("USP_Trade_Trader_QC_Update",
                       new string[] { "Trader_QC_ID", "TraderRepresentativeName", "Trader_QC_Date"
                           , "Total_Bundle", "Total_Accepted_Bundle", "CreatedBy", "CreatedByIP","Remark"},
                       new string[] { Trader_QC_ID.ToString(), TraderRepresentativeName.ToString(), Trader_QC_Date1.ToString(), Total_Bundle.ToString()
                           , Total_Accepted_Bundle.ToString(), CreatedBy.ToString(), CreatedByIP.ToString(), Remark.ToString() },

                                  "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Transact Pass1 Deatils By TP1_ID
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetTP1_Deatils_By_TP1ID(string Key, string Trader_QC_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();

                
                ds1 = obj.ByProcedure("USP_Trade_Trader_QC_Get_IDWise",
                         new string[] { "Trader_QC_ID" },
                           new string[] { Trader_QC_ID.ToString() }, "dataset");

                                 
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Transact Pass1 Collections Records
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_Transact_Pass1_Collections_Records(string Key, string Trader_QC_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_TraderQC_GetCollection_Exists",
                         new string[] { "Trader_QC_ID" },
                           new string[] { Trader_QC_ID.ToString() }, "dataset");


                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    #endregion=======================TP1 Mukhya========================

    #region=======================TP1 Shayak========================

    //To Get TP1 Deatils For TP1 Sahayak By TP1_ID/Trader_QC_ID
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_TP1_Deatils_For_TP1_Sahayak_By_TP1_ID_For_Update(string Key, string Trader_QC_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_TransactPass_Tp_1_Get_PCSLOtNumTrader_ByTransactPass_Tp_1_Id",
                         new string[] { "Trader_QC_ID" },
                           new string[] { Trader_QC_ID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get offices Details of Phud Munshi to Fill Phad Dropdown/Phud_Munsi_ID means User_ID
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_TP1_Deatils_For_TP1_Sahayak_By_TP1_ID(string Key, string OfficeType_ID, string Office_ID, string Phud_Munsi_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_TransactPass_Tp_1_Get_PCSLOtNumTrader_ByTrader_QC_ID",
                 new string[] { "OfficeType_ID", "Office_ID", "Phud_Munsi_ID" },
                   new string[] { OfficeType_ID.ToString(), Office_ID.ToString(), Phud_Munsi_ID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get Warehouses By PCSID for Dropdown
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_Warehouses_ByPCSID(string Key, string Module_ID, string PCS_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_Mst_Warehouse_select_PCSwise",
                    new string[] { "Module_ID", "PCS_ID" },
                      new string[] { Module_ID.ToString(), PCS_ID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Insert TP1 Sahayak
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Insert_TP1_Sahayak(string Key, string  Trader_ID, string Trader_FirmName, string Lot_Number, string Range_ID, string Division_ID, string MainReference
, string PermittedQuantity, string STDBag, string ACTBag, string ValidTillDate, string PreSTDBag_Total, string PreACTBag_Total, string CurentSTDBag, string CurentACTBag, string FromPlacePCS_ID
,string FromPlacePCS_Name, string ToPlaceWarehouse_ID, string ToPlaceWarehouseName, string TransportRoute, string Vehicle_No, string InvestigationPlace, string PermitValidDate, string Date_End
,string Time_End ,string checked1, string Trader_QC_Id,  string CreatedBy, string CreatedByIP, string PCS_ID, string Shipped_Quantity, string Remaining_quantity, string Phad_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string validtilldate1 = Convert.ToDateTime(ValidTillDate.ToString().Trim(), cult).ToString("yyyy/MM/dd");
                string PermitValidDate1 = Convert.ToDateTime(PermitValidDate.ToString().Trim(), cult).ToString("yyyy/MM/dd");
                string Date_End1 = Convert.ToDateTime(Date_End.ToString().Trim(), cult).ToString("yyyy/MM/dd");
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_TransactPass_Tp_1_Insert",
                                new string[] { "Trader_ID","Trader_FirmName","Lot_Number","Range_ID","Division_ID","MainReference"
                            ,"PermittedQuantity","STDBag","ACTBag","ValidTillDate","PreSTDBag_Total","PreACTBag_Total","CurentSTDBag","CurentACTBag","FromPlacePCS_ID"
                        ,"FromPlacePCS_Name","ToPlaceWarehouse_ID","ToPlaceWarehouseName","TransportRoute","Vehicle_No","InvestigationPlace","PermitValidDate","Date_End"
                        ,"Time_End" ,"checked","Trader_QC_Id","IsActive","CreatedBy","CreatedByIP","PCS_ID","Shipped_Quantity","Remaining_quantity","Phad_ID"},
                                new string[] { Trader_ID.ToString() , Trader_FirmName.ToString() , Lot_Number.ToString() , Range_ID.ToString() , Division_ID.ToString() , MainReference.ToString()
 , PermittedQuantity.ToString() , STDBag.ToString() , ACTBag.ToString() , validtilldate1.ToString() , PreSTDBag_Total.ToString() , PreACTBag_Total.ToString() , CurentSTDBag.ToString() , CurentACTBag.ToString() , FromPlacePCS_ID.ToString()
, FromPlacePCS_Name.ToString() , ToPlaceWarehouse_ID.ToString() , ToPlaceWarehouseName.ToString() , TransportRoute.ToString() , Vehicle_No.ToString() , InvestigationPlace.ToString() , PermitValidDate1.ToString() , Date_End1.ToString()
, Time_End.ToString(),checked1.ToString() , Trader_QC_Id.ToString() , "1" , CreatedBy.ToString() , CreatedByIP.ToString() , PCS_ID.ToString() , Shipped_Quantity.ToString() , Remaining_quantity.ToString() , Phad_ID.ToString()},
                                            "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Update TP1 Sahayak
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Update_TP1_Sahayak(string Key, string TransactPass_Tp_1_Id, string MainReference, string STDBag, string ACTBag, string ValidTillDate, string CurentSTDBag, string CurentACTBag, string ToPlaceWarehouse_ID,
string ToPlaceWarehouseName, string TransportRoute, string Vehicle_No, string InvestigationPlace, string PermitValidDate, string Date_End, string Time_End, string checked1, string CreatedBy,
string CreatedByIP, string Shipped_Quantity, string Remaining_quantity)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_TransactPass_Tp_1_Update",
                                  new string[] {"TransactPass_Tp_1_Id","MainReference","STDBag","ACTBag","ValidTillDate","CurentSTDBag","CurentACTBag","ToPlaceWarehouse_ID"
                                      ,"ToPlaceWarehouseName","TransportRoute","Vehicle_No","InvestigationPlace","PermitValidDate","Date_End","Time_End","checked","CreatedBy"
                                      ,"CreatedByIP","Shipped_Quantity","Remaining_quantity"},
                                  new string[] { TransactPass_Tp_1_Id.ToString() , MainReference.ToString() , STDBag.ToString() , ACTBag.ToString() , ValidTillDate.ToString() 
                                      , CurentSTDBag.ToString() , CurentACTBag.ToString() , ToPlaceWarehouse_ID.ToString(),ToPlaceWarehouseName.ToString() , TransportRoute.ToString()
                                      , Vehicle_No.ToString() , InvestigationPlace.ToString() , PermitValidDate.ToString() , Date_End.ToString() , Time_End.ToString() 
                                      , checked1.ToString() , CreatedBy.ToString(),CreatedByIP.ToString() , Shipped_Quantity.ToString() , Remaining_quantity.ToString()},
                                              "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //To Get Previous Sale To Trader
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_Previous_SaleTo_Trader(string Key, string Trader_ID, string Lot_Number, string PCS_Id, string Phad_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("USP_Trade_Get_Previous_SaleTo_Trader",
              new string[] { "Trader_ID", "Lot_Number", "PCS_Id", "Phad_ID" },
                new string[] { Trader_ID.ToString(), Lot_Number.ToString(), PCS_Id.ToString(), Phad_ID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }

            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }

            ds.Clear();
            dt.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

   



    #endregion=======================TP1 Shayak========================



}

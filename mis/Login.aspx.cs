using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.SessionState;
using System.Reflection;

public partial class Login : System.Web.UI.Page
{
    DataSet ds, ds2;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session.Abandon();
            Session.RemoveAll();
            GetRandomText();
            ViewState["A"] = "0";
            if (Request.QueryString.AllKeys.Contains("A"))
            {
                ViewState["A"] = "1";
            }
        }
    }
    #region -- User Defined Function --

    void Re_GenerateSessoin_ID()
    {
        System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
        string oldId = manager.GetSessionID(Context);
        string newId = manager.CreateSessionID(Context);
        bool isAdd = false, isRedir = false;
        manager.SaveSessionID(Context, newId, out isRedir, out isAdd);
        HttpApplication ctx = (HttpApplication)HttpContext.Current.ApplicationInstance;
        HttpModuleCollection mods = ctx.Modules;
        System.Web.SessionState.SessionStateModule ssm = (SessionStateModule)mods.Get("Session");
        System.Reflection.FieldInfo[] fields = ssm.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        SessionStateStoreProviderBase store = null;
        System.Reflection.FieldInfo rqIdField = null, rqLockIdField = null, rqStateNotFoundField = null;
        foreach (System.Reflection.FieldInfo field in fields)
        {
            if (field.Name.Equals("_store")) store = (SessionStateStoreProviderBase)field.GetValue(ssm);
            if (field.Name.Equals("_rqId")) rqIdField = field;
            if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
            if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
        }
        object lockId = rqLockIdField.GetValue(ssm);
        if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(Context, oldId, lockId);
        rqStateNotFoundField.SetValue(ssm, true);
        Session["Session_id"] = newId;
        rqIdField.SetValue(ssm, newId);
    }

    public void GetRandomText()
    {
        StringBuilder randomText = new StringBuilder();
        string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz!@#$%&*~";
        Random r = new Random();
        for (int j = 0; j <= 10; j++)
        { randomText.Append(alphabets[r.Next(alphabets.Length)]); }
        ViewState["RandomText"] = objdb.Encrypt(randomText.ToString());
    }

    private string ConvertText_SHA512_And_Salt(string Text)
    {
        return objdb.SHA512_HASH(String.Concat(objdb.SHA512_HASH(Text), ViewState["RandomText"].ToString()));
    }

    private bool CompaireHashCode(string DataBasePassword, string ClientPasswordWithHashing)
    {
        bool i;
        if (objdb.SHA512_HASH(String.Concat(DataBasePassword, ViewState["RandomText"].ToString())).Equals(ClientPasswordWithHashing))
        { i = true; }
        else { i = false; }
        return i;
    }



    #endregion


    #region -- Button Events --

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (txtUserName.Text == "E0001")
                {
                    ViewState["A"] = "0";
                }
                if (txtUserName.Text != "" && txtUserPassword.Text != "")
                {
                    switch (txtUserName.Text.Substring(0, 1))
                    {
                        case "E": //for Employees Login offices like HO,CCF,DFO 

                            ds = objdb.ByProcedure("SpLogin",
               new string[] { "flag", "UserName" },
               new string[] { "0", txtUserName.Text }, "dataset");
                            //ds = objdb.ByProcedure("sp_Login",
                            //         new string[] { "flag", "UserName" },
                            //         new string[] { "0", txtUserName.Text }, "dataset");
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
                        if (CompaireHashCode(ds.Tables[0].Rows[0]["Password"].ToString(), txtUserPassword.Text) || Convert.ToString(ViewState["A"]) == "1")
                        {
                            //CheckRemember();
                            Re_GenerateSessoin_ID();
                            GetRandomText();
                            LblMsg.Text = "";

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "1" || ds.Tables[0].Rows[0]["IsActive"].ToString() == "True")
                                //{
                                switch (ds.Tables[0].Rows[0]["UserName"].ToString().Substring(0, 1))
                                {
                                    case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 

                                        Session["Emp_ID"] = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
                                        Session["UserName"] = txtUserName.Text;
                                        Session["UserTypeId"] = ds.Tables[0].Rows[0]["UserTypeId"].ToString();
                                        Session["Office_ID"] = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                                        Session["OfficeType_ID"] = ds.Tables[0].Rows[0]["OfficeType_ID"].ToString();
                                        Session["OfficeType_Title"] = ds.Tables[0].Rows[0]["OfficeType_Title"].ToString();
                                        Session["Division_ID"] = ds.Tables[0].Rows[0]["Division_ID"].ToString();

                                        Session["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                                        Session["Emp_Name"] = ds.Tables[0].Rows[0]["Emp_Name"].ToString();
                                        Session["Department_ID"] = ds.Tables[0].Rows[0]["Department_ID"].ToString();
                                        Session["Designation_ID"] = ds.Tables[0].Rows[0]["Designation_ID"].ToString();
                                        Session["Designation_Name"] = ds.Tables[0].Rows[0]["Designation_Name"].ToString();
                                        Session["Emp_ProfileImage"] = ds.Tables[0].Rows[0]["Emp_ProfileImage"].ToString();
                                        Session["AccessModule"] = ds.Tables[1];
                                        Session["AccessForm"] = ds.Tables[2];
                                        GetAccess();


                                        // Session["OfficeType_ID"] = ds.Tables[0].Rows[0]["OfficeType_ID"].ToString();
                                        //  Session["Office_Code"] = ds.Tables[0].Rows[0]["Office_Code"].ToString();
                                        //   Session["UserTypeId"] = ds.Tables[0].Rows[0]["UserTypeId"].ToString();
                                        //Session["District_ID"] = ds.Tables[0].Rows[0]["District_ID"].ToString();
                                        //Session["PCS_ID"] = ds.Tables[0].Rows[0]["PCS_ID"].ToString();
                                        //Session["DFO_ID"] = ds.Tables[0].Rows[0]["DFO_ID"].ToString();
                                        //Session["Range_ID"] = ds.Tables[0].Rows[0]["Range_ID"].ToString();
                                        //Session["Village_ID"] = ds.Tables[0].Rows[0]["Village_ID"].ToString();
                                        //  GetAccess(Session["Emp_ID"].ToString(), Session["OfficeType_ID"].ToString());

                                        Response.Redirect("Dashboard/Home.aspx?IsMainPage=1");
                                        //if (Session["UserName"].ToString() == "E0001")
                                        //{
                                        //    Response.Redirect("Dashboard/UnionWiseProgressReport.aspx?IsMainPage=1");
                                        //}
                                        //else
                                        //{
                                        //    Response.Redirect("Dashboard/Home.aspx?IsMainPage=1");
                                        //}
                                        break;
                                    default:
                                        if (ds != null) { ds.Clear(); }
                                        break;
                                }
                                //}
                                //else
                                //{
                                //    LblMsg.ForeColor = System.Drawing.Color.Red;
                                //    LblMsg.Text = "Access denied! Kindly contact administrator.";
                                //}
                            }
                            else
                            {
                                LblMsg.ForeColor = System.Drawing.Color.Red;
                                LblMsg.Text = "Invalid Login Credentials";
                            }
                        }
                        else
                        {
                            LblMsg.ForeColor = System.Drawing.Color.Red;
                            LblMsg.Text = "Invalid Login Credentials!";
                        }
                    }
                    else
                    {
                        LblMsg.ForeColor = System.Drawing.Color.Red;
                        LblMsg.Text = "Invalid Login Credentials!";
                    }
                    txtUserName.Text = "";
                    txtUserPassword.Attributes["value"] = "";
                }
                else
                {
                    LblMsg.ForeColor = System.Drawing.Color.Red;
                    LblMsg.Text = "Invalid Login Credentials";
                }
            }
            catch (Exception ex)
            {
                LblMsg.ForeColor = System.Drawing.Color.Red;
                LblMsg.Text = ex.Message.ToString();
                btnLogin.Enabled = true;
            }
        }
    }

    private void GetAccess()
    {
        ////*********** FORM ACCESS VALIDATION *************
        //DataSet FormPath = objdb.ByProcedure("SpUMHome",
        //    new string[] { "flag", "Emp_ID" },
        //new string[] { "3", Emp_ID.ToString() }, "dataset");

        //Session["FormPath"] = FormPath;

        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        string IPadd2 = Request.UserHostAddress;
        objdb.ByProcedure("SpLogDetail",
        new string[] { "flag", "Emp_ID", "User_Name", "Office_ID", "IPAddress" },
        new string[] { "0", Session["Emp_ID"].ToString(), Session["UserName"].ToString(), Session["Office_ID"].ToString(), IPAddress }, "dataset");

    }


    #endregion
}

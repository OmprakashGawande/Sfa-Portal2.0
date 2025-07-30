using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;

public partial class mis_HR_AddSeniorityList : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetSeniortyList();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    public void Clear()
    {
        lblError.Text = string.Empty;
       
        btnSubmit.Text = "Save";
        btnClear.Text = "Cancel";
        pnlupload.Visible = true;
        rfvupload.Enabled = false;
        GridView1.SelectedIndex = -1;
        txtYear.Text = string.Empty;
        ddlDesignation.SelectedIndex = 0;
        GetDatatableHeaderDesign();
    }
    private void GetSeniortyList()
    {
        try
        {
            lblError.Text = "";

            ds = objdb.ByProcedure("Sp_tblEmpSenorityList",
                     new string[] { "flag" },
                     new string[] { "0" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                ds.Clear();
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + ex.Message.ToString());
        }
    }

    protected void ValidateFileSize(object sender, ServerValidateEventArgs e)
    {
        decimal size = Math.Round(((decimal)fuSenorilitylistCopy.PostedFile.ContentLength / (decimal)1024), 2);
        string strFileNam = fuSenorilitylistCopy.FileName.ToString();
        string strExtenson = Path.GetExtension(strFileNam);
        if (strExtenson == ".jpg" || strExtenson == ".jpeg" || strExtenson == ".png" || strExtenson == ".pdf" || strExtenson == ".JPG" || strExtenson == ".JPEG" || strExtenson == ".PNG" || strExtenson == ".PDF" )
        {
            if (size > 512)
            {
                CustomValidator1.Text = "<i class='fa fa-exclamation-circle' title='File size must not exceed 512 KB !'></i>";
                CustomValidator1.ErrorMessage = "File size must not exceed 512 KB.";
                e.IsValid = false;

            }
        }
        else
        {
            CustomValidator1.Text = "<i class='fa fa-exclamation-circle' title='Invalid file , Only pdf,jpg,jpeg,png file allowed !'></i>";
            CustomValidator1.ErrorMessage = "Invalid file , Only pdf,jpg,jpeg,png file allowed.";
            e.IsValid = false;

        }

    }
    public string SeniorityListUpload()
    {
        string strFileName = "";

        if (fuSenorilitylistCopy.HasFile)
        {
            string strFileNam = fuSenorilitylistCopy.FileName.ToString();
            string strExtenson = Path.GetExtension(strFileNam);

            try
            {
                strFileName = fuSenorilitylistCopy.FileName.ToString();
                string strExtension = Path.GetExtension(strFileName);
                string strTimeStamp = DateTime.Now.ToString();
                strTimeStamp = strTimeStamp.Replace("/", "-");
                strTimeStamp = strTimeStamp.Replace(" ", "-");
                strTimeStamp = strTimeStamp.Replace(":", "-");
                string strName = Path.GetFileNameWithoutExtension(strFileName);
                strFileName = strName + "-" + strTimeStamp + strExtension;
                string path = Path.Combine(Server.MapPath("../HR/UploadDoc/SeniorityList/"), strFileName);
                string path1 = strFileName;
                fuSenorilitylistCopy.PostedFile.SaveAs(path);
                return path1;
            }
            catch (Exception ex)
            {
                string path2 = Path.Combine(Server.MapPath("../HR/UploadDoc/SeniorityList/"), strFileName);
                if (File.Exists(path2))
                {
                    File.Delete(path2);

                }
                return lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error(Seniority List Copy : ", ex.Message.ToString());
            }
        }
        else
        {
            return "";
        }
    }
    private void InsersertOrUpdateList()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                if (btnSubmit.Text == "Save")
                {

                    ds = objdb.ByProcedure("Sp_tblEmpSenorityList",
                        new string[] { "flag", "Designation_ID", "SLYear", "SLDoc", "CreatedBy", "Office_Id", "ipaddress" },
                        new string[] { "1", ddlDesignation.SelectedValue, txtYear.Text.Trim(), SeniorityListUpload().ToString(), objdb.createdBy(), objdb.Office_ID(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetSeniortyList();
                        Clear();

                        lblError.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetSeniortyList();
                        if(SeniorityListUpload().ToString()!="")
                        {
                            string path4 = Path.Combine(Server.MapPath("../HR/UploadDoc/SeniorityList/"), SeniorityListUpload().ToString());
                            if (File.Exists(path4))
                            {
                                File.Delete(path4);

                            }
                        }
                        lblError.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Warning :" + warning + " for Designation : " + ddlDesignation.SelectedItem.Text + " and Year :" + txtYear.Text);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetSeniortyList();
                        lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    GetDatatableHeaderDesign();
                    ds.Clear();
                }
                if (btnSubmit.Text == "Update")
                {
                    lblError.Text = "";
                    string ccopy = "";
                    if (SeniorityListUpload() == "")
                    {
                        ccopy = ViewState["listcopy"].ToString();
                    }
                    else
                    {

                        ccopy = SeniorityListUpload();
                    }
                    ds = objdb.ByProcedure("Sp_tblEmpSenorityList",
                             new string[] { "flag", "EmpSenorityList_id", "Designation_ID", "SLYear", "SLDoc", "CreatedBy", "Office_Id", "ipaddress", "PageName", "Remark" },
                             new string[] { "2", ViewState["rid"].ToString(), ddlDesignation.SelectedValue, txtYear.Text.Trim(), ccopy, objdb.createdBy(), objdb.Office_ID(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Seniority List Record Updated" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        hypListCopy.Visible = false;
                        hypListCopy.NavigateUrl = "";
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetSeniortyList();
                        if (SeniorityListUpload() != "" && ViewState["listcopy"].ToString() != "")
                        {
                            string path3 = Path.Combine(Server.MapPath("../HR/UploadDoc/SeniorityList/"), ViewState["listcopy"].ToString());
                            if (File.Exists(path3))
                            {
                                File.Delete(path3);

                            }
                        }

                        Clear();
                        lblError.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetSeniortyList();
                        if (SeniorityListUpload().ToString() != "")
                        {
                            string path5 = Path.Combine(Server.MapPath("../HR/UploadDoc/SeniorityList/"), SeniorityListUpload().ToString());
                            if (File.Exists(path5))
                            {
                                File.Delete(path5);

                            }
                        }
                        lblError.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Warning :" + warning + " for Designation : " + ddlDesignation.SelectedItem.Text + " and Year :" + txtYear.Text);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetSeniortyList();
                        Clear();
                        lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    ccopy = "";
                    ds.Clear();
                    GetDatatableHeaderDesign();
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            else
            {
                lblError.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Please Select From Office");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Error !", ex.Message.ToString());
            if (SeniorityListUpload().ToString() != "")
            {
                string pathe = Path.Combine(Server.MapPath("../HR/UploadDoc/SeniorityList/"), SeniorityListUpload().ToString());
                if (File.Exists(pathe))
                {
                    File.Delete(pathe);

                }
            }
            if (ViewState["listcopy"].ToString() != "")
            {
                string path3 = Path.Combine(Server.MapPath("../HR/UploadDoc/SeniorityList/"), ViewState["listcopy"].ToString());
                if (File.Exists(path3))
                {
                    File.Delete(path3);

                }
            }
        }
    }
    protected void ddlDesignation_Init(object sender, EventArgs e)
    {
        try
        {
            ddlDesignation.DataSource = objdb.ByProcedure("SpAdminDesignation",
                                 new string[] { "flag" },
                                 new string[] { "1", }, "Dataset");
            ddlDesignation.DataTextField = "Designation_Name";
            ddlDesignation.DataValueField = "Designation_ID";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsersertOrUpdateList();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblSLYear = (Label)row.FindControl("lblSLYear");
                    Label lblDesignation_ID = (Label)row.FindControl("lblDesignation_ID");
                    Label lblSLDoc = (Label)row.FindControl("lblSLDoc");

                    btnSubmit.Text = "Update";
                    btnClear.Text = "Cancel";
                    pnlupload.Visible = false;
                    rfvupload.Enabled = false;
                    ViewState["rid"] = e.CommandArgument.ToString();
                    txtYear.Text = lblSLYear.Text;
                    ddlDesignation.SelectedValue = lblDesignation_ID.Text;
                    ViewState["listcopy"] = lblSLDoc.Text;

                    if (lblSLDoc.Text != "")
                    {
                        hypListCopy.Visible = true;
                        hypListCopy.NavigateUrl = "../HR/UploadDoc/SeniorityList/" + lblSLDoc.Text;
                    }
                    else
                    {
                        hypListCopy.Visible = false;
                    }


                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LemonChiffon;
                            break;
                        }
                    }
                    GetDatatableHeaderDesign();
                }

            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    ds = objdb.ByProcedure("Sp_tblEmpSenorityList",
                                                new string[] { "flag", "EmpSenorityList_id", "CreatedBy", "PageName", "Remark", "ipaddress" },
                                                new string[] { "3", e.CommandArgument.ToString(), objdb.createdBy(), Path.GetFileName(Request.Url.AbsolutePath), "Seniority List Record Deleted", objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        ds.Clear();
                        GetSeniortyList();
                        lblError.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Deleted Successfully!");
                    }
                    else
                    {
                        ds.Clear();
                        GetSeniortyList();
                        lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        hypListCopy.Visible = false;
        hypListCopy.NavigateUrl = "";
        Clear();
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;


public partial class mis_HR_AddPromotionHistory : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    IFormatProvider culture = new CultureInfo("en-US", true);
    DataSet ds;
    string DOBDate = "";
    string DORDate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetPromotionList();
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
    private void GetEmployee()
    {
        try
        {
            ddlEmployye_Name.DataSource = objdb.ByProcedure("SpEmployeeRoleMap",
                      new string[] { "flag" },
                      new string[] { "6" }, "dataset");
            ddlEmployye_Name.DataTextField = "Emp";
            ddlEmployye_Name.DataValueField = "Emp_ID";

            ddlEmployye_Name.DataBind();
            ddlEmployye_Name.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Error 1", ex.Message.ToString());
        }
    }
    private void GetDepartment()
    {
        try
        {
            ddlDepartment.DataSource = objdb.ByProcedure("SpAdminDepartment", new string[] { "flag" }, new string[] { "1" }, "dataset");
            ddlDepartment.DataTextField = "Department_Name";
            ddlDepartment.DataValueField = "Department_ID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Error 2", ex.Message.ToString());
        }
    }
    public void Clear()
    {
       
        lblError.Text = string.Empty;
        btnSubmit.Text = "Save";
        btnClear.Text = "Cancel";
        pnlupload.Visible = true;
        rfvupload.Enabled = true;
        GridView1.SelectedIndex = -1;
        txtDOB.Text = string.Empty;
        txtDOR.Text = string.Empty;
        ddlDepartment.SelectedIndex = 0;
        ddlDesignation.SelectedIndex = 0;
        ddlOffice.SelectedIndex = 0;
        ddlEmployye_Name.SelectedIndex = 0;
        GetDatatableHeaderDesign();
    }
    private void GetPromotionList()
    {
        try
        {
            lblError.Text = "";

            ds = objdb.ByProcedure("Sp_tblEmpPromotionHistory",
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
        decimal size = Math.Round(((decimal)fuPromotionlistCopy.PostedFile.ContentLength / (decimal)1024), 2);
        string strFileNam = fuPromotionlistCopy.FileName.ToString();
        string strExtenson = Path.GetExtension(strFileNam);
        if (strExtenson == ".jpg" || strExtenson == ".jpeg" || strExtenson == ".png" || strExtenson == ".pdf" || strExtenson == ".JPG" || strExtenson == ".JPEG" || strExtenson == ".PNG" || strExtenson == ".PDF")
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
    public string PromotionListUpload()
    {
        string strFileName = "";

        if (fuPromotionlistCopy.HasFile)
        {
            string strFileNam = fuPromotionlistCopy.FileName.ToString();
            string strExtenson = Path.GetExtension(strFileNam);

            try
            {
                strFileName = fuPromotionlistCopy.FileName.ToString();
                string strExtension = Path.GetExtension(strFileName);
                string strTimeStamp = DateTime.Now.ToString();
                strTimeStamp = strTimeStamp.Replace("/", "-");
                strTimeStamp = strTimeStamp.Replace(" ", "-");
                strTimeStamp = strTimeStamp.Replace(":", "-");
                string strName = Path.GetFileNameWithoutExtension(strFileName);
                strFileName = strName + "-" + strTimeStamp + strExtension;
                string path = Path.Combine(Server.MapPath("../HR/UploadDoc/Promotion_Details/"), strFileName);
                string path1 = strFileName;
                fuPromotionlistCopy.PostedFile.SaveAs(path);
                return path1;
            }
            catch (Exception ex)
            {
                string path2 = Path.Combine(Server.MapPath("../HR/UploadDoc/Promotion_Details/"), strFileName);
                if (File.Exists(path2))
                {
                    File.Delete(path2);

                }
                return lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error(Promotion List Copy : ", ex.Message.ToString());
            }
        }
        else
        {
            return "";
        }
    }
    private void InsersertOrUpdateList()
    {
        string ccopy = "";
        lblError.Text = "";
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                 DateTime date3 = DateTime.ParseExact(txtDOB.Text, "dd/MM/yyyy", culture);
                DateTime date4 = DateTime.ParseExact(txtDOR.Text, "dd/MM/yyyy", culture);
              
                DOBDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                DORDate = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                if (btnSubmit.Text == "Save")
                {

                    ds = objdb.ByProcedure("Sp_tblEmpPromotionHistory",
                        new string[] { "flag", "Emp_ID", "Designation_ID", "Department_ID", "CurrentPosting", "DOB", "DOR", "ServiceBook", "CreatedBy", "Office_Id", "ipaddress" },
                        new string[] { "1", ddlEmployye_Name.SelectedValue, ddlDesignation.SelectedValue, ddlDepartment.SelectedValue, ddlOffice.SelectedValue, DOBDate.ToString(), DORDate.ToString(), PromotionListUpload().ToString(), objdb.createdBy(), objdb.Office_ID(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetPromotionList();
                        Clear();
                      
                        lblError.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetPromotionList();
                        if (PromotionListUpload().ToString() != "")
                        {
                            string path4 = Path.Combine(Server.MapPath("../HR/UploadDoc/Promotion_Details/"), PromotionListUpload().ToString());
                            if (File.Exists(path4))
                            {
                                File.Delete(path4);

                            }
                        }
                        lblError.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Warning :" + warning + " for " + ddlEmployye_Name.SelectedItem.Text);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetPromotionList();
                        lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    GetDatatableHeaderDesign();
                    ds.Clear();
                }
                if (btnSubmit.Text == "Update")
                {
                    if (PromotionListUpload() == "")
                    {
                        ccopy = ViewState["listcopy"].ToString();
                    }
                    else
                    {
                        ccopy = PromotionListUpload();
                    }
                    ds = objdb.ByProcedure("Sp_tblEmpPromotionHistory",
                             new string[] { "flag", "EmpPromotionHistory_id", "Emp_ID", "Designation_ID", "Department_ID", "CurrentPosting", "DOB", "DOR", "ServiceBook", "CreatedBy", "ipaddress", "PageName", "Remark" },
                             new string[] { "2", ViewState["rid"].ToString(), ddlEmployye_Name.SelectedValue, ddlDesignation.SelectedValue, ddlDepartment.SelectedValue, ddlOffice.SelectedValue, DOBDate.ToString(), DORDate.ToString(), ccopy, objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Promotion List Record Updated" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetPromotionList();
                        if (PromotionListUpload() != "" && ViewState["listcopy"].ToString() != "")
                        {
                            string path3 = Path.Combine(Server.MapPath("../HR/UploadDoc/Promotion_Details/"), ViewState["listcopy"].ToString());
                            if (File.Exists(path3))
                            {
                                File.Delete(path3);

                            }
                        }
                        hypListCopy.Visible = false;
                        hypListCopy.NavigateUrl = "";
                        Clear();
                        lblError.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetPromotionList();
                        if (PromotionListUpload().ToString() != "")
                        {
                            string path5 = Path.Combine(Server.MapPath("../HR/UploadDoc/Promotion_Details/"), PromotionListUpload().ToString());
                            if (File.Exists(path5))
                            {
                                File.Delete(path5);

                            }
                        }
                        lblError.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Warning :" + warning + " for " + ddlEmployye_Name.SelectedItem.Text);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetPromotionList();
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
                lblError.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Select Employee Name");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Error !", ex.Message.ToString());
            if (PromotionListUpload().ToString() != "")
            {
                string pathe = Path.Combine(Server.MapPath("../HR/UploadDoc/Promotion_Details/"), PromotionListUpload().ToString());
                if (File.Exists(pathe))
                {
                    File.Delete(pathe);

                }
            }

            if (ccopy != "" || ccopy != null)
            {
                string path3 = Path.Combine(Server.MapPath("../HR/UploadDoc/Promotion_Details/"), ViewState["listcopy"].ToString());
                if (File.Exists(path3))
                {
                    File.Delete(path3);

                }
            }
        }
    }
    private void FillOffice()
    {
        try
        {
           
            ddlOffice.DataSource = objdb.ByProcedure("SpAdminOffice",
                                      new string[] { "flag" },
                                      new string[] { "10", }, "Dataset");
            ddlOffice.DataTextField = "Office_Name";
            ddlOffice.DataValueField = "Office_ID";
            ddlOffice.DataBind();
            ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetEmpDetailsByEmp_ID()
    {
        try
        {
            if(ds!=null)
            {
                ds.Clear();
            }
            ds = objdb.ByProcedure("Sp_tblEmpPromotionHistory",
                                      new string[] { "flag","Emp_ID" },
                                      new string[] { "4",ddlEmployye_Name.SelectedValue }, "Dataset");

            if(ds.Tables[0].Rows.Count>0)
            {
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["Department_ID"].ToString();
                ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["Designation_ID"].ToString();
                ddlOffice.SelectedValue = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                txtDOB.Text = ds.Tables[0].Rows[0]["Emp_Dob"].ToString();
                txtDOR.Text = ds.Tables[0].Rows[0]["Emp_RetirementDate"].ToString();
                
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEmployye_Name_Init(object sender, EventArgs e)
    {
        GetEmployee();
    }
    protected void ddlDepartment_Init(object sender, EventArgs e)
    {
        GetDepartment();
    }
    protected void ddlOffice_Init(object sender, EventArgs e)
    {
        FillOffice();
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
            lblError.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblEmp_ID = (Label)row.FindControl("lblEmp_ID");
                    Label lblDepartment_ID = (Label)row.FindControl("lblDepartment_ID");
                    Label lblDesignation_ID = (Label)row.FindControl("lblDesignation_ID");
                    Label lblCurrentPosting = (Label)row.FindControl("lblCurrentPosting");
                    Label lblDOB = (Label)row.FindControl("lblDOB");
                    Label lblDOR = (Label)row.FindControl("lblDOR");
                    Label lblServiceBook = (Label)row.FindControl("lblServiceBook");

                    btnSubmit.Text = "Update";
                    btnClear.Text = "Cancel";
                    pnlupload.Visible = false;
                    rfvupload.Enabled = false;
                    ViewState["rid"] = e.CommandArgument.ToString();
                    txtDOB.Text = lblDOB.Text;
                    txtDOR.Text = lblDOR.Text;
                    ddlEmployye_Name.SelectedValue = lblEmp_ID.Text;
                    ddlDepartment.SelectedValue = lblDepartment_ID.Text;
                    ddlOffice.SelectedValue = lblCurrentPosting.Text;
                    ddlDesignation.SelectedValue = lblDesignation_ID.Text;
                    ViewState["listcopy"] = lblServiceBook.Text;

                    if (lblServiceBook.Text != "")
                    {
                        hypListCopy.Visible = true;
                        hypListCopy.NavigateUrl = "../HR/UploadDoc/Promotion_Details/" + lblServiceBook.Text;
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
                    ds = objdb.ByProcedure("Sp_tblEmpPromotionHistory",
                                                new string[] { "flag", "EmpPromotionHistory_id", "CreatedBy", "PageName", "Remark", "ipaddress" },
                                                new string[] { "3", e.CommandArgument.ToString(), objdb.createdBy(), Path.GetFileName(Request.Url.AbsolutePath), "Promotion List Record Deleted", objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        ds.Clear();
                        GetPromotionList();
                        lblError.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Deleted Successfully!");
                    }
                    else
                    {
                        ds.Clear();
                        GetPromotionList();
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
    protected void ddlEmployye_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        lblError.Text = string.Empty;
        GetEmpDetailsByEmp_ID();
        GetDatatableHeaderDesign();
    }
}
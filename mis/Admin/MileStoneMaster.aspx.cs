using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
public partial class mis_Admin_MileStoneMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != "" && Session["Emp_ID"] != null)
            {
                if(!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillGrid();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        
    }
    protected void  FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminMileStoneMaster",new string[]{"flag"},new string[]{"6"},"dataset");
            if(ds!= null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridMsg.Text = "No Record Found";

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Document = "";
            string TLDate = "";
            if(txtImpSubject.Text.Trim() =="")
            {
                msg += "Enter Important Subject. \\n ";
            }
            if (txtTlDate.Text.Trim() == "")
            {
                msg += "Select Date. \\n ";
            }
            if (txtOfficerName.Text.Trim() == "")
            {
                msg += "Enter oOfficer Name. \\n ";
            }
            if(txtTlDate.Text!= "")
            {
                TLDate = Convert.ToDateTime(txtTlDate.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                TLDate = "";
            }
            //if (txtImpSubject.Text.Trim() == "")
            //{
            //    msg += "Enter Important Subject. \\n ";
            //}
            if(FileUpload1.HasFile)
            {
                Document = "Upload/" + Guid.NewGuid() + "-" + FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath(Document));
            }
            if(msg == "")
            {
                if(btnSave.Text=="Save")
                {
                    objdb.ByProcedure("SpAdminMileStoneMaster", new string[] { "flag", "MileStone_IsActive", "ImportantSubject", "TLDate", "OfficerName", "FileUpload", "UpdatedBy" }, new string[] { "0", "1", txtImpSubject.Text, TLDate, txtOfficerName.Text, Document, ViewState["Emp_ID"].ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else if(btnSave.Text=="Update")
                {
                    if (FileUpload1.HasFile)
                    {
                        Document = "Upload/" + Guid.NewGuid() + "-" + FileUpload1.FileName;
                        FileUpload1.PostedFile.SaveAs(Server.MapPath(Document));
                    }
                    else 
                    {
                        if (ViewState["Document"].ToString() != "")
                        {
                            Document = ViewState["Document"].ToString();
                        }
                        else
                        {
                            Document = "";

                        }
                        
                    }
                    objdb.ByProcedure("SpAdminMileStoneMaster", new string[] { "flag", "MileStone_ID", "ImportantSubject", "TLDate", "OfficerName", "FileUpload", "UpdatedBy" }, new string[] { "4", ViewState["MileStone_ID"].ToString(), txtImpSubject.Text, TLDate, txtOfficerName.Text, Document, ViewState["Emp_ID"].ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                    btnSave.Text = "Save";
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
    protected void ClearText()
    {
        txtImpSubject.Text = "";
        txtOfficerName.Text = "";
        txtTlDate.Text = "";
        HyperLink1.Visible = false;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["MileStone_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpAdminMileStoneMaster", new string[] { "flag", "MileStone_ID" }, new string[] { "7", ViewState["MileStone_ID"].ToString()}, "dataset");
            if(ds!= null && ds.Tables[0].Rows.Count> 0)
            {
                txtImpSubject.Text = ds.Tables[0].Rows[0]["ImportantSubject"].ToString();
                txtTlDate.Text = ds.Tables[0].Rows[0]["TLDate"].ToString();
                txtOfficerName.Text = ds.Tables[0].Rows[0]["OfficerName"].ToString();
                ViewState["Document"] = ds.Tables[0].Rows[0]["FileUpload"].ToString();
                if(ViewState["Document"].ToString()!= "")
                {
                    HyperLink1.Text = "View";
                    HyperLink1.NavigateUrl = ViewState["Document"].ToString();
                    HyperLink1.Visible = true;
                }
                else
                {
                    HyperLink1.Text = "NA";
                    HyperLink1.Visible = true;

                }
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MileStoneID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpAdminMileStoneMaster", new string[] { "flag", "MileStone_ID" }, new string[] { "8", MileStoneID }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
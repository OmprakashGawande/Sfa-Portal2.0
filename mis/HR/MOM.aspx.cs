using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
public partial class mis_Admin_MOM : System.Web.UI.Page
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
                if (!IsPostBack)
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRMom", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                
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
            string MOMDate = "";
            int status = 0;
            if (txtMeetingNo.Text.Trim() == "")
            {
                msg += "Enter Meeting No. \\n ";
            }
            if (txtMeetingDate.Text.Trim() == "")
            {
                msg += "Select Date. \\n ";
            }
            if (txtMeetingDate.Text != "")
            {
                MOMDate = Convert.ToDateTime(txtMeetingDate.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                MOMDate = "";
            }
            //if (txtImpSubject.Text.Trim() == "")
            //{
            //    msg += "Enter Important Subject. \\n ";
            //}
            if (FileUpload1.HasFile)
            {
                Document = "MomDocs/" + Guid.NewGuid() + "-" + FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath(Document));
            }
            if (msg == "")
            {
                ds = objdb.ByProcedure("SpHRMom", new string[] { "flag", "Mom_No" }, new string[] {"4",txtMeetingNo.Text }, "dataset");
                if(ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (btnSave.Text == "Save" && status == 0)
                {
                    objdb.ByProcedure("SpHRMom", new string[] { "flag", "Mom_No", "Mom_Date", "Mom_FileUpload", "UpdatedBy", "IsActive" }, new string[] { "0",txtMeetingNo.Text,MOMDate, Document, ViewState["Emp_ID"].ToString(),"1" }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Meeting No Already Exist.')", true);
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
        txtMeetingNo.Text = "";
        txtMeetingDate.Text = "";
        HyperLink1.Visible = false;
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Mom_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpHRMom", new string[] { "flag", "Mom_ID" }, new string[] { "3", Mom_ID }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
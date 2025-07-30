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


public partial class mis_HR_HRApplyTour : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    FillDropdown();
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        try
        {
            ddlTourType.ClearSelection();
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtTourRemark.Text = "";
            ddlEmployeeName.ClearSelection();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {

            ddlTourType.Items.Insert(0, new ListItem("Select", "0"));
            //ddlTourType.Items.Insert(1, new ListItem("Warehouse Audit", "1"));
            ddlTourType.Items.Insert(1, new ListItem("Official Tour", "2"));
            ddlTourType.Items.Insert(2, new ListItem("Other", "3"));
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID", "Emp_ID" }, new string[] { "35", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployeeName.DataSource = ds;
                ddlEmployeeName.DataTextField = "Emp_Name";
                ddlEmployeeName.DataValueField = "Emp_ID";
                ddlEmployeeName.DataBind();
                ddlEmployeeName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void InsertData()
    {
        try
        {
            int TotalDays = 0;
            string DocPath = "";
            string msg1 = "";
            string Status = "0";
            if (EmpTourDoc.HasFile)
            {
                DocPath = "../HR/UploadDoc/TourDoc/" + Guid.NewGuid() + "-" + EmpTourDoc.FileName;
                EmpTourDoc.PostedFile.SaveAs(Server.MapPath(DocPath));
            }

            if (msg1 == "")
            {
                ds = objdb.ByProcedure("SpHRTourApplication", new string[] { "flag", "FromDate", "ToDate", "Emp_ID" }, new string[] { "9", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString() }, "dataset");
                if(ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Status = ds.Tables[0].Rows[0]["Status"].ToString();
                }
                if(Status == "0")
                {
                    string d11 = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                    string d22 = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
                    DateTime d1 = DateTime.Parse(d11);
                    DateTime d2 = DateTime.Parse(d22);
                    TotalDays = ((d2 - d1).Days);
                    TotalDays = TotalDays + 1;
                    objdb.ByProcedure("SpHRTourApplication", new string[] { "flag", "Emp_ID", "Office_ID", "TourType", "TourFromDate", "TourToDate", "TourDay", "TourApproveAuthority", "TourDescription", "TourDocument", "TourStatus", "IsActive" },
                          new string[] { "0", ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString(), ddlTourType.SelectedItem.Text, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), TotalDays.ToString(), ddlEmployeeName.SelectedValue.ToString(), txtTourRemark.Text, DocPath, "Pending", "1" }, "dataset");
                    lblMsg.Text = "<div class='alert alert-success alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><h4><i class='icon fa fa-check'></i> Success</h4>आपके द्वारा किया हुआ दौरे का आवेदन सफल रहा |</div>";
                    ClearText();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Already Applied |');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('सम्बंधित दस्तावेज संलग्न करें |');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void applytour_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1;
            lblMsg.Text = "";
            string msg = "";       
            if (ddlTourType.SelectedIndex == 0)
            {
                msg += "कृपया दौरे का प्रकार चुनें |<br/>";
            }
            if (txtFromDate.Text == "")
            {
                msg += "कृपया दिनांक से चुने | <br/>";
            }
            if (txtToDate.Text == "")
            {
                msg += "कृपया दिनांक तक चुने | <br/>";
            }
            if(ddlEmployeeName.SelectedIndex == 0)
            {
                msg += "कृपया कर्मचारी का नाम चुने | <br/>";
            }
            if (txtTourRemark.Text == "")
            {
                msg += "कृपया दौरे का विवरण  भरें | <br/>";
            }
            if (msg == "")
            {
                InsertData();
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
}
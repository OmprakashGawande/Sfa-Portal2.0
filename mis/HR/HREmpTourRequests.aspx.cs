using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

public partial class mis_HR_HREmpTourRequests : System.Web.UI.Page
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
                    string sMonth = DateTime.Now.ToString("MM");
                    ddlMonth.SelectedValue = sMonth.ToString();
                    txtReason.Attributes.Add("readonly", "readonly");
                    FillDropdown();
                    //FillGrid();
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

    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select Office", "0"));
            }
            ddlFinancialYear.SelectedValue = DateTime.Now.Year.ToString(); ;

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
            lblMsg2.Text = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRTourApplication", new string[] { "flag", "Office_ID", "TourApproveAuthority", "Year", "MonthNo" },
                  new string[] { "2", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), ddlFinancialYear.SelectedValue.ToString(),ddlMonth.SelectedValue.ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                // lblMsg2.Text = "No Record Found...";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtOrderDate.Text = "";
            txtOrderNo.Text = "";
            txtReason.Text = "";
            txtRemark.Text = "";
            ViewState["TourId"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRTourApplication", new string[] { "flag", "TourId" },
                  new string[] { "3", ViewState["TourId"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                DetailsView1.DataSource = ds;
                DetailsView1.DataBind();

                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;


                txtReason.Text = ds.Tables[0].Rows[0]["TourDescription"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
                ViewState["TourType"] = ds.Tables[0].Rows[0]["TourType"].ToString();
                ViewState["EmpID"] = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1;
            string DocPath = "";
            string OrderDate = "";
            lblMsg.Text = "";
            string msg = "";            
            if (ddlStatus.SelectedIndex == 0)
            {
                msg += "Select Status. <br/>";
            }
            //if (txtRemark.Text == "")
            //{
            //    msg += "Enter Remark. <br/>";
            //}
            if (txtOrderDate.Text != "")
            {
                OrderDate = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                OrderDate = "";
            }
             
            if (ApprovalDoc.HasFile)
            {
                DocPath = "../HR/UploadDoc/TourApproveDoc/" + Guid.NewGuid() + "-" + ApprovalDoc.FileName;
                ApprovalDoc.PostedFile.SaveAs(Server.MapPath(DocPath));
            }
            objdb.ByProcedure("SpHRTourApplication", new string[] { "flag", "TourId", "TourStatus", "RemarkByApprovalAuth", "TourApprovalOrderNo", "TourApprovalOrderDate", "TourApprovalOrderFile" },
            new string[] { "4", ViewState["TourId"].ToString(), ddlStatus.SelectedValue.ToString(), txtRemark.Text, txtOrderNo.Text, OrderDate, DocPath }, "datatset");


            /***********SEND SMS TO EMPLOYEE****************/
            //DataSet ds3;
            //ds3 = objdb.ByProcedure("SpHRLeaveApplication",
            //    new string[] { "flag", "LeaveId" },
            //    new string[] { "31", ViewState["LeaveId"].ToString() }, "datatset");
            //if (ds3.Tables[0].Rows.Count > 0)
            //{
            //    string EmpMobileNo = ds3.Tables[0].Rows[0]["Emp_MobileNo"].ToString();
            //    string EmpName = ds3.Tables[0].Rows[0]["Emp_Name"].ToString();
            //    string LeaveType = ds3.Tables[0].Rows[0]["Leave_Type"].ToString();
            //    string LeaveFromDate = ds3.Tables[0].Rows[0]["LeaveFromDate"].ToString();
            //    string LeaveToDate = ds3.Tables[0].Rows[0]["LeaveToDate"].ToString();
            //    if (EmpMobileNo != "9893098930" && EmpMobileNo != "0000000000" && EmpMobileNo != null)
            //    {
            //        string Empmessage = "";
            //        if (TotalTakenLeave.ToString() == "0.5")
            //        {
            //            Empmessage = "Your leave request for half day of " + LeaveFromDate + "  has been " + ddlStatus.SelectedItem.Text + ".";
            //        }
            //        else
            //        {
            //            Empmessage = "Your leave request from " + LeaveFromDate + " to " + LeaveToDate + " has been " + ddlStatus.SelectedItem.Text + ".";
            //        }
            //        //string Empmessage = "Your leave request from " + LeaveFromDate + " to " + LeaveToDate + " has been " + ddlStatus.SelectedItem.Text + ".";
            //        string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=3597C1493C124F&type=unicode&routeid=2&contacts=" + EmpMobileNo + "&senderid=MPAGRO&msg=" + Empmessage;
            //        HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
            //        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //        Stream stream = response.GetResponseStream();
            //    }

            //}
            /***********************************************/


            FillGrid();



            //lblMsg.Text = "<div class='alert alert-success alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><h4><i class='icon fa fa-check'></i> Success</h4>Leave has been " + ddlStatus.SelectedItem.Text + " successfully. <a href='HRLeaveAppliedByStaff.aspx'>Click Here</a> to view " + ddlStatus.SelectedItem.Text + " leave requests..</div>";
            ddlStatus.ClearSelection();
            txtRemark.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
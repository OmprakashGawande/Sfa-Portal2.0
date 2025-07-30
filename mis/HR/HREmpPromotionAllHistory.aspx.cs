using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpPromotionAllHistory : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                //GetPromotionList();
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                FillGrid();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
        //try
        //{
        //    if (!IsPostBack)
        //    {

        //        if (Session["Emp_ID"] != null)
        //        {
        //            lblMsg.Text = "";
        //            ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
        //            FillGrid();
        //        }
        //        else
        //        {
        //            Response.Redirect("~/mis/Login.aspx");
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        //}
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHREmpPromotion", new string[] { "flag" }, new string[] { "10" }, "dataset");
            if (ds.Tables[0].Rows.Count >= 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GetDatatableHeaderDesign();
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }
    //private void GetPromotionList()
    //{
    //    try
    //    {
    //        lblError.Text = "";

    //        ds = objdb.ByProcedure("Sp_tblEmpPromotionHistory",
    //                 new string[] { "flag" },
    //                 new string[] { "0" }, "dataset");

    //        if (ds.Tables[0].Rows.Count != 0)
    //        {
    //            GridView1.DataSource = ds.Tables[0];
    //            GridView1.DataBind();
    //            GetDatatableHeaderDesign();
    //        }
    //        else
    //        {
    //            ds.Clear();
    //            GridView1.DataSource = ds;
    //            GridView1.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
    //    }
    //}
}
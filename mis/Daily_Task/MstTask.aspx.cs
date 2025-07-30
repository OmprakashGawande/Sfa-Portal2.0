using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Microsoft.Reporting.WebForms;

public partial class mis_Daily_Task_MstTask : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (objdb.createdBy() != null && objdb.Office_ID() != null && Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                DateTime dd = DateTime.Now;
                fillProjects();
                BindGrid();
            }
        }
        else
        {
            objdb.redirectToHome();
        }

    }
    private void BindGrid()
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("Project_ID");
        dt.Columns.Add("Project_Name_Eng");
        dt.Columns.Add("IsActive");

        dt.Rows.Add(1, "Project A", true);

        gridProject.DataSource = dt;
        gridProject.DataBind();

    }
    protected void fillProjects()
    {
        try
        {
            ddlProject.Items.Clear();
            ds1 = objdb.ByProcedure("USP_Daily_Task_GetAllProjects", new string[] { }, new string[] { }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlProject.DataValueField = "Project_ID";
                        ddlProject.DataTextField = "Project_Name";
                        ddlProject.DataSource = ds1;
                        ddlProject.DataBind();
                        ddlProject.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        ddlProject.Items.Insert(0, new ListItem("No Record Found", "0"));
                    }
                }
                else
                {
                    ddlProject.Items.Insert(0, new ListItem("No Record Found", "0"));
                }
            }
            else
            {
                ddlProject.Items.Insert(0, new ListItem("No Record Found", "0"));
            }
            if (ds1 != null) ds1.Dispose();

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7: " + ex.Message.ToString());
        }
    }
    protected void btnAssignTask_Click(object sender, EventArgs e)
    {
        var button = (Button)sender;
        string projectId = button.CommandArgument;


        txtTaskName.Text = string.Empty;
        txtTaskCode.Text = "DEFAULT123";

        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        // Show the modal using JavaScript
        string script = "openModal();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAssignTaskModal", script, true);
    }


    public void Get(int age) {

        if (age >= 18)
        {
            Console.WriteLine("Eligible");

        }
        else if (age < 18)
        {

            Console.WriteLine("You are a child");
        }
        else {
            Console.WriteLine("Get Out");
        }

    
        
    
    }



}
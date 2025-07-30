using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
public partial class mis_HR_HREmpRetirementList : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != "" && Session["Emp_ID"] != null)
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    ds = objdb.ByProcedure("SpHREmpRetirement", new string[] { "flag" }, new string[] { "1" }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            HyperLink Documents = (HyperLink)row.FindControl("Documents");
                            if (string.IsNullOrEmpty(Documents.Text.Trim()))
                            {
                                string path3 = Path.Combine(Server.MapPath("~/mis/HR/"), Documents.Text.Trim());
                                if (File.Exists(path3))
                                {
                                    Documents.Visible = true;
                                }
                                else
                                {
                                    Documents.Visible = false;
                                }

                            }
                            else
                            {
                                Documents.Visible = false;
                            }


                        }
                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                        GridView1.UseAccessibleHeader = true;

                    }

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
}
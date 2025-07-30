using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_MainMasterNew : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["IsMainPage"] != null)
            {
                Session["Module_Id"] = null;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
            }

            ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
            //spnUsername.InnerHtml = Session["UserName"].ToString() + "<br/><small>" + Session["Designation_Name"].ToString() + "</small>";

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            string profileImage = baseUrl + "mis/HR/" + Session["Emp_ProfileImage"].ToString();

            // Start building sidebar
            string sidebarHtml = @"
<div class='sidebar-wrapper' data-layout='stroke-svg'>
    <div class='logo-wrapper'>
        <a href='index.html'>
            <img class='img-fluid' src='../assets/images/logo/logo.png' alt=''>
        </a>
        <div class='back-btn'><i class='fa fa-angle-left'></i></div>

        <div class='toggle-sidebar'><i class='status_toggle middle sidebar-toggle' data-feather='grid'></i></div>
    </div>
    <div class='logo-icon-wrapper'>
        <a href='index.html'>
            <img class='img-fluid' src='../assets/images/logo/logo-icon.png' alt=''>
        </a>
    </div>
    <nav class='sidebar-main'>
        <div class='left-arrow' id='left-arrow'><i data-feather='arrow-left'></i></div>
        <div id='sidebar-menu'>
            <ul class='sidebar-links' id='simple-bar'>
                <li class='back-btn'>
                    <a href='index.html'>
                        <img class='img-fluid' src='../assets/images/logo/logo-icon.png' alt=''>
                    </a>
                    <div class='mobile-back text-end'>
                        <span>Back</span><i class='fa fa-angle-right ps-2'></i>
                    </div>
                </li>
                <li class='pin-title sidebar-main-title'>
                    <div><h6>Pinned</h6></div>
                </li>
                <li class='sidebar-main-title'>
                    <div><h6 class='lan-1'>General</h6></div>
                </li>
                <li class='sidebar-list'>
                    <a class='sidebar-link sidebar-title' href='../NewTheme/Home.aspx?IsMainPage=1'>
                        <svg class='stroke-icon'>
                            <use href='../assets/svg/icon-sprite.svg#stroke-home'></use>
                        </svg>
                        <svg class='fill-icon'>
                            <use href='../assets/svg/icon-sprite.svg#fill-home'></use>
                        </svg>
                        <span class='lan-3'>Dashboard</span>
                    </a>
                </li>";

            // Dynamic Module-Based Sidebar
            if (Request.QueryString["Module_ID"] != null)
            {
                Session["Module_Id"] = Request.QueryString["Module_ID"].ToString();
            }

            if (Session["Module_Id"] == null)
            {
                DataTable dtAccess = Session["AccessModule"] as DataTable;
                foreach (DataRow row in dtAccess.Rows)
                {
                    sidebarHtml += @"
                <li class='sidebar-list'>
                    <a class='sidebar-link sidebar-title' href='../NewTheme/Home.aspx?Module_Id=" + row["Module_ID"] + @"'>
                        <i class='fa fa-hand-o-right'></i>
                        <span>" + row["Module_Name"] + @"</span>
                    </a>
                </li>";
                }
            }
            else
            {
                DataTable dtAccessForm = Session["AccessForm"] as DataTable;
                DataView dv = dtAccessForm.DefaultView;
                dv.RowFilter = "Module_ID = '" + Session["Module_Id"].ToString() + "'";
                DataTable dtAccess = dv.ToTable();

                string currentMenu = "";
                for (int i = 0; i < dtAccess.Rows.Count; i++)
                {
                    string menuName = dtAccess.Rows[i]["Menu_Name"].ToString();
                    string menuIcon = dtAccess.Rows[i]["Menu_Icon"].ToString();

                    if (menuName != currentMenu)
                    {
                        currentMenu = menuName;

                        sidebarHtml += @"
                    <li class='sidebar-list'>
                        <a class='sidebar-link sidebar-title' href='#'>" + menuIcon + @"
                            <span>" + menuName + @"</span>
                        </a>
                        <ul class='sidebar-submenu'>";

                        while (i < dtAccess.Rows.Count && dtAccess.Rows[i]["Menu_Name"].ToString() == currentMenu)
                        {
                            sidebarHtml += @"
                            <li>
                                <a href='" + dtAccess.Rows[i]["Form_Path"] + @"'>" + dtAccess.Rows[i]["Form_Name"] + @"</a>
                            </li>";
                            i++;
                        }
                        i--; // step back after overshoot

                        sidebarHtml += @"
                        </ul>
                    </li>";
                    }
                }
            }

            // Footer links
            sidebarHtml += @"
                <li class='sidebar-list'>
                    <a class='sidebar-link sidebar-title' href='../Admin/Change_UserPassword.aspx'>
                        <i class='fa fa-key'></i><span>Change Password</span>
                    </a>
                </li>
                <li class='sidebar-list'>
                    <a class='sidebar-link sidebar-title' href='../Login.aspx'>
                        <i class='fa fa-power-off'></i><span>Logout</span>
                    </a>
                </li>";

            // Close sidebar
            sidebarHtml += @"
            </ul>
            <div class='right-arrow' id='right-arrow'><i data-feather='arrow-right'></i></div>
        </div>
    </nav>
</div>";

            // Render final sidebar HTML
            Navigation.InnerHtml = sidebarHtml;
        }
    }


    public void GenerateBreadcrumb(string currentPath)
    {
        try
        {
            string section = "", page = "";
            DataTable dtAccessForm = Session["AccessForm"] as DataTable;

            if (dtAccessForm != null)
            {
                foreach (DataRow row in dtAccessForm.Rows)
                {
                    if (row["Form_Path"].ToString().ToLower().Contains(currentPath.ToLower()))
                    {
                        section = row["Menu_Name"].ToString();
                        page = row["Form_Name"].ToString();
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(page))
            {
                page = "Unknown Page";
            }

            string breadcrumbHtml = @"
        <div class='container-fluid'>
            <div class='page-title'>
                <div class='row'>
                    <div class='col-6'>
                        <h4>" + page + @"</h4>
                    </div>
                    <div class='col-6'>
                        <ol class='breadcrumb'>
                            <li class='breadcrumb-item'>
                                <a href='../NewTheme/Home.aspx?IsMainPage=1'>
                                    <svg class='stroke-icon'>
                                        <use href='../assets/svg/icon-sprite.svg#stroke-home'></use>
                                    </svg>
                                </a>
                            </li>";

            if (!string.IsNullOrEmpty(section))
            {
                breadcrumbHtml += "<li class='breadcrumb-item'>" + section + "</li>";
            }

            breadcrumbHtml += "<li class='breadcrumb-item active'>" + page + "</li>";
            breadcrumbHtml += @"
                        </ol>
                    </div>
                </div>
            </div>
        </div>";

            breadcrumbContainer.InnerHtml = breadcrumbHtml;
        }
        catch
        {
            // fallback if anything fails
            breadcrumbContainer.InnerHtml = "<h4>Page</h4>";
        }
    }


}

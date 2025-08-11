using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
public partial class MainMaster : System.Web.UI.MasterPage
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
            if (Session["Emp_ProfileImage"].ToString() == "")
            {
                imgProfile.ImageUrl = "assets/images/user/user.png";
            }
            else
            {
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                imgProfile.ImageUrl = baseUrl + "mis/HR/" + Session["Emp_ProfileImage"].ToString() ?? "assets/images/user/user.png";
            }
            lblUserName.Text = Session["Emp_Name"].ToString() ?? "Guest";
            lblName2.Text = Session["Emp_Name"].ToString() ?? "Guest";
            lblUserRole.Text = Session["Designation_Name"].ToString() ?? "User";

            // Start building sidebar
            string sidebarHtml = @"
<div class='sidebar-wrapper' data-layout='stroke-svg'>
    <div class='logo-wrapper'>
        <a href='../Dashboard/Home.aspx'>
            <img class='img-fluid' src='../assets/images/logo/logo.png' alt=''>
        </a>
        <div class='back-btn'><i class='fa fa-angle-left'></i></div>

        <div class='toggle-sidebar'><i class='status_toggle middle sidebar-toggle' data-feather='grid'></i></div>
    </div>
    <div class='logo-icon-wrapper'>
        <a href='../Dashboard/Home.aspx'>
            <img class='img-fluid' src='../assets/images/logo/logo-icon.png' alt=''>
        </a>
    </div>
    <nav class='sidebar-main'>
        <div class='left-arrow' id='left-arrow'><i data-feather='arrow-left'></i></div>
        <div id='sidebar-menu'>
            <ul class='sidebar-links' id='simple-bar'>
                <li class='back-btn'>
                    <a href='../Dashboard/Home.aspx'>
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
                </li>
                 <li class='sidebar-list'><i class='fa fa-thumb-tack'></i><a class='sidebar-link sidebar-title link-nav' href='../Dashboard/Home.aspx?IsMainPage=1'>
                    <svg class='stroke-icon'>
                      <use href='../assets/svg/icon-sprite.svg#stroke-home'></use>
                    </svg>
                    <svg class='fill-icon'>
                      <use href='../assets/svg/icon-sprite.svg#fill-home'></use>
                    </svg><span>Dashboard</span>
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
                <li class='sidebar-list'><i class='fa fa-thumb-tack'> </i>
                    <a class='sidebar-link sidebar-title' href='../Dashboard/Home.aspx?Module_Id=" + row["Module_ID"] + @"'>
                          <svg class='stroke-icon'>
                      <use href='../assets/svg/icon-sprite.svg#stroke-others'></use>
                    </svg>
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
                    <li class='sidebar-list'><i class='fa fa-thumb-tack'> </i>
                        <a class='sidebar-link sidebar-title text-white' href='#'>
 <svg class='stroke-icon'>
                      <use href='../assets/svg/icon-sprite.svg#stroke-form'></use>
                    </svg>
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
                <li class='sidebar-list'><i class='fa fa-thumb-tack'> </i>
                    <a class='sidebar-link sidebar-title' href='../Admin/Change_UserPassword.aspx'>
 <svg class='stroke-icon'>
                      <use href='../assets/svg/icon-sprite.svg#stroke-others'></use>
                    </svg>
                        <span>Change Password</span>
                    </a>
                </li>
                <li class='sidebar-list'><i class='fa fa-thumb-tack'> </i>
                    <a class='sidebar-link sidebar-title' href='../Login.aspx'>
 <svg class='stroke-icon'>
                      <use href='../assets/svg/icon-sprite.svg#stroke-others'></use>
                    </svg>
                        <span>Logout</span>
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
                                <a href='../Dashboard/Home.aspx?IsMainPage=1'>
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
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (!IsPostBack)
    //        {
    //            //UserAccess();
    //            if (Request.QueryString["IsMainPage"] != null)
    //            {
    //                Session["Module_Id"] = null;
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
    //            }
    //            ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
    //            spnUsername.InnerHtml = Session["UserName"].ToString() + "<br/><small>" + Session["Designation_Name"].ToString() + "</small>";
    //            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
    //            Navigation.InnerHtml = "<div class='user-panel' style='  background-color: black;'><div class='pull-left image'><img src='" + baseUrl + "mis/HR/" + Session["Emp_ProfileImage"].ToString() + "' class='img-circle' alt='User Image'></div><div class='pull-left info'><p style='font-size:12px;'>" + Session["Emp_Name"].ToString() + "</p><a href='#'><i class='fa fa-circle text-success'></i> Online</a></div></div>";
    //            Navigation.InnerHtml += "<ul class='sidebar-menu' data-widget='tree'>";
    //            Navigation.InnerHtml += "<li class='header' style='font-weight: 600;text-transform: uppercase;letter-spacing: 2px;  background-image: linear-gradient(45deg, red, black);'>" + Session["Office_Name"].ToString() + "</li>";
    //            Navigation.InnerHtml += "<li><hr style='margin: 0' /></li>";
    //            Navigation.InnerHtml += "<li><a href='../Dashboard/Home.aspx?IsMainPage=1' class='Aselect1'><i class='fa fa-home'></i><span>&nbsp;Main Page</span></a></li>";
    //            if (Request.QueryString["Module_ID"] != null)
    //            {
    //                Session["Module_Id"] = Request.QueryString["Module_ID"].ToString();
    //            }
    //            if (Session["Module_Id"] == null)
    //            {
    //                DataTable dtAccess = Session["AccessModule"] as DataTable;
    //                for (int i = 0; i < dtAccess.Rows.Count; i++)
    //                {
    //                    Navigation.InnerHtml += "<li><a href='../Dashboard/Home.aspx?Module_Id=" + dtAccess.Rows[i]["Module_ID"].ToString() + "' class='Aselect1'><i class='fa fa fa-hand-o-right'></i>&nbsp;" + "<span>" + dtAccess.Rows[i]["Module_Name"].ToString() + "</span></a></li>";
    //                }
    //            }
    //            else
    //            {
    //                DataTable dtAccessForm = Session["AccessForm"] as DataTable;
    //                DataView dv = new DataView();
    //                dv = dtAccessForm.DefaultView;
    //                dv.RowFilter = "Module_ID = '" + Session["Module_Id"].ToString() + "'";
    //                DataTable dtAccess = dv.ToTable();
    //                string Menu_Name = "";
    //                string NavigationLi = "";
    //                int IsMainPage = 0;
    //                for (int i = 0; i < dtAccess.Rows.Count; i++)
    //                {
    //                    NavigationLi = "";
    //                    IsMainPage = 0;
    //                    Menu_Name = dtAccess.Rows[i]["Menu_Name"].ToString();

    //                    while (dtAccess.Rows[i]["Menu_Name"].ToString() == Menu_Name)
    //                    {
    //                        IsMainPage++;
    //                        NavigationLi += "<li><a href='" + dtAccess.Rows[i]["Form_Path"].ToString() + "' class='Aselect1'><i class='fa fa-hand-o-right'></i>" + "<span>" + dtAccess.Rows[i]["Form_Name"].ToString() + "</span>" + "</a></li>";
    //                        i++;
    //                        if (dtAccess.Rows.Count == i)
    //                        {
    //                            break;
    //                        }
    //                    }
    //                    i--;
    //                    Navigation.InnerHtml += "<li class='treeview'>";
    //                    Navigation.InnerHtml += "<a href='#'>" + dtAccess.Rows[i]["Menu_Icon"].ToString() + "<span>" + dtAccess.Rows[i]["Menu_Name"].ToString() + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>";
    //                    Navigation.InnerHtml += "<ul class='treeview-menu' style='display: none;'>";
    //                    Navigation.InnerHtml += NavigationLi;
    //                    Navigation.InnerHtml += "</ul>";
    //                    Navigation.InnerHtml += "</li>";
    //                }
    //            }
    //            Navigation.InnerHtml += "<li><a href='../Admin/Change_UserPassword.aspx' class='Aselect1'><i class='fa fa-key'></i><span>Change Password</span></a></li>";
    //            Navigation.InnerHtml += "<li><a href='../Login.aspx' class='Aselect1'><i class='fa fa-power-off'></i><span>Logout</span></a></li>";
    //            Navigation.InnerHtml += "</ul>";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Redirect("~/mis/Login.aspx");
    //    }
    //}
    public void UserAccess()
    {
        if (Session["AccessForm"] != null)
        {
            if (Session["Module_Id"] == null)
            {
                return;
            }
            DataSet FormPath = new DataSet();
            int k = 0;
            string path = ".." + HttpContext.Current.Request.Url.AbsolutePath;
            string HomePage = "../mis/Dashboard/Home.aspx";
            DataTable dtAccessForm = Session["AccessForm"] as DataTable;
            DataView dv = new DataView();
            dv = dtAccessForm.DefaultView;
            dv.RowFilter = "Module_ID = '" + Session["Module_Id"].ToString() + "'";
            FormPath.Tables.Add(dv.ToTable());
            if (path == HomePage)
            {
                k = 1;
            }
            else if (FormPath != null && FormPath.Tables.Count > 0 && FormPath.Tables[0].Rows.Count > 0)
            {
                int Pathcount = FormPath.Tables[0].Rows.Count;
                for (int i = 0; i < Pathcount; i++)
                {
                    string FormAdd = FormPath.Tables[0].Rows[i]["Form_Path"].ToString();
                    FormAdd = "../mis" + FormAdd.Remove(0, 2); ;
                    if (path == FormAdd)
                    {
                        k = 1;
                        break;
                    }
                }
            }
            if (k == 0)
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
}

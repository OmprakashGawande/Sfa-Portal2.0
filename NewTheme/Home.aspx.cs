using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewTheme_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string currentPath = Request.Url.AbsolutePath.Substring(Request.Url.AbsolutePath.LastIndexOf("/") + 1);
        ((mis_MainMasterNew)this.Master).GenerateBreadcrumb(currentPath);
    }
}
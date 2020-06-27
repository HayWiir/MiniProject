using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    

    

    protected void Page_Load(object sender, EventArgs e)
    {
        //ThemeDDL.SelectedIndex = 0;
        
  

        try
        {
            HttpCookie userCookie = Request.Cookies["UserCookie"];
            if (userCookie != null)
            {
                Label1.Text = "Hello " + userCookie["Name"].ToString().ToUpper();
            }
            else
            {
                
            }
        }
        catch (Exception ex)
        {
         //   Response.Write(ex.Message);
        }
        System.Diagnostics.Debug.WriteLine("SomeTextmaster");
    }

    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        HttpCookie userCookie = new HttpCookie("UserCookie");
        userCookie.Expires = DateTime.Now.AddDays(-1);
        Response.Cookies.Add(userCookie);

        Response.Redirect("Login.aspx");

    }

    //protected void ThemeDDL_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    System.Diagnostics.Debug.WriteLine("SomeText");
    //    System.Diagnostics.Debug.WriteLine("SomeText");
    //    Session["theme"] = ThemeDDL.SelectedItem.Value.ToString();
    //    Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
    //}

    protected void ToggleThemeButton_Click(object sender, EventArgs e)
    {
        string theme = (string)Session["theme"];
        if (theme == null)
            return;

        if (HttpContext.Current.Request.Url.AbsolutePath.Contains("Restaurant.aspx"))
            Session["ResToggle" + Request.QueryString["restaurant_id"]] = true;

        System.Diagnostics.Debug.WriteLine("Themetoggle");
        theme = (theme == "Theme1") ? "Theme2" : "Theme1";
        Session["theme"] = theme;
        Server.Transfer(Request.RawUrl);
    }
}

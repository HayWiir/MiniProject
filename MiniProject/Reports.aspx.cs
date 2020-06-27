using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        string theme = (string)Session["theme"];

        if ((theme != null) && (theme.Length != 0))
        {

            Page.Theme = theme;
        }
        else
        {
            Page.Theme = "Theme1";
            Session["theme"] = "Theme1";
            System.Diagnostics.Debug.WriteLine("SomeTextLogin");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.DefaultButton = SearchButton.UniqueID;
        HyperLink hl = (HyperLink)Master.FindControl("Home");
        hl.NavigateUrl = "~/AdminLanding.aspx";
        HttpCookie userCookie = Request.Cookies["UserCookie"];
        try
        {
            if (userCookie != null)
            {
                //Label1.Text = "Hello " + userCookie["Name"].ToString();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void ResView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "GoTo") return;
        int res_id = Convert.ToInt32(e.CommandArgument);

        Response.Redirect("Restaurant.aspx?restaurant_id=" + res_id.ToString());
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        string name = SResName.Text ?? "";
        string cuisine = SResCus.Text ?? "";
        string location = SResLoc.Text ?? "";
        string rate = (SResRate.Text == "") ? "0" : SResRate.Text;

        string qry = "SELECT * FROM restaurants WHERE restaurant_name LIKE '%{0}%' AND restaurant_cusine LIKE '%{1}%' AND restaurant_location LIKE '%{2}%' AND restaurant_rating >= {3}";
        qry = string.Format(qry, name, cuisine, location, rate);


        ResData.SelectCommand = qry;
        if (!ResView.Visible) ResView.Visible = true;
        ResView.DataBind();



    }

    protected void ResetButton_Click(object sender, EventArgs e)
    {
        SResName.Text = SResCus.Text = SResLoc.Text = SResRate.Text = "";
        SearchButton_Click(sender, e);

    }
}

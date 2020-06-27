using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminLanding : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["datacon"].ToString());

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
        this.Form.DefaultButton = Button2.UniqueID;
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

    

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;

        string insert_qry = "INSERT INTO restaurants(restaurant_name, restaurant_cusine, restaurant_location) values(@resname, @rescuisine, @resloc)";

        try
        {
            SqlCommand cmd = new SqlCommand(insert_qry, con);
            cmd.Parameters.AddWithValue("@resname", ResNameBox.Text);
            cmd.Parameters.AddWithValue("@rescuisine", ResCuisineBox.Text);
            cmd.Parameters.AddWithValue("@resloc", ResLocBox.Text);
            con.Open();
            int ret = cmd.ExecuteNonQuery();
            if (ret != 0)
            {
                Label2.Text = "Restaurant Added!";
                Label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

        }

        ResNameBox.Text = "";
        ResCuisineBox.Text = "";
        ResLocBox.Text = "";

        ResView.DataBind();



    }

    protected void ResView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "GoTo") return;
        int res_id = Convert.ToInt32(e.CommandArgument);

        Response.Redirect("Restaurant.aspx?restaurant_id=" + res_id.ToString());
    }

 

    protected void AppCommGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        int comm_id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName=="Approve")
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE user_comments SET is_approved=1 WHERE comment_id=@comm_id", con);
                cmd.Parameters.AddWithValue("@comm_id", comm_id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

            }

        }

        else if (e.CommandName == "Reject")
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM user_comments WHERE comment_id=@comm_id and is_approved=0", con);
                cmd.Parameters.AddWithValue("@comm_id", comm_id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

            }

        }


        AppCommGrid.DataBind();
    }

    protected void ReportsButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Reports.aspx");
    }

   
}
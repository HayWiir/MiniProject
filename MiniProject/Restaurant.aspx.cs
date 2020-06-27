using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Restaurant : System.Web.UI.Page
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
        this.Form.DefaultButton = CommentButton.UniqueID;

        
        HyperLink hl = (HyperLink)Master.FindControl("Home");
        HttpCookie userCookie = Request.Cookies["UserCookie"];

        try
        {
            if (userCookie != null)
            {
                string userType = userCookie["UserType"];
                if (userType == "admin")
                {
                    hl.NavigateUrl = "~/AdminLanding.aspx";
                }
                else
                {
                    hl.NavigateUrl = "~/UserLanding.aspx";
                    RatingPanel.Visible = true;
                    CommentPanel.Visible = true;
                }
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

        string view_string = "ResView";

        if (Request.QueryString["restaurant_id"]!=null)
        {
            view_string += Request.QueryString["restaurant_id"];

            Restaurant_Load(Request.QueryString["restaurant_id"]);
        }

        if (!IsPostBack)
        {
            int page_views;
            if (Application[view_string] == null)
            {
                page_views = 1;
            }
            else
            {
                page_views = (int)Application[view_string];
                page_views += 1;
            }

            if(Session["ResToggle" + Request.QueryString["restaurant_id"]]!=null)
            {
                bool is_toggle = (bool)Session["ResToggle" + Request.QueryString["restaurant_id"]];
                if (is_toggle)
                {
                    page_views -= 1;
                    Session["ResToggle" + Request.QueryString["restaurant_id"]] = false;
                }
                
            }

            Application.Lock();
            Application[view_string] = page_views;
            Application.UnLock();

            ViewsLabel.Text = "This page has been viewed " + page_views + " times.";
        }
    }

    private void Restaurant_Load(string res_id)
    {
        
        DataSet res_data = new DataSet();
        string sql = "SELECT * FROM restaurants WHERE restaurant_id=@res_id";

        try
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@res_id", res_id);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(res_data, "data");

            DataRow row = res_data.Tables["data"].Rows[0];
            ResName.Text = row["restaurant_name"].ToString();
            ResCuisine.Text = row["restaurant_cusine"].ToString();
            ResRating.Text = row["restaurant_rating"].ToString();
            ResLocation.Text = row["restaurant_location"].ToString();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }


    }

    protected void RateButton_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["restaurant_id"] == null)
            return;

        HttpCookie userCookie = Request.Cookies["UserCookie"];

        
        if (userCookie!=null)
        {
            int total_raters = 0;
            double curr_rating =0;

            
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants where restaurant_id=@res_id", con);
                cmd.Parameters.AddWithValue("@res_id", Request.QueryString["restaurant_id"]);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    
                    total_raters = Convert.ToInt32(reader["restaurant_total_raters"]);
                    curr_rating = Convert.ToDouble(reader["restaurant_rating"]);

                    
                    curr_rating = ((curr_rating * total_raters) + Convert.ToInt32(RatingList.SelectedValue)) / (double) (total_raters+1);
                    total_raters += 1;
                }
                reader.Close();

                if (total_raters!=0)
                { 
                    try
                    {
                        cmd.CommandText = "UPDATE restaurants SET restaurant_total_raters=@total_raters, restaurant_rating=@curr_rating WHERE restaurant_id=@res_id";
                        //cmd.Parameters.AddWithValue("@res_id", Request.QueryString["restaurant_id"]);
                        cmd.Parameters.AddWithValue("@total_raters", total_raters);
                        cmd.Parameters.AddWithValue("@curr_rating", (float)curr_rating);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        
                        Response.Write("Error in sql 1" + ex.Message);
                    }

                    try
                    {
                        cmd.CommandText = "INSERT INTO user_ratings(rating_value,user_id,restaurant_id) VALUES(@rating_val, @user_id, @res_id)";
                        cmd.Parameters.AddWithValue("@rating_val", Convert.ToInt32(RatingList.SelectedValue));
                        cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(userCookie["ID"]));
                        //cmd.Parameters.AddWithValue("@res_id", Request.QueryString["restaurant_id"]);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        Response.Write("Error in sql 2" + ex.Message);
                    }


                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                
            }

            RateLabel.Text = "Submitted";
            Restaurant_Load(Request.QueryString["restaurant_id"]);
            RateButton.Enabled = false;

        }
    }

    protected void CommentButton_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        if (Request.QueryString["restaurant_id"] == null)
            return;

        HttpCookie userCookie = Request.Cookies["UserCookie"];

        if (userCookie!=null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO user_comments(user_id, restaurant_id, comment_content) VALUES(@user_id, @res_id, @content)", con);
                cmd.Parameters.AddWithValue("@user_id", userCookie["ID"]);
                cmd.Parameters.AddWithValue("@res_id", Request.QueryString["restaurant_id"]);
                cmd.Parameters.AddWithValue("@content", CommentBox.Text.ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

            }

            CommentView.DataBind();
            CommentButton.Enabled = false;
            CommentMsg.Text = "Submitted";
        }


    }
}
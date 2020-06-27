using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;



public partial class Login : System.Web.UI.Page
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

        this.Form.DefaultButton = Button1.UniqueID;

        HyperLink hl = (HyperLink)Master.FindControl("Home");
        hl.Visible = false;

        Button lb = (Button) Master.FindControl("LogoutButton");
        lb.Visible = false;

        Label la = (Label) Master.FindControl("Label1");
        la.Visible = false;



        HttpCookie userCookie;
        userCookie = Request.Cookies["UserCookie"];

        if (userCookie!=null)
        {
            string userType;
            userType = userCookie["UserType"];
            if (userType=="admin")
            {
                Response.Redirect("AdminLanding.aspx");
            }
            else if (userType == "user")
            {
                Response.Redirect("UserLanding.aspx");
            }
            else { }
        }
        else
        {
            userCookie = new HttpCookie("UserCookie");
            userCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(userCookie);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        HttpCookie userCookie;
        try
        {
            if (!Page.IsValid)
                return;
            userCookie = Request.Cookies["UserCookie"];
            string uid = TextBox1.Text;
            string pass = TextBox2.Text;
            
            string qry = "select * from users where user_name=@user_name and password=@passwd";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@user_name", uid);
            cmd.Parameters.AddWithValue("@passwd", pass);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();            
            if (sdr.Read())
            {
                
                userCookie["Name"] = sdr["user_name"].ToString();
                userCookie["ID"] = sdr["user_id"].ToString();
                int is_admin = Convert.ToInt32(sdr["is_admin"]);
                if (is_admin == 1)
                {
                    //Label3.Text = "Admin Login";
                    userCookie["UserType"] = "admin";
                    Response.Cookies.Add(userCookie);
                    
                    Response.Redirect("AdminLanding.aspx");
                }
                else
                {
                    // Label3.Text = "User Login";
                    userCookie["UserType"] = "user";
                    Response.Cookies.Add(userCookie);
                    
                    Response.Redirect("UserLanding.aspx");
                }

            }
            else
            {
                Label3.Text = "UserId & Password Is not correct Try again..!!";

            }
            con.Close();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }




    }
 }

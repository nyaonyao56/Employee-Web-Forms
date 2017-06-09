using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            if(userText.Text == "admin" && passText.Text == "mesos")
            {
                Response.Redirect("Employee.aspx");
            }else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Wrong user or password, please try again');", true);
            }
        }
    }
}
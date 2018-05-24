using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage {
    protected void Page_Load(object sender, EventArgs e) {
        UsernameLabel.Text = SessionManager.Instance.LoggedUser == null ? "" : SessionManager.Instance.LoggedUser;
        LoginButton.Text = SessionManager.Instance.LoggedIn ? "Logout" : "Login";
        
        if (!SessionManager.Instance.LoggedIn) {
            MenuPanel.Visible = false;
        }
    }

	public void ShowError(bool success, string message) {
		ResponsePanel.CssClass = "alert alert-dismissible " + (success ? "alert-success" : "alert-danger");
		ResponsePanel.Controls.Clear();
		ResponsePanel.Visible = true;

		HtmlGenericControl a = new HtmlGenericControl();
		a.TagName = "a";
		a.Attributes["href"] = "#";
		a.Attributes["class"] = "close";
		a.Attributes["data-dismiss"] = "alert";
		a.Attributes["aria-label"] = "close";
		a.InnerHtml = "&times;";

		ResponsePanel.Controls.Add(a);

		HtmlGenericControl p = new HtmlGenericControl();
		p.TagName = "p";
		p.InnerHtml = message;

		ResponsePanel.Controls.Add(p);
	}

	public void Alert(string message) {
		ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + message + "')", true);
	}

    protected void LoginButton_Click(object sender, EventArgs e) {
        if (SessionManager.Instance.LoggedIn) {
            SessionManager.Instance.Logout();
            Response.Redirect("openings.aspx");
        } else {
            Response.Redirect("login.aspx");
        }
    }
}

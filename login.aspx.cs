using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void LoginButton_Click(object sender, EventArgs e) {
        string error;
        User user;
        bool success = UsersTable.Instance.SelectUser(LoginUsernameTextBox.Text, out user, out error);

        if (!success) {
            (Master as MasterPage).ShowError(success, error);
            return;
        }

        if (user == null) {
            (Master as MasterPage).ShowError(false, "User does not exist.");
            return;
        } else if (user.password != LoginPasswordTextBox.Text) {
            (Master as MasterPage).ShowError(false, "Wrong password.");
            return;
        }

        SessionManager.Instance.Login(user.username);
        Response.Redirect("openings.aspx");
    }

    protected void RegisterButton_Click(object sender, EventArgs e) {
        string error;
        bool success = UsersTable.Instance.Insert(RegisterUsernameTextBox.Text, RegisterPasswordTextBox.Text, FirstNameTextBox.Text, LastNameTextBox.Text, out error);

        (Master as MasterPage).ShowError(success, error);
    }

    protected void ShouldLogin_Click(object sender, EventArgs e) {
        RegisterPanel.Visible = false;
        LoginPanel.Visible = true;
    }
    protected void ShouldRegister_Click(object sender, EventArgs e) {
        LoginPanel.Visible = false;
        RegisterPanel.Visible = true;
    }
}
